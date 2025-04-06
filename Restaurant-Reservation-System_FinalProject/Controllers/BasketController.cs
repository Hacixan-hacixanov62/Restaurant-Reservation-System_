using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System_.Service.Extensions;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetCartSection()
        {         
            return PartialView("_cartSectionPartial");
        }

        public async Task<IActionResult> RemoveToCart(int id)
        {
            await _basketService.RemoveToCartAsync(id);

            string returnUrl = Request.GetReturnUrl();

            return Redirect(returnUrl);
        }

        public IActionResult RedirectForCart()  
        {
           
            return PartialView("_BasketPartial");
        }

        public async Task<IActionResult> AddToCart(int id, int count = 1)
        {
            await _basketService.AddToCartAsync(id, count);

            return RedirectToAction(nameof(RedirectForCart));
        }
        public async Task<IActionResult> DecreaseToCart(int id)
        {
            await _basketService.DecreaseToCartAsync(id);

            return RedirectToAction(nameof(RedirectForCart));
        }

    }
}
