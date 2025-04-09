
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.AppUserDtos;
using Restaurant_Reservation_System_.Service.Dtos.OrderItemDtos;

namespace Restaurant_Reservation_System_.Service.Dtos.OrderDtos
{
    public class OrderGetDto:IDto
    {
        public int Id { get; set; }
        public string AppUserId { get; set; } = null!;
         public List<OrderItemGetDto> OrderItems { get; set; } = [];
        public decimal TotalPrice { get; set; }
        public string City { get; set; } = null!;
        public string? Apartment { get; set; }
        public string? CompanyName { get; set; }
        public string Street { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public UserGetDto AppUser { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
    }
}
