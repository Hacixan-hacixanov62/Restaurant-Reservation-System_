using Restaurant_Reservation_System_.Core.Entittes.Comman;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Category:BaseEntity
    {
        public ICollection<CategoryDetail> CategoryDetails { get; set; } = [];
        public ICollection<Product> Products { get; set; } = [];
        public bool IsDeleted { get; set; } = false;
    }
}
