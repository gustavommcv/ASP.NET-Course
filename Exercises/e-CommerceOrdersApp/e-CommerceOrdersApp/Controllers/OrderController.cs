using e_CommerceOrdersApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_CommerceOrdersApp.Controllers
{
    public class OrderController : Controller
    {
        [HttpPost]
        [Route("/order")]
        public IActionResult Index([FromForm] Order order)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join("\n", ModelState.Values
                    .SelectMany(value => value.Errors)
                    .Select(err => err.ErrorMessage));

                return BadRequest(message);
            }

            order.GenerateOrderNo();

            return Content("New Order Number: " + order.OrderNo.ToString());
        }
    }
}
