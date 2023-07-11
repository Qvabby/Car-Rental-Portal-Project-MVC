using Car_Rental_Portal_Project_MVC.Models.Enums;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Car;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Car_Rental_Portal_Project_MVC.Controllers
{
	public class CarController : Controller
	{
		public IActionResult CarPage(GetCarViewModel car)
		{
			return View(car);
		}

		[HttpGet]
		public IActionResult AddCar()
		{
            
            return View();
		}
        [HttpPost]
        public IActionResult AddCar(AddCarViewModel model)
        {
			if (!ModelState.IsValid)
			{
                return View(model);
            }

			return View();
            
        }
    }
}
