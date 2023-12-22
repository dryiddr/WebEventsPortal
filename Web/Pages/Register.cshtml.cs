using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class RegisterModel : PageModel
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public RegisterModel(IConfiguration configuration, HttpClient httpClient)
    {
        _configuration = configuration;
        _httpClient = httpClient;
    }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }

    [BindProperty]
    public string NickName { get; set; }

    [BindProperty]
    public string Name { get; set; }

    [BindProperty]
    public string Description { get; set; }

    [BindProperty]
    public IFormFile Avatar { get; set; }

    [BindProperty]
    public string ErrorMessage { get; set; }

    public void OnGet()
    {
        // Logic when the page is loaded (if needed)
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var apiUrl = _configuration.GetValue<string>("ApiUrl");
        var registerUrl = $"{apiUrl}/api/Auth/register";

        try
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(Email), "email");
                content.Add(new StringContent(Password), "password");
                content.Add(new StringContent(NickName), "nickName");
                content.Add(new StringContent(Name), "name");
                content.Add(new StringContent(Description), "description");

                if (Avatar != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        await Avatar.CopyToAsync(stream);
                        content.Add(new ByteArrayContent(stream.ToArray()), "avatar", Avatar.FileName);
                    }
                }

                var response = await _httpClient.PostAsync(registerUrl, content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();

                // Get the token from the response
                var token = JsonConvert.DeserializeObject<JObject>(responseBody)["access_token"].ToString();

                // Save the token in the session
                HttpContext.Session.SetString("access_token", token);

                // If registration is successful, redirect to the main page
                return RedirectToPage("/Index");
            }
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = "An error occurred while processing your request.";
        }

        return Page();
    }
}
