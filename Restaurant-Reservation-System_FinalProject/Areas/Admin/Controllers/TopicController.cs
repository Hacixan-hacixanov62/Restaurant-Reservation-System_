using Microsoft.AspNetCore.Mvc;

namespace Restaurant_Reservation_System_FinalProject.Areas.Admin.Controllers
{
    public class TopicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
