
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.OrderItemDtos;

namespace Restaurant_Reservation_System_.Service.Dtos.OrderDtos
{
    public class OrderCreateDto:IDto
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? Apartment { get; set; }
        public string? CompanyName { get; set; }
        public string Street { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<OrderItemCreateDto> OrderItems { get; set; } = [];
    }
}
