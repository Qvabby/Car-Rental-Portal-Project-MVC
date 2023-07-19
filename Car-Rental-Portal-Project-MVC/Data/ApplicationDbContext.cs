using Car_Rental_Portal_Project_MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_Portal_Project_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public ICollection<CarRental> CarRentals { get; set; }
        public DbSet<ApplicationUser> AplicationUsers { get; set; }
        public DbSet<ApplicationCar> ApplicationCars => Set<ApplicationCar>();
    }
}