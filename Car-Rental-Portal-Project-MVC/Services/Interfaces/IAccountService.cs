using Car_Rental_Portal_Project_MVC.Models;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Account;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Car;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Portal_Project_MVC.Services.Interfaces
{
    public interface IAccountService
    {
        //get
        Task<ServiceResponse<RegisterViewModel>> Register();
        //post
        Task<ServiceResponse<RegisterViewModel>> Register(RegisterViewModel model);
        //post
        Task<ServiceResponse<LoginViewModel>> LogIn(LoginViewModel model);
        //post
        Task<ServiceResponse<ForgotPasswordViewModel>> ForgetPassword(ForgotPasswordViewModel model, IUrlHelper Url, HttpContext HttpContext);
        //post
        Task<ServiceResponse<ResetPasswordViewModel>> ResetPassword(ResetPasswordViewModel model);
        //post
        Task<ServiceResponse<GetCarViewModel>> RentCar(GetCarViewModel model);
        //post
        Task<ServiceResponse<GetCarViewModel>> HireCar(GetCarViewModel model);
    }
}
