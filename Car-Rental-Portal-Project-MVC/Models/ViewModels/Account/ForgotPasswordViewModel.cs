using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Portal_Project_MVC.Models.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }   

    }
}
