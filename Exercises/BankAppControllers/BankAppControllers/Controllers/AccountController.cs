using BankAppControllers.Data;
using Microsoft.AspNetCore.Mvc;

namespace BankAppControllers.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {
            return Content("Welcome to the Best Bank");
        }

        [Route("/account-details/{id:int:?}")]
        [HttpGet]
        public IActionResult AccountDetails()
        {
            if (!Request.RouteValues.ContainsKey("id"))
                return NotFound("Account Number should be supplied");

            var id = Convert.ToInt16(Request.RouteValues["id"]);
            if (_accountService.GetByID(id) != null)
                return Json(_accountService.GetByID(id));

            return NotFound("We dont have any account with this number!");
        }

        [Route("/account-statement")]
        [HttpGet]
        public IActionResult AccountStatement()
        {
            return File("sample.pdf", "application/pdf");
        }

        [Route("/get-current-balance/{id:int?}")]
        [HttpGet]
        public IActionResult AccountBalance()
        {
            if (!Request.RouteValues.ContainsKey("id"))
                return NotFound("Account Number should be supplied");

            var id = Convert.ToInt16(Request.RouteValues["id"]);
            if (_accountService.GetByID(id) != null)
                return Content(_accountService.GetByID(id).currentBalance.ToString());

            return NotFound("We dont have any account with this number!");
        }
    }
}
