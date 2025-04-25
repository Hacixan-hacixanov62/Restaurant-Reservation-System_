using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Helpers;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Superadmin")]
    public class ReservationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IReservationService _reservationServcie;
        public ReservationController(AppDbContext context,IReservationService reservationService)
        {            
            _context = context;
            _reservationServcie = reservationService;   
        }


        public async Task<IActionResult> Index(int page = 1, int take = 8)
        {

            try
            {
                var categories = _context.Reservations
                    .Include(c => c.Table)                 
                    .AsQueryable();
                PaginatedList<Reservation> paginatedList = PaginatedList<Reservation>.Create(categories, take, page);
                return View(paginatedList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public async Task<IActionResult> RepairOrEnd(int id)
        {
             await _reservationServcie.RepairOrEnd(id);
            return RedirectToAction("Index");
        }

    }
}
