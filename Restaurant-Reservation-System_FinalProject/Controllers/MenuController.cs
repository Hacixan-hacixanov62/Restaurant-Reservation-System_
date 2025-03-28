using Microsoft.AspNetCore.Mvc;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
