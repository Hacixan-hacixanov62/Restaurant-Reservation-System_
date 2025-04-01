
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDetailDtos;

namespace Restaurant_Reservation_System_.Service.Dtos.CategoryDtos
{
    public class CategoryUpdateDto
    {
        public string Title { get; set; } = null!;
        public string SubTitle { get; set; } = null!;
    }
}
