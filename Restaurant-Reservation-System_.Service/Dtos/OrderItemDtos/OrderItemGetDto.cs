
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;

namespace Restaurant_Reservation_System_.Service.Dtos.OrderItemDtos
{
    public class OrderItemGetDto:IDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductId { get; set; }
        public ProductGetDto Product { get; set; } = null!;
        public int Count { get; set; }
    }
}
