using AutoMapper;
using Car_Rental_Portal_Project_MVC.Data;
using Car_Rental_Portal_Project_MVC.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Portal_Project_MVC.Models.ViewModels.Car
{
    public class CarFilterViewModel
    {
        [StringLength(20)]
        public string? Manufacturer { get; set; }
        [StringLength(20)]
        public string? Model { get; set; }
        [Range(1900, 2023)]
        public int? Year { get; set; }
        [Range(0, 99999)]
        public decimal? Price { get; set; }
        public float? Engine { get; set; }
        public TransmissionEnum? Transmission { get; set; }
        public FuelTypeEnum? FuelType { get; set; }
        public int? FuelTank { get; set; }
        public WheelTypeEnum? WheelType { get; set; }
        [StringLength(20)]
        public string? Location { get; set; }
        public int? PeopleAmount { get; set; }
        public List<GetCarViewModel> CarsToFilter { get; set; }

    }
}
