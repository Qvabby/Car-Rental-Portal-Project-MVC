using Car_Rental_Portal_Project_MVC.Models.Enums;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Car;

namespace Car_Rental_Portal_Project_MVC.Services.Interfaces
{
	public interface ICarService
	{
		Task<ServiceResponse<List<GetCarViewModel>>> GetAllCars();
		Task<ServiceResponse<GetCarViewModel>> GetCarById(int id);
		Task<ServiceResponse<GetCarViewModel>> AddCar(AddCarViewModel car);
		Task<ServiceResponse<GetCarViewModel>> UpdateCar(UpdateCarViewModel car);
		Task<ServiceResponse<GetCarViewModel>> DeleteCar(int id);
	}

}
