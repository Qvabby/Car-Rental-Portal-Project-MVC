using Car_Rental_Portal_Project_MVC.Data;
using Car_Rental_Portal_Project_MVC.Models;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Account;
using Car_Rental_Portal_Project_MVC.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Car_Rental_Portal_Project_MVC.Services.Implementations
{
    public class AccountService : IAccountService
    {
        public readonly UserManager<IdentityUser> _userManager;
        public readonly SignInManager<IdentityUser> _signinManager;
        public readonly ApplicationDbContext _db;
        private readonly UrlEncoder _urlEncoder;
        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager, ApplicationDbContext db, UrlEncoder urlEncoder)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _db = db;
            _urlEncoder = urlEncoder;
        }

        public async Task<ServiceResponse<LoginViewModel>> LogIn()
        {
            var response = new ServiceResponse<LoginViewModel>();
            try
            {
                //Login GET FUNCTIONALITY
                var loginViewModel = new LoginViewModel();

                response.Data = loginViewModel;
                response.Message = $"Succesfully Created Register View Model, and Returned returnURL.";

                return response;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.success = false;
                response.Description = e.InnerException.Message;
                return response;
            }

        }

        public async Task<ServiceResponse<LoginViewModel>> LogIn(LoginViewModel model)
        {
            var response = new ServiceResponse<LoginViewModel>();

            var result = await _signinManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var user = _db.AplicationUsers.FirstOrDefault(x => x.Email.ToLower() == model.Email.ToLower());

                response.Data = model;
                response.Message = $"Successfully Logged in";
                return response;
            }
            else if (result.IsLockedOut)
            {
                var user = _db.AplicationUsers.FirstOrDefault(u => u.Email == model.Email);
                var time = user.LockoutEnd - DateTime.UtcNow;
                var Seconds = time.Value.Seconds;
                var Minutes = time.Value.Minutes;
                string ErrorMessage = $"Hello {user.Email}. Your Account is Locked for {Minutes} Minutes {Seconds} seconds";

                response.IsLockedOut = true;
                response.Data = model;
                response.Message = ErrorMessage;
            }
            return response;
            
        }

        //REGISTER GET
        public async Task<ServiceResponse<RegisterViewModel>> Register()
        {
            var response = new ServiceResponse<RegisterViewModel>();
            try
            {
                //REGISTER GET FUNCTIONALITY
                var registerViewModel = new RegisterViewModel();
                
                response.Data = registerViewModel;
                response.Message = $"Succesfully Created Register View Model, and Returned returnURL.";

                return response;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.success = false;
                response.Description = e.InnerException.Message;
                return response;
            }
        }

        public async Task<ServiceResponse<RegisterViewModel>> Register(RegisterViewModel model)
        {
            var response = new ServiceResponse<RegisterViewModel>();
            try
            {
                //creating User
                var User = new ApplicationUser 
                { 
                    UserName = model.Username, 
                    Email = model.Email, 
                    Name = model.Name, 
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                };
                //Registering.
                var result = await _userManager.CreateAsync(User, model.Password);
                if (result.Succeeded)
                {
                    //SUCCESS
                    await _signinManager.SignInAsync(User, isPersistent: false);
                    response.Data = model;
                    response.Message = $"Successfully Registered {model.Name} into database.";
                    response.Description = $"bla bla";
                    return response;
                }
                //NOT SUCCESS
                response.Data = model;
                response.success = false;
                response.Message = result.ToString();
                return response;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.success = false;
                response.Description = e.InnerException.Message;
                return response;
            }
        }

    }
}
