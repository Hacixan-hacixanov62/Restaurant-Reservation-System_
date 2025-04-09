using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Service.Dtos.BasketDtos;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_FinalProject.ViewCommponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly IBasketService _basketService;
        public HeaderViewComponent(IBasketService basketService)
        {            
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           var basket = _basketService.GetUserBasketItem();

           CartGetDto cartGetDto =  new CartGetDto 
           { 
               Subtotal =basket.Subtotal,
               Total = basket.Total,
               Count = basket.Count,
               Discount = basket.Discount

           };

            return View(cartGetDto);
        }
    }
}
