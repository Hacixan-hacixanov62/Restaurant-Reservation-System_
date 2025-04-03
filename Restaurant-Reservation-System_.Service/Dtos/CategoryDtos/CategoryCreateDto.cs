
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Service.Dtos.CategoryDtos
{
    public class CategoryCreateDto
    {
        [Required]
        [StringLength(maximumLength: 200)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 200)]
        public string SubTitle { get; set; } = null!;
    }
}
