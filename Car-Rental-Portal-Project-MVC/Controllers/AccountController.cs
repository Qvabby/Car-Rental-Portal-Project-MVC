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

    }
}
