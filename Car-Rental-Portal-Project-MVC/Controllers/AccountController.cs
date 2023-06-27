using Car_Rental_Portal_Project_MVC.Data;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Account;
using Car_Rental_Portal_Project_MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Car_Rental_Portal_Project_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public readonly UserManager<IdentityUser> _userManager;
        public readonly SignInManager<IdentityUser> _signinManager;
        public readonly ApplicationDbContext _db;


        public AccountController(UserManager<IdentityUser> usermanager, SignInManager<IdentityUser> signinmanager, ApplicationDbContext db)
        {
            _userManager = usermanager;
            _signinManager = signinmanager;
            _db = db;        
        }
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
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            ReturnUrl = ReturnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var result = await _signinManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var user = _db.AplicationUsers.FirstOrDefault(x => x.Email.ToLower() == model.Email.ToLower());
                    var claims = await _userManager.GetClaimsAsync(user);
                    if (claims.Count > 0)
                    {
                        await _userManager.RemoveClaimAsync(user, claims.FirstOrDefault(x => x.Type == "FirstName"));
                    }
                    await _userManager.AddClaimAsync(user, new Claim("FirstName", user.Name));
                    return LocalRedirect(ReturnUrl);
                }
                else if (result.IsLockedOut)
                {
                    var user = _db.AplicationUsers.FirstOrDefault(u => u.Email == model.Email);
                    var time = user.LockoutEnd - DateTime.UtcNow;
                    var Seconds = time.Value.Seconds;
                    var Minutes = time.Value.Minutes;
                    string ErrorMessage = $"Hello {user.Email}. Your Account is Locked for {Minutes} Minutes {Seconds} seconds";
                    ModelState.AddModelError(string.Empty, ErrorMessage);
                    return View(model);
                }
            }
            return RedirectToAction("Login");
        }

    }
}
