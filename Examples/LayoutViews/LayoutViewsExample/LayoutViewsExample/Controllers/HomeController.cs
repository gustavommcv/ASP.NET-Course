using Microsoft.AspNetCore.Mvc;

namespace LayoutViewsExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route(nameof(Index))]
        public IActionResult Index()
        {
            return View();
        }

        [Route(nameof(About))]
        public IActionResult About()
        {
            return View();
        }

        [Route(nameof(Contact))]
        public IActionResult Contact()
        {
            return View();
        }
    }
}
