using Autofac;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace DIExample.Controllers
{
    public class HomeController : Controller
    {

        /*private readonly ICitiesService _citiesService;
        private readonly ICitiesService _citiesService2;
        private readonly ICitiesService _citiesService3;*/
        private readonly ILifetimeScope _scopeFactory;

        public HomeController(//ICitiesService citiesService, ICitiesService citiesService2, ICitiesService citiesService3,
            ILifetimeScope serviceScopeFactory)
        {
            /*_citiesService = citiesService;
            _citiesService2 = citiesService2;
            _citiesService3 = citiesService3;*/
            _scopeFactory = serviceScopeFactory;
        }


        [Route("/")]
        // public IActionResult Index([FromServices] ICitiesService _citiesService)
        public IActionResult Index()
        {
            ViewBag.Title = "Cities";
            //var cities = _citiesService.GetCities();

            /*ViewBag.InstanceId_CitiesService1 = _citiesService.ServiceInstanceId;
            ViewBag.InstanceId_CitiesService2 = _citiesService2.ServiceInstanceId;
            ViewBag.InstanceId_CitiesService3 = _citiesService3.ServiceInstanceId;*/

            List<string> cities = new List<string>();
            using (var scope = _scopeFactory.BeginLifetimeScope()) {
                // Injected CitieService
                var citiesService = scope.Resolve<ICitiesService>();
                // DB work
                cities = citiesService.GetCities();
            } // end of scope; it calls citiesservice.dispose()

            return View(cities);
        }
    }
}
