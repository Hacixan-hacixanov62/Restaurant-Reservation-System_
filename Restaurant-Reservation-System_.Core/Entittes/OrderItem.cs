using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class OrderItem:BaseAuditableEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Count { get; set; }
        [Range(0.01, 10000.00, ErrorMessage = "Qiymət 0.01 ilə 10000.00 arasında olmalıdır.")]
        public decimal TotalPrice { get; set; }
    }
}
