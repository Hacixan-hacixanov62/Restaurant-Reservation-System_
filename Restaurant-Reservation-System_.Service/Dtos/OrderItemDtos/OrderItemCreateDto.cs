using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;


namespace Restaurant_Reservation_System_.Service.Dtos.OrderItemDtos
{
    public class OrderItemCreateDto
    {
        public int ProductId { get; set; }
        public ProductGetDto Product { get; set; } = null!;
        public int Count { get; set; }
    }
}
