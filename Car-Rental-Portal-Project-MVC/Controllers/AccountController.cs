﻿using AutoMapper;
using Car_Rental_Portal_Project_MVC.Data;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Account;
using Car_Rental_Portal_Project_MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Car_Rental_Portal_Project_MVC.Models;

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
        public readonly IMapper _mapper;

        public AccountController(UserManager<IdentityUser> usermanager, SignInManager<IdentityUser> signinmanager, ApplicationDbContext db, IAccountService accountService, IEmailService emaiService, IMapper mapper)
        {
            _userManager = usermanager;
            _signinManager = signinmanager;
            _db = db;
            _accountService = accountService;
            _emailService = emaiService;
            _mapper = mapper;
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
                else if (response.Message == "LockOut")
                {
                    //if we have lockout.
                    ModelState.AddModelError(string.Empty, response.Description);
                    return View(response.Data);
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
        public async Task<IActionResult> Profile()
        {
            var userid = await _userManager.GetUserIdAsync(await _userManager.GetUserAsync(User));

            var user = await _db.AplicationUsers
                .Include(u => u.OwnCars)
                .FirstOrDefaultAsync(x => x.Id == userid);

            var UserModel = _mapper.Map<ProfileViewModel>(user);
            return View(UserModel);
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

        // ...

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signinManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl, string? remoteError)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider : {remoteError}");
                return View(nameof(Login));
            }
            var info = await _signinManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }
            var result = await _signinManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false); //logofffromsystem
            if (result.Succeeded)
            {
                //uf the user havean acc
                await _signinManager.UpdateExternalAuthenticationTokensAsync(info);
                return LocalRedirect(returnUrl);
            }
            else if (result.RequiresTwoFactor)
            {
                return RedirectToAction("VerifyAuthCode", new { ReturnUrl = returnUrl });
            }
            else
            {
                //if user doesnt have acc
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["ProviderDisplayName"] = info.ProviderDisplayName;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var name = info.Principal.FindFirstValue(ClaimTypes.Name);
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email, Name = name });

            }
            return View(nameof(Login));
        }
    }
}

