using Car_Rental_Portal_Project_MVC.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Portal_Project_MVC.Models.ViewModels.Car
{
    public class UpdateCarViewModel
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
        [Range(1900, 2023)]
        public int Year { get; set; }
        [Required]
        [MaxLength(99999)]
        public decimal Price { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$")]
        public float Engine { get; set; }
        [Required]
        [EnumDataType(typeof(TransmissionEnum))]
        public TransmissionEnum Transmission { get; set; }
        public IEnumerable<SelectListItem>? TransmissionList { get; set; }
        [Required]
        [EnumDataType(typeof(FuelTypeEnum))]
        public FuelTypeEnum FuelType { get; set; }
        public IEnumerable<SelectListItem>? FuelTypeList { get; set; }
        [Required]
        public int FuelTank { get; set; }
        [Required]
        [EnumDataType(typeof(WheelTypeEnum))]
        public WheelTypeEnum WheelType { get; set; }
        public IEnumerable<SelectListItem>? WheelTypeList { get; set; }
        [Required]
        [StringLength(20)]
        public string Location { get; set; }
        [Required]
        public int PeopleAmount { get; set; }
        public string HiredByUserId { get; set; }
        public string Description { get; set; }
    }
}
