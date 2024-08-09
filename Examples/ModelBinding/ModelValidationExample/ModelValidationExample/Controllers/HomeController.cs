using Microsoft.AspNetCore.Mvc;
using ModelValidationExample.Models;

namespace ModelValidationExample.Controllers
{
    public class HomeController : Controller
    {
        // JSON: { "PersonName": "William", "Email": "william@mail.com", "Phone": "999999999",
        // "Password": "123", "ConfirmPassword": "123"}


        [Route("/register")]
        public IActionResult Index([FromBody]
            //[Bind(nameof(Person.PersonName), nameof(Person.Email), 
            //nameof(Person.Password), nameof(Person.ConfirmPassword))]
            Person person, [FromHeader(Name = "User-Agent")] string UserAgent)
        {
            if (!ModelState.IsValid)
            {
                // List<string> errorsList = new List<string>();
                string errors = string.Join("\n", 
                ModelState.Values.SelectMany(value => value.Errors)
                                .Select(error => error.ErrorMessage));
                /*
                foreach (var value in ModelState.Values) 
                { 
                    foreach (var error in value.Errors)
                    {
                        errorsList.Add(error.ErrorMessage);
                    }
                }
                string errors = string.Join("\n", errorsList);
                */
                return BadRequest(errors);
            }
            return Content($"{person}, {UserAgent}");
        }
    }
}
