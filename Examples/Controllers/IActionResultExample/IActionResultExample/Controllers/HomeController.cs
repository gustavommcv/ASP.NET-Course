using Microsoft.AspNetCore.Mvc;
using IActionResultExample.Models;

namespace IActionResultExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("/book/{bookid?}")]
        public IActionResult Index([FromRoute] int? bookid, [FromQuery] bool? isloggedin, Book book)
        {
            if (book.bookid.HasValue == false)
            {
                //Response.StatusCode = 400;
                //return Content("Book id is not supplied");
                return BadRequest("Book id is not supplied or is empty");
            }

            if (book.bookid <= 0)
            {
                //Response.StatusCode = 400;
                //return Content("Book id cannot be less than 0");
                return BadRequest("Book id cannot be less than 0");
            }

            if (book.bookid > 1000)
            {
                Response.StatusCode = 400;
                return Content("Book id cannot be greater than 1000");
            }

            if (isloggedin.HasValue == false || isloggedin == false)
            {
                //Response.StatusCode = 401;
                //return Content("User must be authenticated");
                return Unauthorized("User must be authenticated");
            }

            return Content(book.ToString());

        }
    }
}
