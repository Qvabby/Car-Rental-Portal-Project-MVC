using Car_Rental_Portal_Project_MVC.Models.Enums;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Car;
using Car_Rental_Portal_Project_MVC.Services.Interfaces;
using Car_Rental_Portal_Project_MVC.Services;

public class CarService : ICarService
{
	private readonly ICarService _carService;

	public CarService(ICarService carRepository)
	{
		_carService = carRepository;
	}

	public async Task<ServiceResponse<CarFilterResultViewModel>> FilterByCity(string city)
	{
		var response = new ServiceResponse<CarFilterResultViewModel>();

		try
		{
			var filteredCars = await _carService.FilterByCity(city);
			response.Data = new CarFilterResultViewModel { FilteredCars = filteredCars };
			response.Message = "Filtering by city successful.";
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = "Error occurred while filtering by city.";
			response.Description = ex.Message;
		}

		return response;
	}

	public async Task<ServiceResponse<CarFilterResultViewModel>> FilterByReleaseYear(int startYear, int endYear)
	{
		var response = new ServiceResponse<CarFilterResultViewModel>();

		try
		{
			var filteredCars = await _carService.FilterByReleaseYear(startYear, endYear);
			response.Data = new CarFilterResultViewModel { FilteredCars = filteredCars };
			response.Message = "Filtering by release year successful.";
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = "Error occurred while filtering by release year.";
			response.Description = ex.Message;
		}

		return response;
	}

	public async Task<ServiceResponse<CarFilterResultViewModel>> FilterByCapacity(int capacity)
	{
		var response = new ServiceResponse<CarFilterResultViewModel>();

		try
		{
			var filteredCars = await _carService.FilterByCapacity(capacity);
			response.Data = new CarFilterResultViewModel { FilteredCars = filteredCars };
			response.Message = "Filtering by capacity successful.";
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = "Error occurred while filtering by capacity.";
			response.Description = ex.Message;
		}

		return response;
	}

	public async Task<ServiceResponse<CarFilterResultViewModel>> FilterByManufacturer(string manufacturer)
	{
		var response = new ServiceResponse<CarFilterResultViewModel>();

		try
		{
			var filteredCars = await _carService.FilterByManufacturer(manufacturer);
			response.Data = new CarFilterResultViewModel { FilteredCars = filteredCars };
			response.Message = "Filtering by manufacturer successful.";
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = "Error occurred while filtering by manufacturer.";
			response.Description = ex.Message;
		}

		return response;
	}

	public async Task<ServiceResponse<CarFilterResultViewModel>> FilterByModel(string model)
	{
		var response = new ServiceResponse<CarFilterResultViewModel>();

		try
		{
			var filteredCars = await _carService.FilterByModel(model);
			response.Data = new CarFilterResultViewModel { FilteredCars = filteredCars };
			response.Message = "Filtering by model successful.";
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = "Error occurred while filtering by model.";
			response.Description = ex.Message;
		}

		return response;
	}

	public async Task<ServiceResponse<CarFilterResultViewModel>> FilterByPrice(decimal minPrice, decimal maxPrice)
	{
		var response = new ServiceResponse<CarFilterResultViewModel>();

		try
		{
			var filteredCars = await _carService.FilterByPrice(minPrice, maxPrice);
			response.Data = new CarFilterResultViewModel { FilteredCars = filteredCars };
			response.Message = "Filtering by price successful.";
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = "Error occurred while filtering by price.";
			response.Description = ex.Message;
		}

		return response;
	}

	public async Task<ServiceResponse<CarFilterResultViewModel>> FilterByEngine(float minEngine, float maxEngine)
	{
		var response = new ServiceResponse<CarFilterResultViewModel>();

		try
		{
			var filteredCars = await _carService.FilterByEngine(minEngine, maxEngine);
			response.Data = new CarFilterResultViewModel { FilteredCars = filteredCars };
			response.Message = "Filtering by engine successful.";
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = "Error occurred while filtering by engine.";
			response.Description = ex.Message;
		}

		return response;
	}

	public async Task<ServiceResponse<CarFilterResultViewModel>> FilterByTransmission(TransmissionEnum transmission)
	{
		var response = new ServiceResponse<CarFilterResultViewModel>();

		try
		{
			var filteredCars = await _carService.FilterByTransmission(transmission);
			response.Data = new CarFilterResultViewModel { FilteredCars = filteredCars };
			response.Message = "Filtering by transmission successful.";
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = "Error occurred while filtering by transmission.";
			response.Description = ex.Message;
		}

		return response;
	}

	public async Task<ServiceResponse<CarFilterResultViewModel>> FilterByFuelType(FuelTypeEnum fuelType)
	{
		var response = new ServiceResponse<CarFilterResultViewModel>();

		try
		{
			var filteredCars = await _carService.FilterByFuelType(fuelType);
			response.Data = new CarFilterResultViewModel { FilteredCars = filteredCars };
			response.Message = "Filtering by fuel type successful.";
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = "Error occurred while filtering by fuel type.";
			response.Description = ex.Message;
		}

		return response;
	}

	public async Task<ServiceResponse<CarFilterResultViewModel>> FilterByFuelTank(int minFuelTank, int maxFuelTank)
	{
		var response = new ServiceResponse<CarFilterResultViewModel>();

		try
		{
			var filteredCars = await _carService.FilterByFuelTank(minFuelTank, maxFuelTank);
			response.Data = new CarFilterResultViewModel { FilteredCars = filteredCars };
			response.Message = "Filtering by fuel tank successful.";
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = "Error occurred while filtering by fuel tank.";
			response.Description = ex.Message;
		}

		return response;
	}

	public async Task<ServiceResponse<CarFilterResultViewModel>> FilterByWheelType(WheelTypeEnum wheelType)
	{
		var response = new ServiceResponse<CarFilterResultViewModel>();

		try
		{
			var filteredCars = await _carService.FilterByWheelType(wheelType);
			response.Data = new CarFilterResultViewModel { FilteredCars = filteredCars };
			response.Message = "Filtering by wheel type successful.";
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = "Error occurred while filtering by wheel type.";
			response.Description = ex.Message;
		}

		return response;
	}

	public async Task<ServiceResponse<CarFilterResultViewModel>> FilterByLocation(string location)
	{
		var response = new ServiceResponse<CarFilterResultViewModel>();

		try
		{
			var filteredCars = await _carService.FilterByLocation(location);
			response.Data = new CarFilterResultViewModel { FilteredCars = filteredCars };
			response.Message = "Filtering by Location type successful.";
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = "Error occurred while filtering by Location.";
			response.Description = ex.Message;
		}

		return response;
	}

	public async Task<ServiceResponse<CarFilterResultViewModel>> FilterByAll(CarFilterParameters filterParameters)
	{
		var response = new ServiceResponse<CarFilterResultViewModel>();

		try
		{
			var filteredCars = await _carService.FilterByAll(filterParameters);
			response.Data = new CarFilterResultViewModel { FilteredCars = filteredCars };
			response.Message = "Filtering successful.";
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = "Error occurred while filtering ";
			response.Description = ex.Message;
		}

		return response;
	}
}
