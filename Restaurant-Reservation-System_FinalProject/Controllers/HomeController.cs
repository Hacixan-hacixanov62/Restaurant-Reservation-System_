using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.Service.Dtos.CommanDtos;
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
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(x => x.ProductImages).Include(x => x.Category).Take(12).ToListAsync();
            var categories = await _context.Categories.Where(x => x.Products.Count > 0).Take(10).ToListAsync();
            var sliders = await _context.Sliders.ToListAsync();
           // var topComments = await _context.Comments.OrderByDescending(x => x.Rating).Include(x => x.AppUser).Take(3).ToListAsync();
            var about = await _context.Abouts.ToListAsync();

            HomeVm homeVm = new HomeVm()
            {
                Abouts = about,
                Products = products,
                Categories = categories,
                Sliders = sliders,
               // Comments = topComments
            };
       

            return View(homeVm);
        }
        [HttpPost]
        public async Task<IActionResult> AddSubscriber(SubscribeCreateDto dto)
        {
            var result = await _subscribeService.CreateAsync(dto, ModelState);

            string returnUrl = Request.GetReturnUrl();

            return Redirect(returnUrl);
        }

        public IActionResult Error(string? json)
        {
            if (!string.IsNullOrEmpty(json))
            {

                string decodedJson = Uri.UnescapeDataString(json);

                var dto = JsonConvert.DeserializeObject<ErrorDto>(decodedJson);
                return View(dto);
            }

            return View(new ErrorDto
            {
                StatusCode = 500,
                Message = "Gözlənilməyən xəta baş verdi."
            });
        }
    }
}
