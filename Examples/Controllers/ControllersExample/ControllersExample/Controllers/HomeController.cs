using ControllersExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("home")]
        [Route("/")]
        public ContentResult Index()
        {
            /*
            return new ContentResult()
            {
                Content = "<h1>Hello from Index</h1>",
                ContentType = "text/html"
            };
            */
            return Content("Hello from Index", "text/plain");
        }

        [Route("person")]
        public JsonResult Person()
        {
            Person person = new Person { Id = Guid.NewGuid(), Age = 20, FirstName = "Gustavo", LastName = "Veronese" };
            // return new JsonResult(person);
            return Json(person);
        }

        [Route("file")]
        public FileResult File()
        {
            //return new VirtualFileResult("/images/photo.png", "application/png");
            return File("/images/photo.png", "application/png");
        }

        [Route("file2")]
        public IActionResult File2()
        {
            //return new VirtualFileResult("/images/photo.png", "application/png");
            return File("C:\\Users\\gugam\\OneDrive\\Imagens\\AI\\PenguinSuit1.jpg", "application/jpg");
        }
    }
}
