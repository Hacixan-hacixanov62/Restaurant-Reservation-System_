
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDetailDtos;

namespace Restaurant_Reservation_System_.Service.Dtos.CategoryDtos
{
    public class CategoryCreateDto:IDto
    {
        public List<CategoryGetDto> Categories { get; set; } = [];
        public List<CategoryDetailCreateDto> CategoryDetails { get; set; } = [];
    }
}
