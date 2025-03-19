using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_FinalProject.ViewModels;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVm homeVm = new();
            homeVm.Sliders = _context.Sliders.ToList();

            return View(homeVm);
        }
    }
}
