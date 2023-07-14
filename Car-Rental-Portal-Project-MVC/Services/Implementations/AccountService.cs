using AutoMapper;
using Azure;
using Car_Rental_Portal_Project_MVC.Data;
using Car_Rental_Portal_Project_MVC.Models;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Account;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Car;
using Car_Rental_Portal_Project_MVC.Services.Interfaces;
using Google.Apis.Gmail.v1;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System;
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
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager, ApplicationDbContext db, UrlEncoder urlEncoder, IMapper mapper, IEmailService emailService)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _db = db;
            _urlEncoder = urlEncoder;
            _mapper = mapper;
            _emailService = emailService;
        }
        /// <summary>
        /// This is Account Login Method, Used to Log In user using parameter "LoginViewModel" model.
        /// </summary>
        /// <param name="model"> Login View Model. </param>
        /// <returns></returns>
        public async Task<ServiceResponse<LoginViewModel>> LogIn(LoginViewModel model)
        {
            //Creating Response.
            var response = new ServiceResponse<LoginViewModel>();
            //Trying To Log in the user.
            var result = await _signinManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: true);
            //Getting User From Database.
            var user = _db.AplicationUsers
                .FirstOrDefault(x => x.UserName.ToLower() == model.Username.ToLower());
            try
            {
                if (result.Succeeded)
                {
                    //if User has been signed in.
                    response.Data = model;
                    response.Message = $"Successfully Logged in";
                    return response;
                }
                else if (result.IsLockedOut)
                {
                    //If User is blocked.
                    var time = user.LockoutEnd - DateTime.UtcNow;
                    var Seconds = time.Value.Seconds;
                    var Minutes = time.Value.Minutes;
                    string ErrorMessage = $"Hello {user.UserName}. Your Account is Locked for {Minutes} Minutes {Seconds} seconds";

                    response.success = false;
                    response.Data = model;
                    response.Message = "LockOut";
                    response.Description = ErrorMessage;
                    return response;
                }
            }
            catch (Exception e)
            {
                //if Exception happens.
                response.success = false;
                response.Message = e.Message;
                response.Description = e.Source;
                return response;
            }
            //else.
            response.success = false;
            response.Message = $"We couldn't Log {model.Username} in.";
            response.Description = result.ToString();
            return response;
            
        }
        /// <summary>
        /// Register GET method, used to draw Registers' View.
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse<RegisterViewModel>> Register()
        {
            var response = new ServiceResponse<RegisterViewModel>();
            try
            {
                //Creating ViewModel (in case we have claims etc.)
                var registerViewModel = new RegisterViewModel();
                
                response.Data = registerViewModel;
                response.Message = $"Succesfully Created Register View Model, and Returned returnURL.";

                return response;
            }
            catch (Exception e)
            {
                //if exception.
                response.Message = e.Message;
                response.success = false;
                response.Description = e.InnerException.Message;
                return response;
            }
        }
        /// <summary>
        /// Register POST method, Which is used to register User, using RegisterViewModel Parameter.
        /// </summary>
        /// <param name="model"> Register View Model </param>
        /// <returns></returns>
        public async Task<ServiceResponse<RegisterViewModel>> Register(RegisterViewModel model)
        {
            //Creating Response
            var response = new ServiceResponse<RegisterViewModel>();
            try
            {
                //creating User
                var User = _mapper.Map<ApplicationUser>(model);
                //Registering.
                var result = await _userManager.CreateAsync(User, model.Password);
                if (result.Succeeded)
                {
                    //SUCCESS
                    await _signinManager.SignInAsync(User, isPersistent: false);
                    response.Data = model;
                    response.Message = $"Successfully Registered {model.Name} into database!!";
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
                //If Exception Occurs.
                response.Message = e.Message;
                response.success = false;
                response.Description = e.InnerException.Message;
                return response;
            }
        }
        /// <summary>
        /// Password Forget POST Method, which is used to Generate Token for reseting password.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ServiceResponse<ForgotPasswordViewModel>> ForgetPassword(ForgotPasswordViewModel model, IUrlHelper Url, HttpContext HttpContext)
        {
            var response = new ServiceResponse<ForgotPasswordViewModel>();
            try
            {
                //getting logged in user.
                var user = await _userManager.FindByEmailAsync(model.Email);
                //checking user.
                if (user == null)
                {
                    //if its null then redirect
                    response.success = false;
                    response.Message = "User is Null";
                    response.Description = "We couldn't Locate the user.";
                    return response;
                }

                //generating token.
                var Token = await _userManager.GeneratePasswordResetTokenAsync(user);
                //getting CallBackUrl
                var callBackUrl = Url.Action("ResetPassword", "Account",
                    new
                    {
                        UserId = user.Id,
                        Code = Token,
                    }
                    , protocol: HttpContext.Request.Scheme);
                //Sending To Mail.
                await _emailService.SendEmailAsync(model.Email, "Reset Password",
                    "Please Reset your Password Follow the Link:<a href=\"" + callBackUrl + "\">click Here</a> ");

                response.success = true;
                response.Message = "Message Has Been Sent.";
                response.Description = $"We have Sent Email Confirmation To {user.Email}";
                return response;

            }
            catch (Exception e)
            {
                response.success = false;
                response.Message = e.Message;
                response.Description = e.InnerException.Message;
                return response;
            }
        }
        /// <summary>
        /// Password Reseting POST Method, Used for reseting password.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<ResetPasswordViewModel>> ResetPassword(ResetPasswordViewModel model)
        {
            //creating response
            var response = new ServiceResponse<ResetPasswordViewModel>();
            try
            {
                //getting user.
                var user = await _userManager.FindByIdAsync(model.UserId.ToString());
                if (user == null)
                {
                    //if its null then redirect
                    response.success = false;
                    response.Message = "User is Null";
                    response.Description = "We couldn't Locate the user.";
                    return response;
                }
                //getting and checking result.
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                if (result.Succeeded)
                {
                    response.success = true;
                    response.Message = "Result is successful.";
                    response.Description = $"We have Reseted Password of the {user.Email}";
                    return response;
                }
                //else
                response.success = false;
                response.Message = "error";
                return response;
            }
            catch (Exception e)
            {
                response.success = false;
                response.Message = e.Message;
                response.Description = e.InnerException.Message;
                return response;
            }
            
        }

        public Task<ServiceResponse<GetCarViewModel>> RentCar(GetCarViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetCarViewModel>> HireCar(GetCarViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
