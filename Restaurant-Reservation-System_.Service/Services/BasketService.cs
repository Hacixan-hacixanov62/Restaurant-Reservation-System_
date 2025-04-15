using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Core.Enums;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Localizers;
using Restaurant_Reservation_System_.DataAccess.Repositories;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Dtos.BasketDtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;
using Restaurant_Reservation_System_.Service.Exceptions;
using Restaurant_Reservation_System_.Service.Extensions;
using Restaurant_Reservation_System_.Service.Services.IService;
using System.Security.Claims;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class BasketService:IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private const string BASKET_KEY = "RestaurantCart";
        public BasketService(IBasketRepository basketRepository, IMapper mapper, IHttpContextAccessor contextAccessor, IProductService productService, AppDbContext context)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _productService = productService;
            _context = context;
        }

        public async Task<bool> AddToCartAsync(int id, int count = 1)
        {
            var isExistProduct = await _productService.IsExistAsync(id);

            if (isExistProduct is false)
                throw new NotFoundException("Notfound Basket Product");


            if (count < 1)
                count = 1;

            if (_checkAuthorized())
            {
                string userId = _getUserId();

                var existCartItem = await _basketRepository.GetAsync(x => x.ProductId == id && x.AppUserId == userId);

                if (existCartItem is { })
                {
                    existCartItem.Count += count;

                    _basketRepository.Update(existCartItem);
                    await _basketRepository.SaveChangesAsync();

                    return true;
                }

                CartItem cartItem = new() { AppUserId = userId, ProductId = id, Count = count };

                await _basketRepository.CreateAsync(cartItem);
                await _basketRepository.SaveChangesAsync();

                return true;
            }
            else
            {
                var cartItems = _readCartFromCookie();

                var existItem = cartItems.FirstOrDefault(x => x.ProductId == id);

                if (existItem is { })
                    existItem.Count += count;
                else
                {
                    CartItem newCartItem = new() { ProductId = id, Count = count };

                    cartItems.Add(newCartItem);
                }

                _writeCartInCookie(cartItems);

                return true;
            }
        }

        public async Task ClearCartAsync()
        {
            if (!_checkAuthorized())
            {
                _writeCartInCookie(new());
                return;
            }

            string userId = _getUserId();

            var cartItems = await _basketRepository.GetFilter(x => x.AppUserId == userId).ToListAsync();

            foreach (var cartItem in cartItems)
            {
                _basketRepository.Delete(cartItem);
            }

            await _basketRepository.SaveChangesAsync();
        }

        public async Task<bool> DecreaseToCartAsync(int id)
        {
            var isExistProduct = await _productService.IsExistAsync(id);

            if (isExistProduct is false)
                throw new NotFoundException("Notfound Product");

            if (_checkAuthorized())
            {
                string userId = _getUserId();

                var existCartItem = await _basketRepository.GetAsync(x => x.ProductId == id && x.AppUserId == userId);

                if (existCartItem is null)
                    throw new NotFoundException("Notfound Product");

                if (existCartItem.Count <= 1)
                    return true;

                existCartItem.Count--;

                _basketRepository.Update(existCartItem);
                await _basketRepository.SaveChangesAsync();

                return true;

            }
            else
            {
                var cartItems = _readCartFromCookie();

                var existItem = cartItems.FirstOrDefault(x => x.ProductId == id);

                if (existItem is null)
                    throw new NotFoundException("Notfound Product");

                if (existItem.Count <= 1)
                    return true;

                existItem.Count--;

                _writeCartInCookie(cartItems);

                return true;
            }
        }

        public async Task RemoveToCartAsync(int id)
        {
            var isExistProduct = await _productService.IsExistAsync(id);

            if (isExistProduct is false)
                throw new NotFoundException("Notfound Product");

            if (_checkAuthorized())
            {
                string userId = _getUserId();

                var existCartItem = await _basketRepository.GetAsync(x => x.ProductId == id && x.AppUserId == userId);

                if (existCartItem is null)
                    throw new NotFoundException("Notfound Product");

                _basketRepository.Delete(existCartItem);
                await _basketRepository.SaveChangesAsync();
            }
            else
            {
                List<CartItem> cartItems = _readCartFromCookie();

                var existItem = cartItems.FirstOrDefault(x => x.ProductId == id);

                if (existItem is null)
                    throw new NotFoundException("Notfound Product");

                cartItems.Remove(existItem);

                _writeCartInCookie(cartItems);
            }
        }

        private List<CartItem> _readCartFromCookie()
        {
            string json = _contextAccessor.HttpContext?.Request.Cookies[BASKET_KEY] ?? "";

            var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(json) ?? new();
            return cartItems;
        }

        private void _writeCartInCookie(List<CartItem> cartItems)
        {
            string newJson = JsonConvert.SerializeObject(cartItems);

            _contextAccessor.HttpContext?.Response.Cookies.Append(BASKET_KEY, newJson);
        }


        private string _getUserId()
        {
            return _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        }


        private bool _checkAuthorized()
        {
            return _contextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }


        public Task RemoveToBasket(int id, string? returnUrl)
        {
            throw new NotImplementedException();
        }

        public Task EditBasketItem(int id, int count)
        {
            throw new NotImplementedException();
        }

        public Task AddToBasket(int id, string? returnUrl, int count = 1, int page = 1)
        {
            throw new NotImplementedException();
        }

        public CartGetDto GetUserBasketItem()
        {
            CartGetDto cartGetDto = new CartGetDto();

            if (_checkAuthorized())
            {
                var userId = _getUserId();

                var items = _context.CartItems
                    .Include(x => x.Product)
                    .ThenInclude(x => x.ProductImages.Where(x => x.IsMain == true))
                    .Where(x => x.AppUserId == userId).ToList();

                foreach (var bi in items)
                {

                    CartItemDto basketItemVM = new CartItemDto()
                    {
                        Count = bi.Count,

                        Product = new ProductGetDto
                        {
                            Id = bi.Product.Id,
                            Name = bi.Product.Name,
                            Price = bi.Product.Price,
                            Discount = bi.Product.Discount,
                        }
                    };
                    ProductGetDto dto = _mapper.Map<ProductGetDto>(bi.Product);

                    cartGetDto.Items.Add(basketItemVM);
                    cartGetDto.Total += (basketItemVM.Product.Discount > 0 ? basketItemVM.Product.Discount : basketItemVM.Product.Price) * basketItemVM.Count;

                }

            }
            else
            {
                var basketStr = _readCartFromCookie();

                List<CartItem> cookieItems = null;

                if (basketStr != null)
                    cookieItems = _readCartFromCookie();
                else
                    cookieItems = _readCartFromCookie();


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
