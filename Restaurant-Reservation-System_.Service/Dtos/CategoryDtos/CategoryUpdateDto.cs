
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDetailDtos;

namespace Restaurant_Reservation_System_.Service.Dtos.CategoryDtos
{
    public class CategoryUpdateDto:IDto
    {
        public int Id { get; set; }
       
        public List<CategoryGetDto> Categories { get; set; } = [];
        public List<CategoryDetailUpdateDto> CategoryDetails { get; set; } = [];
    }
}
