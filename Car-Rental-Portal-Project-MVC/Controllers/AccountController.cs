using Car_Rental_Portal_Project_MVC.Data;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Account;
using Car_Rental_Portal_Project_MVC.Services;
using Car_Rental_Portal_Project_MVC.Services.Implementations;
using Car_Rental_Portal_Project_MVC.Services.Interfaces;
using identityStep.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Car_Rental_Portal_Project_MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public readonly UserManager<IdentityUser> _userManager;
        public readonly SignInManager<IdentityUser> _signinManager;
        public readonly ApplicationDbContext _db;
        public readonly IEmailService _emailService;


		public AccountController(UserManager<IdentityUser> usermanager, SignInManager<IdentityUser> signinmanager, ApplicationDbContext db, IAccountService accountService, IEmailService emaiService)
		{
			_userManager = usermanager;
			_signinManager = signinmanager;
			_db = db;
			_accountService = accountService;
            _emailService = emaiService;
		}
		[HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string? returnUrl = null)
        {
            //Getting Response from Registers' GET service method.
            var response = await _accountService.Register();
            if (response.success)
            {
                //In case response was successful.
                //ReturnURL.
                ViewData["ReturnUrl"] = returnUrl;
                //returning View
                return View(response.Data);
            }
            else
            {
                //else.
                return NotFound();
            }

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //Getting Response from Services' Register Method.
            var response = await _accountService.Register(model);
            if (response.success)
            {
                //in case response was successful. (user was registered)
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //else.
                return NotFound();
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? ReturnUrl = null)
        {
            //ReturnUrl
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? ReturnUrl = null)
        {
            //ReturnURL
            ViewData["ReturnUrl"] = ReturnUrl;
            ReturnUrl = ReturnUrl ?? Url.Content("~/");
            //Checking ModelState
            if (ModelState.IsValid)
            {
                //Getting Response From Service Login Method
                var response = await _accountService.LogIn(model);
                if (response.success)
                {
                    //If response was successful.
                    return LocalRedirect(ReturnUrl);
                }
                else if (response.IsLockedOut)
                {
                    //if we have lockout.
                    return View(model);
                }
            }
            //else.
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            //signing out any user.
            await _signinManager.SignOutAsync();
            //redirecting to Home Controllers' Index Method.
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
		{
            //Checking model state validation.
			if (ModelState.IsValid)
			{
                //getting logged in user.
                var response = await _accountService.ForgetPassword(model, Url, HttpContext);
				if (response.success)
				{
                    return RedirectToAction("ForgotPasswordConfirmation");
                    
				}
                return RedirectToAction("ForgotPassword");

            }
			return View();
		}
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string Code = null, string UserId = null)
        {
            return Code == null ? RedirectToAction("Error", "Home") : View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            //checking modelstate
            if (ModelState.IsValid)
            {
                var response = await _accountService.ResetPassword(model);
                if (response.success)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }

               return RedirectToAction("ForgotPasswordConfirmation");


            }
            return RedirectToAction("Error", "Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
