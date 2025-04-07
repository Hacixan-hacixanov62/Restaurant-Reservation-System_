
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Service.Dtos.CategoryDtos
{
    public class CategoryGetDto:IDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 200)]
        public string SubTitle { get; set; } = null!;
        public List<ProductGetDto> Products { get; set; } = [];
    }
}
