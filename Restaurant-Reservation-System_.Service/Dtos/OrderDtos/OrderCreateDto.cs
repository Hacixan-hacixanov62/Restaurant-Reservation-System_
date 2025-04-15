
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.OrderItemDtos;

namespace Restaurant_Reservation_System_.Service.Dtos.OrderDtos
{
    public class OrderCreateDto:IDto
    {
        public string? Name { get; set; } = "Hacixan";
        public string? Surname { get; set; } = "Hacixan";
        public string City { get; set; } = null!;
        public string? Apartment { get; set; }
        public string? CompanyName { get; set; }
        public string Street { get; set; } = "City";
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<OrderItemCreateDto> OrderItems { get; set; } = [];
        public string stripeToken { get; set; } = null!;
        public string stripeEmail { get; set; } = null!;
    }
}
