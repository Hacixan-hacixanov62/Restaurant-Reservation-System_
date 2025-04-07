

using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {            
            _orderRepository = orderRepository;
        }

        public Task CancelOrderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task NextOrderStatusAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task PrevOrderStatusAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RepairOrderAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
