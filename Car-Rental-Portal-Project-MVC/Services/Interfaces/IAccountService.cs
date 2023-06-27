using Car_Rental_Portal_Project_MVC.Models.ViewModels.Account;

namespace Car_Rental_Portal_Project_MVC.Services.Interfaces
{
    public interface IAccountService
    {
        //get
        Task<ServiceResponse<RegisterViewModel>> Register();
        //post
        Task<ServiceResponse<RegisterViewModel>> Register(RegisterViewModel model);
    }
}
