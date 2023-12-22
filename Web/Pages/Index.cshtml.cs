using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly Options _options;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(HttpClient httpClient, Options options, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _options = options;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public WeatherInfo ParisWeather { get; private set; }

        [BindProperty]
        public WeatherInfo KievWeather { get; private set; }

        [BindProperty]
        public List<Event> PopularEvents { get; private set; }

        [BindProperty]
        public bool IsAuthenticated { get; private set; }

        [BindProperty]
        public UserInfoObj UserInfo { get; private set; }

        public async Task OnGetAsync()
        {
            // Check user authentication and get user information
            try
            {
                // Get the token from the session
                var accessToken = HttpContext.Session.GetString("access_token");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    // Add the token to the headers
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }

                var userJson = await _httpClient.GetStringAsync($"{_options.ApiUrl}/api/User/GetUser");
                if (!string.IsNullOrEmpty(userJson))
                {
                    IsAuthenticated = true;
                    UserInfo = JsonConvert.DeserializeObject<UserInfoObj>(userJson);

                    UserInfo.Avatar = $"https://avatarstoredroid.blob.core.windows.net/avatars/{UserInfo.Id}.jpg";
                }
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Handle 401 (Unauthorized) error
                IsAuthenticated = false;
                UserInfo = null;
            }

            // Fetch weather data for Paris and Kiev
            ParisWeather = await GetWeatherAsync("Paris");
            KievWeather = await GetWeatherAsync("Kiev");

            this.PopularEvents = new List<Event>(await GetPopularEventsAsync());
        }

        public async Task<IActionResult> OnGetPopularEvents()
        {
            var popularEvents = await GetPopularEventsAsync();
            return new JsonResult(popularEvents);
        }

        private async Task<IEnumerable<Event>> GetPopularEventsAsync()
        {
            var apiUrl = _options.ApiUrl;
            var url = $"{apiUrl}/api/Article/popularEvents?period=month&PageNumber=1&Count=100";

            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<IEnumerable<Event>>(response);
        }

        public class Event
        {
            public string Name { get; set; }
            public int CountLikes { get; set; }
            public int Rating { get; set; }
            public DateTime CreationDate { get; set; }
            public List<string> Tags { get; set; }
            public int CountViews { get; set; }
        }

        public class UserInfoObj
        {
            public string Id { get; set; }
            public string NickName { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Avatar { get; set; }
            public List<Article> Articles { get; set; }
        }

        public class Article
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int CountLikes { get; set; }
            public int Rating { get; set; }
            public DateTime CreationDate { get; set; }
            public List<Tag> Tags { get; set; }
            public int CountViews { get; set; }
        }

        public class Tag
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        private async Task<WeatherInfo> GetWeatherAsync(string city)
        {
            var apiUrl = "https://funceventsapp.azurewebsites.net/api/Weather";
            var response = await _httpClient.GetStringAsync($"{apiUrl}?code=O0q-WibA2WGmu9ML5-FXSrEaM6zjpLCAD2ZY-mbCRBNGAzFuK8qEyw==&city={city}");
            return JsonConvert.DeserializeObject<WeatherInfo>(response);
        }

        public class WeatherInfo
        {
            // Define properties based on the weather response structure
            public string City { get; set; }
            public MainInfo Main { get; set; }
            public List<Weather> weather { get; set; }

            public class MainInfo
            {
                // Define properties for main weather information
                public double Temp { get; set; }
                public double FeelsLike { get; set; }
                // ... (add other properties as needed)
            }

            // Use the Weather class directly
            public class Weather
            {
                public string Main { get; set; }
                public string Description { get; set; }
                public string Icon { get; set; }
                // ... (add other properties as needed)
            }
        }

    }
}
