using Microsoft.AspNetCore.Mvc;
using WeatherAppRazorViews.Models;

namespace WeatherAppRazorViews.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("/")]
        [Route("/home")]
        public IActionResult Index()
        {
            List<CityWeather> cityWeatherList = new List<CityWeather>()
                {
                    new CityWeather()
                    {
                        CityUniqueCode = "LDN",
                        CityName = "London",
                        DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),
                        TemperatureFahrenheit = 33
                    },
                    new CityWeather()
                    {
                        CityUniqueCode = "NYC",
                        CityName = "New York",
                        DateAndTime = Convert.ToDateTime("2030-01-01 3:00"),
                        TemperatureFahrenheit = 60
                    },
                    new CityWeather()
                    {
                        CityUniqueCode = "PAR",
                        CityName = "Paris",
                        DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),
                        TemperatureFahrenheit = 82
                    }
                };

            return View(cityWeatherList);
        }

        [HttpGet]
        [Route("weather/{cityCode?}")]
        public IActionResult GetWeather(string? cityCode)
        {
            if (cityCode == null) { return BadRequest("City Code must be supplied"); }

            List<CityWeather> cityWeatherList = new List<CityWeather>()
                {
                    new CityWeather()
                    {
                        CityUniqueCode = "LDN",
                        CityName = "London",
                        DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),
                        TemperatureFahrenheit = 33
                    },
                    new CityWeather()
                    {
                        CityUniqueCode = "NYC",
                        CityName = "New York",
                        DateAndTime = Convert.ToDateTime("2030-01-01 3:00"),
                        TemperatureFahrenheit = 60
                    },
                    new CityWeather()
                    {
                        CityUniqueCode = "PAR",
                        CityName = "Paris",
                        DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),
                        TemperatureFahrenheit = 82
                    }
                };

            var city = cityWeatherList.Where(city => city.CityUniqueCode.Equals(cityCode)).FirstOrDefault();

            if (city != null)
                return View(city);

            return NotFound("City not found!");
        }
    }
}
