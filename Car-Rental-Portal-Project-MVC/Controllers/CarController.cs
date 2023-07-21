using AutoMapper;
using Car_Rental_Portal_Project_MVC.Data;
using Car_Rental_Portal_Project_MVC.Models.Enums;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Car;
using Car_Rental_Portal_Project_MVC.Services;
using Car_Rental_Portal_Project_MVC.Services.Interfaces;
using identityStep;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_Portal_Project_MVC.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public CarController(ICarService carService, UserManager<IdentityUser> userManager, ApplicationDbContext db, IMapper mapper)
        {
            _carService = carService;
            _userManager = userManager;
            _db = db;
            _mapper = mapper;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CarPage(int id)
        {
            var car = _mapper.Map<GetCarViewModel>(
                await _db.ApplicationCars
                .Include(x => x.ApplicationUser)
                .FirstOrDefaultAsync(x => x.Id == id));
            return View(car);
        }
        [HttpGet]
        public async Task<IActionResult> AddCar()
        {
            List<SelectListItem> transmissionList = new List<SelectListItem>()
            {
                new SelectListItem() { Value = TransmissionEnum.Manual.ToString(), Text = TransmissionEnum.Manual.ToString() },
                new SelectListItem() { Value = TransmissionEnum.Automatic.ToString(), Text = TransmissionEnum.Automatic.ToString() }
            };

            List<SelectListItem> fuelTypeList = new List<SelectListItem>()
            {
                new SelectListItem() { Value = FuelTypeEnum.Diesel.ToString(), Text = FuelTypeEnum.Diesel.ToString() },
                new SelectListItem() { Value = FuelTypeEnum.Electric.ToString(), Text = FuelTypeEnum.Electric.ToString() },
                new SelectListItem() { Value = FuelTypeEnum.Gas.ToString(), Text = FuelTypeEnum.Gas.ToString() },
                new SelectListItem() { Value = FuelTypeEnum.Gasoline.ToString(), Text = FuelTypeEnum.Gasoline.ToString() },
                new SelectListItem() { Value = FuelTypeEnum.Hybrid.ToString(), Text = FuelTypeEnum.Hybrid.ToString() },
            };
            List<SelectListItem> wheelTypeList = new List<SelectListItem>()
            {
                new SelectListItem() { Value = WheelTypeEnum.Left.ToString(), Text = WheelTypeEnum.Left.ToString() },
                new SelectListItem() { Value = WheelTypeEnum.Right.ToString(), Text = WheelTypeEnum.Right.ToString() }
            };
            var UserId = await _userManager.GetUserIdAsync(await _userManager.GetUserAsync(User));
            var user = await _db.AplicationUsers.FirstOrDefaultAsync(x => x.Id == UserId);
            AddCarViewModel addCarViewModel = new AddCarViewModel()
            {
                TransmissionList = transmissionList,
                FuelTypeList = fuelTypeList,
                WheelTypeList = wheelTypeList,
                ApplicationUserId = UserId,
                ApplicationUser = user,
            };
            return View(addCarViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddCar(AddCarViewModel viewModel)
        {
            var gluser = await _userManager.GetUserAsync(User);
            var UserId = await _userManager.GetUserIdAsync(gluser);
            var user = await _db.AplicationUsers.FirstOrDefaultAsync(x => x.Id == UserId);
            if (user != null)
            {
                ModelState.Root.Children[13].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            }
            viewModel.ApplicationUserId = UserId;
            viewModel.ApplicationUser = user;


            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var response = await _carService.AddCar(viewModel);
            if (response.success)
            {
                return RedirectToAction("Profile", "Account");
            }

            ModelState.AddModelError("Error", $"Message: {response.Message} Description: {response.Description}");
            return View(viewModel);

        }
        [HttpPost]
        public async Task<IActionResult> FilteredCars(CarFilterViewModel viewmodel)
        {
            if (viewmodel.CarsToFilter.Count == 0)
            {
                viewmodel.CarsToFilter = _mapper.Map<List<GetCarViewModel>>(_db.ApplicationCars.ToList());
            }

            var response = await _carService.Filter(viewmodel);
            if (response.success)
            {
                return FilteredCars(response.Data);
            }
            return NotFound();
        }
        public IActionResult FilteredCars(List<GetCarViewModel> data)
        {
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCar(int id)
        {
            var car = _mapper.Map<UpdateCarViewModel>(await _db.ApplicationCars.FirstOrDefaultAsync(x => x.Id == id));
            return View(car);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCar(UpdateCarViewModel car)
        {
            var response = await _carService.UpdateCar(car);
            if (response.success)
            {
                return RedirectToAction("Profile", "Account");
            }

            ModelState.AddModelError("Error", $"Failed to update car: {response.Message}");
            return View(car);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var response = await _carService.DeleteCar(id);

            if (response.success)
            {
                return RedirectToAction("Profile", "Account");
            }
            else
            {
                ModelState.AddModelError("Error", response.Message);
                return View();

            }
        }
        [HttpGet]
        public async Task<IActionResult> HireCar(int id)
        {
            var model = new HireCarViewModel()
            {
                CarId = id,
                UserId = await _userManager.GetUserIdAsync(await _userManager.GetUserAsync(User)),
                HiredFrom = DateTime.Now,
                HiredTo = DateTime.Now,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> HireCar(HireCarViewModel viewmodel)
        {
            var response = await _carService.HireCar(viewmodel);
            if (response.success && response.Data != null)
            {
                return RedirectToAction("Profile", "Account");
            }
            else if (response.Message == "HIRED")
            {
                return View();
            }
            return NotFound();
        }
    }
}
