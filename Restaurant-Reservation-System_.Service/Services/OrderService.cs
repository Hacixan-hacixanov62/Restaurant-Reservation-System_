using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Core.Enums;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Dtos.OrderDtos;
using Restaurant_Reservation_System_.Service.Exceptions;
using Restaurant_Reservation_System_.Service.Services.IService;
using System.Security.Claims;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IBasketService _basketService;
        private readonly IHttpContextAccessor _contextAccessor;
        public OrderService(IOrderRepository orderRepository,IMapper mapper,IBasketService basketService, IHttpContextAccessor contextAccessor)
        {            
            _orderRepository = orderRepository;
            _mapper = mapper;
            _basketService = basketService;
            _contextAccessor = contextAccessor;
        } 

        public async Task CancelOrderAsync(int id)
        {
            var order = await _orderRepository.GetAsync(id, x => x.Include(x => x.OrderItems));

            if (order is null)
                throw new NotFoundException("NotFound Order");

           order.IsCanceled = !order.IsCanceled;

            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();
        }
        
        public async Task<bool> CreateAsync(OrderCreateDto dto, ModelStateDictionary ModelState)
        {
            if (!ModelState.IsValid)
                return false;
           
            var order = _mapper.Map<Order>(dto);

            string? userName = _contextAccessor.HttpContext?.User.Identity?.Name;

            // `CreatedBy` və `UpdatedBy` doldurulur
            order.CreatedBy = userName ?? "Unknown";
            order.UpdatedBy = userName ?? "Unknown";
            order.CreatedAt = DateTime.UtcNow;
            //var status = await _statusService.GetFirstAsync();
            //order.StatusId = status.Id;

            string userId = _getUserId()!;
            order.AppUserId = userId;

            await _orderRepository.CreateAsync(order);
            await _orderRepository.SaveChangesAsync();

            //foreach (var item in dto.OrderItems)
            //    await _productService.IncreaseSalesCountAsync(item.ProductId, item.Count);

            return true;
        }

        private string? _getUserId()
        {
            return _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _orderRepository.GetAsync(id);

            if (order is null)
                throw new NotFoundException("NotFound Order");

          await  _orderRepository.Delete(order);
            await _orderRepository.SaveChangesAsync();
        }

        public Task<List<OrderGetDto>> GetAllAsync(Languages language = Languages.Azerbaijan)
        {
            throw new NotImplementedException();
        }

        public Task<OrderGetDto> GetAsync(int id, Languages language = Languages.Azerbaijan)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Order> GetAll()
        {
            return  _orderRepository.GetAll();
        }
        public Task<OrderUpdateDto> GetUpdatedDtoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task NextOrderStatusAsync(int id)
        {
            var order = await _orderRepository.GetAsync(id, x => x.Include(x => x.OrderItems));

            if (order is null)
                throw new NotFoundException("Order NotFound");

            if (order.Status == false)
                order.Status = null;
            else
                order.Status = true;

            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task PrevOrderStatusAsync(int id)
        {
            var order = await _orderRepository.GetAsync(id, x => x.Include(x => x.OrderItems));

            if (order is null)
                throw new NotFoundException("NotFound Order");

            if (!order.IsCanceled)
            {
                if (order.Status is true)
                    order.Status = null;
                else
                    order.Status = false;

                _orderRepository.Update(order);
                await _orderRepository.SaveChangesAsync();
            }

          
        }

        public async Task RepairOrderAsync(int id)
        {
            var order = await _orderRepository.GetAsync(id, x => x.Include(x => x.OrderItems));

            if (order is null)
                throw new NotFoundException("NotFound Order");
            order.IsCanceled = !order.IsCanceled;

            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();

        }

        public async Task<bool> UpdateAsync(OrderUpdateDto dto, ModelStateDictionary ModelState)
        {
            if (ModelState.IsValid)
                return false;

            var existOrder = await _orderRepository.GetAsync(dto.Id);

            if (existOrder is null)
                throw new NotFoundException("Order NotFound");

            existOrder = _mapper.Map(dto, existOrder);

            _orderRepository.Update(existOrder);
            await _orderRepository.SaveChangesAsync();

            return true;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await  _orderRepository.GetAll()
                                          .Include(m=>m.OrderItems)
                                          .ToListAsync();
        }

        public async Task<Order> DetailAsync(int id)
        {
            var order = await _orderRepository.GetAll()
                                        .Include(m => m.OrderItems)
                                        .ThenInclude(x => x.Product)
                                        .ThenInclude(x => x.ProductImages)
                                        .FirstOrDefaultAsync(m=>m.Id == id);
            await _orderRepository.SaveChangesAsync();
            if(order == null)
            {
                throw new NotFoundException("Order NotFound");
            }

            return order;

        }
    }
}
