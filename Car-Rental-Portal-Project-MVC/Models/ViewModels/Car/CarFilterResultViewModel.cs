using Car_Rental_Portal_Project_MVC.Models.Enums;
using Car_Rental_Portal_Project_MVC.Services;
using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Portal_Project_MVC.Models.ViewModels.Car
{
	public class CarFilterResultViewModel
	{
		public int Id { get; set; }
		[Required]
		[StringLength(20)]
		public string Manufacturer { get; set; }
		[Required]
		[StringLength(20)]
		public string Model { get; set; }
		//1900-2023
		[Required]
		[MaxLength(2023)]
		[MinLength(1900)]
		public int Year { get; set; }
		[Required]
		[MaxLength(99999)]
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
		public ServiceResponse<CarFilterResultViewModel> FilteredCars { get; internal set; }
	} 
}
