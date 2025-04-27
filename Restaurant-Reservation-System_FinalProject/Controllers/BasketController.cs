using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.Service.Dtos.OrderDtos;
using Restaurant_Reservation_System_.Service.Extensions;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.ViewModels.BasketVM;
using Stripe;
using System.Security.Claims;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        public BasketController(IBasketService basketService, AppDbContext context, UserManager<AppUser> userManager)
        {
            _basketService = basketService;
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var cartItems = await GetBasketAsync();
            return View(cartItems); 
        }

        public async Task<IActionResult> GetCartSection()
        {  
            var cart = _basketService.GetCartAsync();
            return PartialView("_cartSectionPartial",cart);
        }

        public async Task<IActionResult> RemoveToCart(int id)
        {
            await _basketService.RemoveToCartAsync(id);

            string returnUrl = Request.GetReturnUrl();

            return Redirect(returnUrl);
        }

      
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

        public async Task<IActionResult> DeleteBasket(int id)
        {
            await _basketService.DeleteBasket(id);
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


        public async Task<IActionResult> Checkout()
        {
            var basketItems = await GetBasketAsync();
            decimal total = 0;

            basketItems.ForEach(x =>
            {
                total += x.Product.Price * x.Count;
            });


            ViewBag.Total = total;

            return View(basketItems);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Checkout(OrderCreateDto dto)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return Unauthorized();


            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return BadRequest();

            var basketItems = await GetBasketAsync();

            decimal total = 0;

            basketItems.ForEach(bi =>
            {
                total += bi.Count * bi.Product.Price;
            });

            ViewBag.Total = total;



            // Stripe-in kodd hissesi
            //========================================================================================
          
            var optionCust = new CustomerCreateOptions
            {
                Email = dto.stripeEmail,
                Name = user.FullName,
                Phone = "994 051 516"
            };
            var serviceCust = new CustomerService();
            Customer customer = serviceCust.Create(optionCust);

            total = total * 100;    
            var optionsCharge = new ChargeCreateOptions  // Odenisin Melumatlari saxlanilir
            {

                Amount = (long)total, //Dollari cente cevirir
                Currency = "USD",
                Description = "Dannys Restourant Order",
                Source = dto.stripeToken,
                ReceiptEmail = "hajikhanih@code.edu.az"


            };
            var serviceCharge = new ChargeService();
            Charge charge = serviceCharge.Create(optionsCharge);

            //===========================================================================
            Order order = new()
            {
                AppUser = user,
                Status = false,
                OrderItems = new List<OrderItem>(),
                PhoneNumber = dto.PhoneNumber,
                City = dto.City,
                Apartment = dto.Apartment,
                Street = dto.Street,
                Email = user.Email,
                Name = user.FullName,
                CompanyName = dto.CompanyName,
                Surname = dto.Surname
            };

            foreach (var bItem in basketItems)
            {
                OrderItem orderItem = new()
                {
                    Order = order,
                    Product = bItem.Product,
                    Count = bItem.Count,
                    TotalPrice = bItem.Product.Price,    

                };
                order.OrderItems.Add(orderItem);

                _context.CartItems.Remove(bItem);
            }
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", new { orderId = order.Id });

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

        public IActionResult GetBasket()
        {
            var basket = _basketService.GetUserBasketItem();
            return PartialView("_BasketPartial", basket.Items);
        }
    }
}
