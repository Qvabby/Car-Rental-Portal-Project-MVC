using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Portal_Project_MVC.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
