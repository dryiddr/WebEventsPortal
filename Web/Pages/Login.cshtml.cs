using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class LoginModel : PageModel
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public LoginModel(IConfiguration configuration, HttpClient httpClient)
    {
        _configuration = configuration;
        _httpClient = httpClient;
    }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }

    [BindProperty]
    public string ErrorMessage { get; set; }

    public void OnGet()
    {
        // Logic when the page is loaded (if needed)
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var apiUrl = _configuration.GetValue<string>("ApiUrl");
        var loginUrl = $"{apiUrl}/api/Auth/login";

        var loginData = new { email = Email, password = Password };

        try
        {
            var response = await _httpClient.PostAsync(loginUrl,
            new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            // Get the token from the response
            var token = JsonConvert.DeserializeObject<JObject>(responseBody)["access_token"].ToString();

            // Save the token in the session
            HttpContext.Session.SetString("access_token", token);

            // If login is successful, redirect to the main page
            return RedirectToPage("/Index");
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                ErrorMessage = "Invalid email or password";
            }
            else
            {
                ErrorMessage = "An error occurred while processing your request.";
            }
        }

        return Page();
    }
}
