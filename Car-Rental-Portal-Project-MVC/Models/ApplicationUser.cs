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
        [ForeignKey("ApplicationUserId")]
        public ICollection<ApplicationCar> ApplicationCars { get; set; } = new List<ApplicationCar>();
        public ICollection<RentOrder> RentOrders { get; set; } = new List<RentOrder>();
        public ICollection<int?> LikedCars { get; set; } = new List<int?>();
    }
}
