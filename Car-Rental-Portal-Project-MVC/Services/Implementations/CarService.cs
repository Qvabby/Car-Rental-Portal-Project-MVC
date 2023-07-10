using Car_Rental_Portal_Project_MVC.Models.Enums;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Car;
using Car_Rental_Portal_Project_MVC.Services.Interfaces;
using Car_Rental_Portal_Project_MVC.Services;
using Car_Rental_Portal_Project_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Car_Rental_Portal_Project_MVC.Data;
using AutoMapper;

public class CarService : ICarService
{
	private readonly ApplicationDbContext _dbContext;
	private readonly IMapper _mapper;
	public CarService(ApplicationDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}
	public async Task<ServiceResponse<CarViewModel>> AddCar(CarViewModel car)
	{

		var response = new ServiceResponse<CarViewModel>();

		try
		{
			// checks on the car data
			if (car == null)
			{
				response.success = false;
				response.Message = "Invalid car data.";
				return response;
			}

			// Map the car view model to the application car entity
			var newCar = _mapper.Map<ApplicationCar>(car);

			// Save the new car to the database using the injected ApplicationDbContext
			_dbContext.ApplicationCars.Add(newCar);
			await _dbContext.SaveChangesAsync();

			// Map the added car back to the view model
			var addedCarViewModel = _mapper.Map<CarViewModel>(newCar);

			response.Data = addedCarViewModel;
			response.Message = "Car added successfully.";
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = "An error occurred while adding the car: " + ex.Message;
		}

		return response;
	}

	public async Task<ServiceResponse<CarViewModel>> DeleteCar(int id)
	{
		var response = new ServiceResponse<CarViewModel>();

		try
		{		
			//Search Car by id
			var car = await _dbContext.ApplicationCars.FindAsync(id);
			
			// Check if the car exists
			if (car == null)
			{
				response.success = false;
				response.Message = "Car not found.";
				return response;
			}
			// Remove the car from the DataBase
			_dbContext.ApplicationCars.Remove(car);

			// Save the changes to the database
			await _dbContext.SaveChangesAsync();

			response.Message = "Car deleted successfully.";
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = "An error occurred while deleting the car: " + ex.Message;
			
		}

		return response;
	}

	public async Task<ServiceResponse<List<CarViewModel>>> GetAllCars()
	{
		var response = new ServiceResponse<List<CarViewModel>>();

		try
		{
			// Retrieve all cars from the database
			var cars = await _dbContext.ApplicationCars.ToListAsync();

			// Check if any cars were found
			if (cars == null || cars.Count == 0)
			{
				response.success = false;
				response.Message = "No cars found.";
				return response;
			}

			// Map the list of cars to a list of CarViewModel
			var carViewModels = _mapper.Map<List<CarViewModel>>(cars);

			response.Data = carViewModels;
			response.Message = "Cars retrieved successfully.";
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = "An error occurred while retrieving cars: " + ex.Message;
		}

		return response;
	}


	public async Task<ServiceResponse<CarViewModel>> GetCarById(int id)
	{
		var response = new ServiceResponse<CarViewModel>();

		try
		{
			// Find By Id
			var car = await _dbContext.ApplicationCars.FindAsync(id);

			if (car != null)
			{
				// Map the car entity to CarViewModel
				var carViewModel = _mapper.Map<CarViewModel>(car);
				response.Data = carViewModel;
				response.Message = "Car retrieved successfully.";
			}
			else
			{
				response.success = false;
				response.Message = "Car not found.";
			}
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = $"An error occurred while retrieving the car: {ex.Message}";
		}

		return response;
	}


	public async Task<ServiceResponse<CarViewModel>> UpdateCar(CarViewModel car)
	{
		var response = new ServiceResponse<CarViewModel>();

		try
		{
			//check on the car 
			if (car == null)
			{
				response.success = false;
				response.Message = "Invalid car data.";
				return response;
			}

			// Retrieve the existing car from the database
			var existingCar = await _dbContext.ApplicationCars.FindAsync(car.Id);

			if (existingCar == null)
			{
				response.success = false;
				response.Message = "Car not found.";
				return response;
			}

			// Update the properties of the existing car entity with the new values from the view model
			existingCar.Manufacturer = car.Manufacturer;
			existingCar.Model = car.Model;
			existingCar.Year = car.Year;
			existingCar.Price = car.Price;
			existingCar.Engine = car.Engine;
			existingCar.Transmission = car.Transmission;
			existingCar.FuelType = car.FuelType;
			existingCar.FuelTank = car.FuelTank;
			existingCar.WheelType = car.WheelType;
			existingCar.Location = car.Location;
			existingCar.PeopleAmount = car.PeopleAmount;
			existingCar.Rented = car.Rented;
			existingCar.RentedByUserId = car.RentedByUserId;

			// Save the changes to the database
			await _dbContext.SaveChangesAsync();

			// Map the updated car entity back to the view model
			var updatedCarViewModel = _mapper.Map<CarViewModel>(existingCar);

			response.Data = updatedCarViewModel;
			response.Message = "Car updated successfully.";
		}
		catch (Exception ex)
		{
			response.success = false;
			response.Message = $"An error occurred while updating the car: {ex.Message}";
		}

		return response;
	}

}
