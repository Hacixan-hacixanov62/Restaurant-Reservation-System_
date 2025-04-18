using Microsoft.AspNetCore.Http;
using Restaurant_Reservation_System_.Core.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Service.Dtos.ChefDtos
{
    public class ChefUpdateDto
    {

        [Required]
        [StringLength(maximumLength: 50)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Name Duzgun daxil edin.")]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 50)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "SurName Duzgun daxil edin.")]
        public string Surname { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 500)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Desc Duzgun daxil edin.")]
        public string Description { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 100)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Biographia Duzgun daxil edin.")]
        public string Biographia { get; set; } = null!;
        [NotMapped]
        [MaxSizeAttribute(2 * 1024 * 1024)]
        [AllowedTypes("image/jpeg", "image/png")]
        public IFormFile Image { get; set; } = null!;
    }
}
