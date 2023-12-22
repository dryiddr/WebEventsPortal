using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class CreateEventModel : PageModel
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateEventModel(IConfiguration configuration, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    [BindProperty]
    public string EventName { get; set; }

    [BindProperty]
    public string CategoryId { get; set; }

    [BindProperty]
    public string EventText { get; set; }

    [BindProperty]
    public string Tags { get; set; }

    [BindProperty]
    public string ErrorMessage { get; set; }

    public void OnGet()
    {
        // Logic when the page is loaded (if needed)
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var apiUrl = _configuration.GetValue<string>("ApiUrl");
        var createEventUrl = $"{apiUrl}/api/Article/CreateEvent";

        try
        {
            var eventData = new
            {
                status = 0,
                name = EventName,
                categoryId = "a58b52f5-d8dd-4530-a9bc-1c75a3e6a7f3",
                text = EventText,
                tags = System.Array.Empty<object>()
            };


            // Get the token from the session
            var accessToken = HttpContext.Session.GetString("access_token");
            if (!string.IsNullOrEmpty(accessToken))
            {
                // Add the token to the headers
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await _httpClient.PostAsync(createEventUrl, new StringContent(JsonConvert.SerializeObject(eventData), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            // Handle successful event creation
            return RedirectToPage("/Index");
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = "An error occurred while processing your request.";
        }

        return Page();
    }
}
