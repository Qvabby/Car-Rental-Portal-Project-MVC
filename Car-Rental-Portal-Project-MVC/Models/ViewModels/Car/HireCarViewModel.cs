namespace Car_Rental_Portal_Project_MVC.Models.ViewModels.Car
{
    public class HireCarViewModel
    {
        public string UserId { get; set; }
        public int CarId { get; set; }
        public DateTime HiredFrom { get; set; }
        public DateTime HiredTo { get; set; }
    }
}
