using Restaurant_Reservation_System_.Core.Entittes.Comman;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Language:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string ImagePath { get; set; } = null!;
        public ICollection<CategoryDetail> CategoryDetails { get; set; } = [];
    }
}
