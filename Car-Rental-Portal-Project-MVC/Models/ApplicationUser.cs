using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental_Portal_Project_MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; } = string.Empty;
        [PersonalData]
        public string LastName { get; set; } = string.Empty;
        public DateTime UserCreatedAt { get; set; } = DateTime.Now;
        //relations
        [ForeignKey("UserId")]
        public ICollection<ApplicationCar> OwnCars { get; set; } = new List<ApplicationCar>();
    }
}
