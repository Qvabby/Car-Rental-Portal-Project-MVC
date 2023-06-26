using Microsoft.AspNetCore.Identity;

namespace Car_Rental_Portal_Project_MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; } = string.Empty;
        [PersonalData]
        public string LastName { get; set; } = string.Empty;
        public DateTime UserCreatedAt { get; set; } = DateTime.Now;
    }
}
