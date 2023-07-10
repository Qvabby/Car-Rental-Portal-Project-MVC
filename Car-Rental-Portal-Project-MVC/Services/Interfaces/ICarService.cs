using Car_Rental_Portal_Project_MVC.Models.Enums;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Car;

namespace Car_Rental_Portal_Project_MVC.Services.Interfaces
{
	public interface ICarService
	{
		Task<ServiceResponse<List<CarViewModel>>> GetAllCars();
		Task<ServiceResponse<CarViewModel>> GetCarById(int id);
		Task<ServiceResponse<CarViewModel>> AddCar(CarViewModel car);
		Task<ServiceResponse<CarViewModel>> UpdateCar(CarViewModel car);
		Task<ServiceResponse<CarViewModel>> DeleteCar(int id);
	}

}
