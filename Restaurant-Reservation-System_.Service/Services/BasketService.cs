using AutoMapper;
using Azure;
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
using Restaurant_Reservation_System_.Service.ViewModels.BasketVM;
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

        public async Task<CartGetDto> GetCartAsync()
        {

            if (_checkAuthorized())
            {
                var userId = _getUserId();

              

                var cartItems = await _basketRepository.GetFilter(x => x.AppUserId == userId,
                               x => x.Include(x => x.Product)
                                          .Include(x => x.Product.Category)
                                          .Include(x => x.Product.ProductImages)).ToListAsync() ?? new List<CartItem>();

                var dtos = _mapper.Map<List<CartItemDto>>(cartItems);

                decimal subtotal = dtos.Sum(x => x.Count * x.Product.Price);
                decimal discount = subtotal * 0.20m;
                decimal total = subtotal - discount;

                var cartDto = new CartGetDto()
                {
                    Items = dtos,
                    Count = dtos.Count,
                    Subtotal = subtotal,
                    Total = total,
                    Discount = discount,
                };

                return cartDto;
            }
            else
            {
                var cartItems = _readCartFromCookie();

                var dtos = _mapper.Map<List<CartItemDto>>(cartItems);

                foreach (var dto in dtos)
                {
                    var product = await _productService.GetByIdAsync(dto.ProductId);

                    if (product is null)
                    {
                        dtos.Remove(dto);
                        continue;
                    }

                    dto.Product = product;
                }

                decimal totalPrice = dtos.Sum(x => (x.Count * x.Product.Price));
                var cartDto = new CartGetDto()
                {
                    Items = dtos,
                    Count = dtos.Count,
                    Subtotal = totalPrice,
                    Total = totalPrice,
                    Discount = 0,
                };

                return cartDto;
            }
        }

        public Task DeleteBasket(int id)
        {
            CartGetDto vm = new CartGetDto();
            var userId = _getUserId();

            if (userId != null)
            {

                var basketItem = _context.CartItems.FirstOrDefault(x => x.AppUserId == userId && x.ProductId == id);

                if (basketItem == null)
                    throw new NotFoundException("Basket NotFound");
                else
                {
                    _context.CartItems.Remove(basketItem);
                    _context.SaveChanges();
                }

                var basketItems = _context.CartItems.Include(x => x.Product).ThenInclude(p => p.ProductImages).Where(x => x.AppUserId == userId).ToList();
                foreach (var item in basketItems)
                {

                    CartItemDto basketItemVM = new CartItemDto
                    {
                        Count = item.Count,
                        Product = _mapper.Map<ProductGetDto>(item.Product)
                    };

                    vm.Items.Add(basketItemVM);

                    vm.Total += (basketItemVM.Product.Discount > 0 ? basketItemVM.Product.Discount : basketItemVM.Product.Discount) * basketItemVM.Count;

                }


            }
            else
            {
                //var cartItems = _readCartFromCookie();

                //List<BasketCookieVM> cookieItems = null;

                //if (cartItems == null)
                //    cookieItems = new List<CartItemCreateDto>();
                //else
                //    cookieItems = JsonConvert.DeserializeObject<List<CartItemCreateDto>>(cartItems);

                //var existItem = cookieItems.FirstOrDefault(x => x.ProductId == id);

                //if (existItem == null)
                //    throw new NotFoundException();
                //else
                //    cookieItems.Remove(existItem);

                //Response.Append("basket", JsonConvert.SerializeObject(cookieItems));

                // Guest user (non-authorized)
                var cartItems = _readCartFromCookie(); // Read from cookie

                var existItem = cartItems.FirstOrDefault(x => x.ProductId == id);
                if (existItem == null)
                    throw new NotFoundException();

                cartItems.Remove(existItem);
                _writeCartInCookie(cartItems); // Update cookie

                foreach (var cItem in cartItems)
                {
                    CartItemDto basketItemVM = new CartItemDto
                    {
                        Count = cItem.Count,
                        Product = _mapper.Map<ProductGetDto>(cItem.ProductId)
                    };

                    vm.Items.Add(basketItemVM);

                    vm.Total += (basketItemVM.Product.Discount > 0 ? basketItemVM.Product.Discount : basketItemVM.Product.Discount) * basketItemVM.Count;

                }


            }
            return Task.CompletedTask;

        }
    }
}
