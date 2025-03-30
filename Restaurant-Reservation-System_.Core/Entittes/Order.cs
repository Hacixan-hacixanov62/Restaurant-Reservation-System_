using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Order
    {
        public int Id { get; set; }
        public AppUser? AppUser { get; set; } = null!;
        public string? AppUserId { get; set; } = null!;
        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(65)]
        public string Surname { get; set; } = null!;
        public string City { get; set; } = null!;
        [Required]
        [MaxLength(65)]
        public string? Apartment { get; set; }
        public string? CompanyName { get; set; }
        public string Street { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<OrderItem> OrderItems { get; set; }
    }
}
