using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Portal_Project_MVC.Controllers
{
	public class CarController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
