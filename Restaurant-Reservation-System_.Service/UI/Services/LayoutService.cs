using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.Service.Dtos.BasketDtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.UI.Services.IService;
using Restaurant_Reservation_System_.Service.ViewModels.BasketVM;
using System.Security.Claims;
using System.Text.Json;

namespace Restaurant_Reservation_System_FinalProject.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LayoutService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public CartGetDto GetUserBasketItem()
        {
            CartGetDto cartGetDto = new CartGetDto();

            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var items = _context.CartItems
                    .Include(x => x.Product)
                    .ThenInclude(x => x.ProductImages.Where(x => x.IsMain == true))
                    .Where(x => x.AppUserId == userId).ToList();

                foreach (var bi in items)
                {

                    CartItemDto basketItemVM = new CartItemDto()
                    {
                        Count = bi.Count,
                        MainImage=bi.Product.ProductImages.FirstOrDefault(m=>m.IsMain==true).Url,
                        Product = new ProductGetDto
                        {
                            Id = bi.Product.Id,
                            Name = bi.Product.Name,
                            Price = bi.Product.Price,
                            Discount = bi.Product.Discount,
                        }
                    };
                   // ProductGetDto dto = _mapper.Map<ProductGetDto>(bi.Product);

                    cartGetDto.Items.Add(basketItemVM);
                    cartGetDto.Total += (basketItemVM.Product.Discount > 0 ? basketItemVM.Product.Discount : basketItemVM.Product.Price) * basketItemVM.Count;

                }

            }
            else
            {
                var basketStr = _httpContextAccessor.HttpContext.Request.Cookies["RestaurantCart"];

                List<CartItemCreateDto> cookieItems = null;

                if (basketStr != null)
                    cookieItems = JsonConvert.DeserializeObject<List<CartItemCreateDto>>(basketStr);
                else
                    cookieItems = new List<CartItemCreateDto>();


                try
                {
                    foreach (var cItem in cookieItems)
                    {
                        var product = _context.Products.Include(x => x.ProductImages.Where(x => x.IsMain == true)).FirstOrDefault(x => x.Id == cItem.ProductId);

                        if (product is null)
                            continue;

                        CartItemDto basketItemVM = new CartItemDto()
                        {
                            
                            Count = cItem.Count,
                            MainImage = product.ProductImages.FirstOrDefault(x => x.IsMain == true)?.Url,
                            Product = new ProductGetDto
                            {
                                Id = product.Id,
                                Name = product.Name,
                                Price = product.Price,
                                MainImage = product.ProductImages.FirstOrDefault(x => x.IsMain == true)?.Url,
                            }
                        };


                        cartGetDto.Items.Add(basketItemVM);
                        cartGetDto.Total += (basketItemVM.Product.Discount > 0 ? basketItemVM.Product.Discount : basketItemVM.Product.Price) * basketItemVM.Count;

                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }



            }

            return cartGetDto;
        }
    }
}
