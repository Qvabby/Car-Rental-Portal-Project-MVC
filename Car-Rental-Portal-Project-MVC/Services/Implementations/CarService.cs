using Car_Rental_Portal_Project_MVC.Models.Enums;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Car;
using Car_Rental_Portal_Project_MVC.Services.Interfaces;
using Car_Rental_Portal_Project_MVC.Services;
using Car_Rental_Portal_Project_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Car_Rental_Portal_Project_MVC.Data;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Google.Apis.Gmail.v1.Data;

public class CarService : ICarService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;
    public CarService(ApplicationDbContext dbContext, IMapper mapper, UserManager<IdentityUser> userManager)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _userManager = userManager;
    }
    public async Task<ServiceResponse<GetCarViewModel>> AddCar(AddCarViewModel car)
    {
        var response = new ServiceResponse<GetCarViewModel>();
        try
        {
            // checks on the car data
            if (car == null)
            {
                response.success = false;
                response.Message = "Invalid car model.";
                return response;
            }

            // Map the car view model to the application car entity
            var newCar = _mapper.Map<ApplicationCar>(car);

            // Save the new car to the database using the injected ApplicationDbContext
            await _dbContext.AddAsync(newCar);
            await _dbContext.SaveChangesAsync();

            // Map the added car back to the view model
            var addedCarViewModel = _mapper.Map<GetCarViewModel>(newCar);

            response.Data = addedCarViewModel;
            response.Message = "Car added successfully.";
            return response;
        }
        catch (Exception ex)
        {
            response.success = false;
            response.Message = "An error occurred while adding the car: " + ex.Message;
            response.Description = ex.InnerException.ToString();
            return response;
        }
    }

    public async Task<ServiceResponse<GetCarViewModel>> DeleteCar(int id)
    {
        var response = new ServiceResponse<GetCarViewModel>();

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
            return response;
        }
        catch (Exception ex)
        {
            response.success = false;
            response.Message = "An error occurred while deleting the car: " + ex.Message;
            return response;
        }

    }

    public async Task<ServiceResponse<List<GetCarViewModel>>> Filter(CarFilterViewModel viewModel)
    {
        var response = new ServiceResponse<List<GetCarViewModel>>();

        try
        {
            var query = viewModel.CarsToFilter.AsQueryable();

            if (!string.IsNullOrEmpty(viewModel.Manufacturer))
            {
                query = query.Where(x => x.Manufacturer.Contains(viewModel.Manufacturer));
            }

            if (!string.IsNullOrEmpty(viewModel.Model))
            {
                query = query.Where(x => x.Model.Contains(viewModel.Model));
            }

            if (viewModel.Year > 0)
            {
                query = query.Where(x => x.Year == viewModel.Year);
            }

            if (viewModel.Price > 0)
            {
                query = query.Where(x => x.Price <= viewModel.Price);
            }

            if (viewModel.Engine > 0)
            {
                query = query.Where(x => x.Engine == viewModel.Engine);
            }

            if (viewModel.Transmission != 0)
            {
                query = query.Where(x => x.Transmission == viewModel.Transmission);
            }

            if (viewModel.FuelType != 0)
            {
                query = query.Where(x => x.FuelType == viewModel.FuelType);
            }

            if (viewModel.FuelTank > 0)
            {
                query = query.Where(x => x.FuelTank == viewModel.FuelTank);
            }

            if (viewModel.WheelType != 0)
            {
                query = query.Where(x => x.WheelType == viewModel.WheelType);
            }

            if (!string.IsNullOrEmpty(viewModel.Location))
            {
                query = query.Where(x => x.Location.Contains(viewModel.Location));
            }

            if (viewModel.PeopleAmount > 0)
            {
                query = query.Where(x => x.PeopleAmount == viewModel.PeopleAmount);
            }

            var cars = await query.ToListAsync();

            if (cars == null || cars.Count == 0)
            {
                response.Data = null;
                response.Message = "NotFound";
                return response;
            }

            response.Data = _mapper.Map<List<GetCarViewModel>>(cars);
            response.Message = "Cars retrieved successfully.";
            return response;
        }
        catch (Exception ex)
        {
            response.success = false;
            response.Message = "An error occurred while retrieving cars: " + ex.Message;
            return response;
        }
    }



    public async Task<ServiceResponse<GetCarViewModel>> GetCarById(int id)
    {
        var response = new ServiceResponse<GetCarViewModel>();

        try
        {
            // Find By Id
            var car = await _dbContext.ApplicationCars.FindAsync(id);

            if (car != null)
            {
                // Map the car entity to CarViewModel
                var carViewModel = _mapper.Map<GetCarViewModel>(car);
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

    public async Task<ServiceResponse<GetCarViewModel>> UpdateCar(UpdateCarViewModel car)
    {
        var response = new ServiceResponse<GetCarViewModel>();

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
            _mapper.Map(car, existingCar);
            #region NoMapUpdate
            //existingCar.Manufacturer = car.Manufacturer;
            //existingCar.Model = car.Model;
            //existingCar.Year = car.Year;
            //existingCar.Price = car.Price;
            //existingCar.Engine = car.Engine;
            //existingCar.Transmission = car.Transmission;
            //existingCar.FuelType = car.FuelType;
            //existingCar.FuelTank = car.FuelTank;
            //existingCar.WheelType = car.WheelType;
            //existingCar.Location = car.Location;
            //existingCar.PeopleAmount = car.PeopleAmount;
            //existingCar.Rented = car.Rented;
            //existingCar.RentedByUserId = car.RentedByUserId;
            #endregion
            // Save the changes to the database
            await _dbContext.SaveChangesAsync();

            // Map the updated car entity back to the view model
            var updatedCarViewModel = _mapper.Map<GetCarViewModel>(existingCar);

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
