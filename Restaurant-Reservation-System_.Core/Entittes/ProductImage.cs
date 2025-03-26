using Restaurant_Reservation_System_.Core.Entittes.Comman;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class ProductImage:BaseEntity
    {
        public string Name { get; set; }
        public bool IsMain { get; set; } = false;
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool? Status { get; set; }
    }
}
