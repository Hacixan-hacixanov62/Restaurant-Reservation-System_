using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.Service.UI.VM;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class MenuController : Controller
    {

        private readonly AppDbContext _context;
        public MenuController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            MenuVm menuVm = new();
            menuVm.Products =_context.Products.ToList();
            return View(menuVm);
        }
    }
}
