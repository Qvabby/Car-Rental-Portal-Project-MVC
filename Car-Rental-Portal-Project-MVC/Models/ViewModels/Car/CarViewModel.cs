using Car_Rental_Portal_Project_MVC.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Portal_Project_MVC.Models.ViewModels.Car
{
	public class CarViewModel
	{
		public int Id { get; set; }

		[Required]
		[StringLength(20)]
		public string Manufacturer { get; set; }

		[Required]
		[StringLength(20)]
		public string Model { get; set; }

		[Required]
		[Range(1900, 2023)]
		public int Year { get; set; }

		[Required]
		[Range(0, 99999)]
		public decimal Price { get; set; }

		[Required]
		public float Engine { get; set; }

		[Required]
		public TransmissionEnum Transmission { get; set; }

		[Required]
		public FuelTypeEnum FuelType { get; set; }

		[Required]
		public int FuelTank { get; set; }

		[Required]
		public WheelTypeEnum WheelType { get; set; }

		[Required]
		[StringLength(20)]
		public string Location { get; set; }

		[Required]
		public int PeopleAmount { get; set; }

		[Required]
		public bool Rented { get; set; }

		public string RentedByUserId { get; set; }
	}
}
