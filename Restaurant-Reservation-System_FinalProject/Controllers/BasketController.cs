using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.Service.Extensions;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.ViewModels.BasketVM;
using System.Security.Claims;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly AppDbContext _context;
        public BasketController(IBasketService basketService,AppDbContext context)
        {
            _basketService = basketService;
            _context = context;
            
        }
        public async Task<IActionResult> Index()
        {
            var cartItems = await GetBasketAsync();
            return View(cartItems);
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

        //public IActionResult RedirectForCart()  
        //{
           
        //    return PartialView("_BasketModalPartial");
        //}
        
        public async Task<IActionResult> AddToCart(int id, int count = 1)
        {
            await _basketService.AddToCartAsync(id, count);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DecreaseToCart(int id)
        {
            await _basketService.DecreaseToCartAsync(id);

            return RedirectToAction(nameof(Index));
        }



        private List<CartItem> _getBasket()
        {
            List<CartItem> basketItems = new();
            if (Request.Cookies["RestaurantCart"] != null)
            {
                basketItems = JsonConvert.DeserializeObject<List<CartItem>>(Request.Cookies["RestaurantCart"]) ?? new();
            }

            return basketItems;
        }


        private async Task<List<CartItem>> GetBasketAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var basketItems = await  _context.CartItems.Include(x => x.Product).ThenInclude(x => x.ProductImages).Where(x => x.AppUserId == userId).ToListAsync();
                return basketItems;
            }

            var basktItms = _getBasket();
            foreach (var item in basktItms)
            {
                var product = await _context.Products.Include(x => x.ProductImages).FirstOrDefaultAsync(x => x.Id == item.ProductId);
                item.Product = product;


            }

            return basktItms;
        }
    }
}
