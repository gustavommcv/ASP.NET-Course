using Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WeatherAppDependencyInjection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICitiesService _citiesService;

        public HomeController(ICitiesService citiesService)
        {
            _citiesService = citiesService;
        }

        [HttpGet]
        [Route("/")]
        [Route("/home")]
        public IActionResult Index()
        {
            ViewBag.Title = "Index";
            return View(_citiesService.GetCities().ToList());
        }

        [HttpGet]
        [Route("weather/{cityCode?}")]
        public IActionResult GetWeather(string? cityCode)
        {
            ViewBag.Title = "City";
            if (cityCode == null) { return BadRequest("City Code must be supplied"); }

            var city = _citiesService.GetCities().Where(city => city.CityUniqueCode.Equals(cityCode)).FirstOrDefault();

            if (city != null)
                return View(city);

            return NotFound("City not found!");
        }
    }
}
