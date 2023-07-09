using Car_Rental_Portal_Project_MVC.Models.Enums;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Car;

namespace Car_Rental_Portal_Project_MVC.Services.Interfaces
{
	public interface ICarService
	{
		Task<ServiceResponse<CarFilterResultViewModel>> FilterByCity(string city);
		Task<ServiceResponse<CarFilterResultViewModel>> FilterByReleaseYear(int startYear, int endYear);
		Task<ServiceResponse<CarFilterResultViewModel>> FilterByCapacity(int capacity);
		Task<ServiceResponse<CarFilterResultViewModel>> FilterByManufacturer(string manufacturer);
		Task<ServiceResponse<CarFilterResultViewModel>> FilterByModel(string model);
		Task<ServiceResponse<CarFilterResultViewModel>> FilterByPrice(decimal minPrice, decimal maxPrice);
		Task<ServiceResponse<CarFilterResultViewModel>> FilterByEngine(float minEngine, float maxEngine);
		Task<ServiceResponse<CarFilterResultViewModel>> FilterByTransmission(TransmissionEnum transmission);
		Task<ServiceResponse<CarFilterResultViewModel>> FilterByFuelType(FuelTypeEnum fuelType);
		Task<ServiceResponse<CarFilterResultViewModel>> FilterByFuelTank(int minFuelTank, int maxFuelTank);
		Task<ServiceResponse<CarFilterResultViewModel>> FilterByWheelType(WheelTypeEnum wheelType);
		Task<ServiceResponse<CarFilterResultViewModel>> FilterByLocation(string location);
		Task<ServiceResponse<CarFilterResultViewModel>> FilterByAll(CarFilterParameters filterParameters);
	}

}
