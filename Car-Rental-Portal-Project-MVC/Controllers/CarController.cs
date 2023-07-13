using Car_Rental_Portal_Project_MVC.Data;
using Car_Rental_Portal_Project_MVC.Models.Enums;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Car;
using Car_Rental_Portal_Project_MVC.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_Portal_Project_MVC.Controllers
{
	public class CarController : Controller
	{
        private readonly ICarService _carService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;
        public CarController(ICarService carService, UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _carService = carService;
            _userManager = userManager;
            _db = db;
        }
        public IActionResult CarPage(GetCarViewModel car)
		{
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
                UserId = UserId,
                ApplicationUser = user
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
                ModelState.Root.Children[12].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            }
            viewModel.UserId = UserId;
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

            ModelState.AddModelError("Error",$"Message: {response.Message} Description: {response.Description}");
            return View(viewModel);
            
        }
    }
}
