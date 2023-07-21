using Car_Rental_Portal_Project_MVC.Models.ViewModels.Car;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Car_Rental_Portal_Project_MVC.Models.ViewModels.Account
{
    public class ProfileViewModel
    {
        //<p>Username: @DBuser.UserName</p>
        //           <p>Full Name: @DBuser.Name @DBuser.LastName</p>
        //           <p>Email: @DBuser.Email</p>
        //           <p>Phone: @DBuser.PhoneNumber</p>
        public string Username { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<GetCarViewModel> ApplicationCars { get; set; }
        public List<RentOrder> RentOrders { get; set; }
        //public ICollection<int> LikedCars { get; set; } = new List<int>();

    }
}
