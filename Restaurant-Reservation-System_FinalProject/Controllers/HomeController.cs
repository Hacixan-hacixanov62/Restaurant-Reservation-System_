using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.Service.Dtos.SubscribeDtos;
using Restaurant_Reservation_System_.Service.Extensions;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.UI.VM;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISubscribeService _subscribeService;
        public HomeController(AppDbContext context,ISubscribeService subscribeService)
        {
            _context = context;
            _subscribeService = subscribeService;
        }
        public IActionResult Index()
        {
            HomeVm homeVm = new();
            homeVm.Sliders = _context.Sliders.ToList();
            homeVm.Abouts = _context.Abouts.ToList();
            homeVm.Categories = _context.Categories.ToList();
            homeVm.Products = _context.Products.ToList();   
            

            return View(homeVm);
        }

        public async Task<IActionResult> AddSubscriber(SubscribeCreateDto dto)
        {
            var result = await _subscribeService.CreateAsync(dto, ModelState);

            string returnUrl = Request.GetReturnUrl();

            return Redirect(returnUrl);
        }
    }
}
