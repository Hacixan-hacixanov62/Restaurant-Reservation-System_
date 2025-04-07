using Microsoft.AspNetCore.Http;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.Service.Dtos.BasketDtos;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.UI.Services.IService;

namespace Restaurant_Reservation_System_FinalProject.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext _httpContext;
        private readonly IBasketService _basketService;
        public LayoutService(AppDbContext context, IHttpContextAccessor httpContextAccessor,IBasketService basketService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _httpContext = _httpContextAccessor.HttpContext;
            _basketService = basketService;
        }

    }
}
