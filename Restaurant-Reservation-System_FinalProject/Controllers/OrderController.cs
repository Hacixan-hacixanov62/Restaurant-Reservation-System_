using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.Service.Dtos.OrderDtos;
using Restaurant_Reservation_System_.Service.Services.IService;
using System.Security.Claims;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;       
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrderController(AppDbContext context, UserManager<AppUser> userManager,IOrderService orderService,IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _orderService = orderService;
            _mapper = mapper;
        }
  
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return BadRequest();

            var orders = await _context.Orders.Where(x => x.AppUserId == userId).OrderByDescending(x => x.Id).Include(x => x.OrderItems).ThenInclude(x => x.Product).ThenInclude(x => x.ProductImages).ToListAsync();
           var  ordersDto = _mapper.Map<List<OrderGetDto>>(orders);
            return View(ordersDto);            
        }   

 

    }
}
