using CookieAuthentication.App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookieAuthentication.App.Pages
{
    public class IndexModel : PageModel
    {

        public List<WeatherData> WeatherList { get; set; }

        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            // Example data
            WeatherList = new List<WeatherData>
            {
                new WeatherData { Date = "2025-01-22", Temperature = 12, Description = "Sunny" },
                new WeatherData { Date = "2025-01-23", Temperature = 8, Description = "Cloudy" },
                new WeatherData { Date = "2025-01-24", Temperature = 10, Description = "Rainy" }
            };
        }
    }
}
