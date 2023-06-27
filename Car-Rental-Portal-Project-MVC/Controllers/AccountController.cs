using Car_Rental_Portal_Project_MVC.Models.ViewModels.Account;
using Car_Rental_Portal_Project_MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Portal_Project_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Register(string? returnUrl = null)
        {
            //FIRST WE ARE DOING THE FUNCTIONILITY AND RETURNING RESPONSE FROM THE SERVICE.
            var response = await _accountService.Register();
            if (response.success)
            {
                //SECOND WE DO ALL THE WEB THING IN CONTROLLER IF NEEDED.
                ViewData["ReturnUrl"] = returnUrl;
                //LASTLY RETURNING.
                return View(response.Data);
            }
            else
            {
                return NotFound();
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var response = await _accountService.Register(model);
            if (response.success)
            {
                return RedirectToAction("Home", "Index");
            }
            else
            {
                return NotFound();
            }
        }

    }
}
