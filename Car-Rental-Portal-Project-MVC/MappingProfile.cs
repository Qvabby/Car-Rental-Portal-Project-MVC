using AutoMapper;
using Car_Rental_Portal_Project_MVC.Models;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Account;

namespace Car_Rental_Portal_Project_MVC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Syntax - CreateMap< , >().ReverseMap();
            CreateMap<RegisterViewModel, ApplicationUser>().ReverseMap();
        }
    }
}
