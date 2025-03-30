using Restaurant_Reservation_System_.Core.Entittes.Comman;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class CategoryDetail:BaseEntity
    {
        public string Name { get; set; } = null!;
        public Category Category { get; set; } = null!;
        public int CategoryId { get; set; }
        public Language Language { get; set; }
        public int LanguageId { get; set; }
    }
}
