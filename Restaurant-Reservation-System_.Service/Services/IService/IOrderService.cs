using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.Dtos.OrderDtos;
using Restaurant_Reservation_System_.Service.Generic;


namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface IOrderService:IModifyService<OrderCreateDto, OrderUpdateDto>, IGetServiceWithLanguage<OrderGetDto>
    {
        Task CancelOrderAsync(int id);
        Task RepairOrderAsync(int id);
        Task NextOrderStatusAsync(int id);
        Task PrevOrderStatusAsync(int id);
        Task<List<Order>> GetAllAsync();
        IQueryable<Order> GetAll();
        Task<Order> DetailAsync(int id);
        //Task CreateAsync(OrderCreateDto orderCreateDto);    
    }
}
