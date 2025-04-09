using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.Helpers;
using Restaurant_Reservation_System_.Service.Dtos.OrderDtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;
using Restaurant_Reservation_System_.Service.Services;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Superadmin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
           _orderService = orderService;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 2)
        {
            var orders = _orderService.GetAll(); 

            var paginatedOrders =  PaginatedList<Order>.Create(orders, take , page);

            return View(paginatedOrders); 
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] OrderCreateDto orderCreateDto, ModelStateDictionary ModelState)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }


            await _orderService.CreateAsync(orderCreateDto,ModelState);
            return RedirectToAction("Index");

        }


        [HttpGet("admin/Order/detail")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                await _orderService.DetailAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // Slider tapılmadıqda
            }
        }
        public async Task<IActionResult> PrevStatus(int id)
        {
            await _orderService.PrevOrderStatusAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> NextStatus(int id)
        {
            await _orderService.NextOrderStatusAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CancelOrder(int id)
        {
            await _orderService.CancelOrderAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RepairOrder(int id)
        {
            await _orderService.RepairOrderAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
