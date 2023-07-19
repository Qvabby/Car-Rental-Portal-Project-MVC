namespace Car_Rental_Portal_Project_MVC.Models
{
    public class RentOrder
    {
        public int id { get; set; }
        public string RentedByUserId { get; set; }

        //Relations
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int CarId { get; set; }
        public ApplicationCar ApplicationCar { get; set; }
    }
}
