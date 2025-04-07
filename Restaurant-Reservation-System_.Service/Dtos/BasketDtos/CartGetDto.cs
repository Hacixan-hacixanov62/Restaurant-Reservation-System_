
namespace Restaurant_Reservation_System_.Service.Dtos.BasketDtos
{
    public class CartGetDto
    {
        public int Count { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }

        public List<CartItemDto> Items { get; set; } = [];
    }
}
