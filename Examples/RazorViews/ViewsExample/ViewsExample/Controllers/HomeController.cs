using Microsoft.AspNetCore.Mvc;
using ViewsExample.Models;
namespace ViewsExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        [Route("/home")]
        public IActionResult Index()
        {
            ViewData["appTitle"] = "Asp.Net Core Demo App";
            return View("Index", new Person() { Name = "Gustavo", DateOfBirth = Convert.ToDateTime("2004-07-01"), Gender = Gender.Male } ); // index.cshtml
            // return View("abc"); // abc.cshtml
            // return new ViewResult() { ViewName = "abc" };
        }

        [Route("person-details/{name}")]
        public IActionResult Details(string? name)
        {
            Console.WriteLine(name);
            if (string.IsNullOrEmpty(name)) return BadRequest("Person name can't be null");

            List<Person> persons = new List<Person>()
            {
                new Person() { Name = "Gustavo", DateOfBirth = Convert.ToDateTime("2004-07-01"), Gender = Gender.Male },
                new Person() { Name = "John", DateOfBirth = Convert.ToDateTime("2000-05-06"), Gender = Gender.Male },
                new Person() { Name = "Linda", DateOfBirth = Convert.ToDateTime("2005-01-09"), Gender = Gender.Female }
            };

            Person matchedPerson = persons.Where(person => person.Name == name).FirstOrDefault();

            if (matchedPerson != null) 
                return View(matchedPerson);

            return BadRequest("Person not found!");
        }

        [Route("person-with-product")]
        public IActionResult PersonWithProduct()
        {
            var person = new Person() { Name = "Gustavo", DateOfBirth = Convert.ToDateTime("2004-07-01"), Gender = Gender.Male };
            var product = new Product() { Id = 1, Name = "Nintendo smith" };
            var personAndProduct = new PersonAndProductWrapperModel() { PersonData = person, ProductData = product };

            return View("PersonWithProduct", personAndProduct);
        }
    }
}
