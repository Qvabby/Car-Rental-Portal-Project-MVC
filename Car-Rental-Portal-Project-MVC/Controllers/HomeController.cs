using AutoMapper;
using Car_Rental_Portal_Project_MVC.Data;
using Car_Rental_Portal_Project_MVC.Models.ViewModels;
using Car_Rental_Portal_Project_MVC.Models.ViewModels.Car;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Car_Rental_Portal_Project_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IActionResult> Index(List<GetCarViewModel> CarsFromDb)
        {
            CarsFromDb = _mapper.Map<List<GetCarViewModel>>(await _db.ApplicationCars.ToListAsync());
            return View(CarsFromDb);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}