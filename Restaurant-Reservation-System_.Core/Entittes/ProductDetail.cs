using Restaurant_Reservation_System_.Core.Entittes.Comman;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class ProductDetail:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public int ProductId { get; set; }
    }
}
