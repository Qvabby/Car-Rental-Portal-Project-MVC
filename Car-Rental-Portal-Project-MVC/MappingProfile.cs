using AutoMapper;
using Car_Rental_Portal_Project_MVC.Models;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Account;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Car;

namespace Car_Rental_Portal_Project_MVC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Syntax - CreateMap< , >().ReverseMap();
            CreateMap<RegisterViewModel, ApplicationUser>().ReverseMap();
            CreateMap<ProfileViewModel, ApplicationUser>().ReverseMap();
            CreateMap<AddCarViewModel, ApplicationCar>().ReverseMap();
            CreateMap<GetCarViewModel, ApplicationCar>().ReverseMap();
            CreateMap<UpdateCarViewModel, ApplicationCar>().ReverseMap();
            CreateMap<ApplicationCar, GetCarViewModel>().ReverseMap();
            CreateMap<GetCarViewModel,UpdateCarViewModel>().ReverseMap();
        }
    }
}
