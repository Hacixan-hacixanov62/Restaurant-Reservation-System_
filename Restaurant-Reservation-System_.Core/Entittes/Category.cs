using Restaurant_Reservation_System_.Core.Entittes.Comman;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
