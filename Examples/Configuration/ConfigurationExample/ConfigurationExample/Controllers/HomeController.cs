using ConfigurationExample.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeatherApiOptions _options;

        public HomeController(IOptions<WeatherApiOptions> weatherApiOptions)
        {
            _options = weatherApiOptions.Value;
        }

        [Route("/")]
        public IActionResult Index()
        {
            /*
            ViewBag.MyKey = _configuration.GetValue<string>("weatherapi:clientid", "Cannot found the key");
            ViewBag.MyAPIKey = _configuration.GetValue<string>("weatherapi:clientsecret", "Cannot found the key");
            */
            //var weatherapiSection = _options.GetSection("weatherapi").Get<WeatherApiOptions>();

            /*var options1 = new WeatherApiOptions();
            options1.GetSection("weatherapi").Bind(_options);*/

            /* ViewBag.MyKey = _configuration.GetSection("weatherapi")["clientid"];
             ViewBag.MyAPIKey = _configuration.GetSection("weatherapi")["clientsecret"];*/
            /* ViewBag.MyKey = weatherapiSection.GetValue<string>("clientid");
             ViewBag.MyAPIKey = weatherapiSection.GetValue<string>("clientsecret");*/

            ViewBag.MyAPIKey = _options.ClientSecret;
            //ViewBag.MyAPIKey = weatherapiSection.ClientSecret;
            ViewBag.MyKey = _options.ClientID;

            return View();
        }
    }
}
