

using Restaurant_Reservation_System_.Service.Abstractions.Dtos;

namespace Restaurant_Reservation_System_.Service.Dtos.CategoryDetailDtos
{
    public class CategoryDetailCreateDto:IDto
    {
        public string Name { get; set; } = null!;
        public int LanguageId { get; set; }
    }
}
