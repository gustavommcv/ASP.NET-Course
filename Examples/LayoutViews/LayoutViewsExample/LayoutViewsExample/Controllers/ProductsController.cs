using Microsoft.AspNetCore.Mvc;

namespace LayoutViewsExample.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route($"{nameof(Search)}/" + "{id?}")]
        public IActionResult Search(int? id)
        {
            ViewBag.Id = id;
            return View();
        }

        [Route($"{nameof(Order)}")]
        public IActionResult Order()
        {
            return View();
        }
    }
}
