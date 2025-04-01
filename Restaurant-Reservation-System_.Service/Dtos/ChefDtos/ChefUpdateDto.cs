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
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 50)]
        public string Surname { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 500)]
        public string Description { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 100)]
        public string Biographia { get; set; } = null!;
        [NotMapped]
        [MaxSizeAttribute(2 * 1024 * 1024)]
        [AllowedTypes("image/jpeg", "image/png")]
        public IFormFile Image { get; set; } = null!;
    }
}
