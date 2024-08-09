using Microsoft.AspNetCore.Mvc;

namespace EnvinronmentExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment) 
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("/")]
        public IActionResult Index()
        {
            if (_webHostEnvironment.IsDevelopment())
                return View();

            return Content("Not in a development environment");
        }
    }
}
