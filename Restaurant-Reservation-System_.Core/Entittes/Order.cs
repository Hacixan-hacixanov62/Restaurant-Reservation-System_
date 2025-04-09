using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Order:BaseAuditableEntity
    {
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
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public bool? Status { get; set; }
        public bool IsCanceled { get; set; }

    }
}
