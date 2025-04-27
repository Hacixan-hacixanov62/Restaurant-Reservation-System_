using Microsoft.AspNetCore.Http;
using Restaurant_Reservation_System_.Core.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Reservation_System_.Service.Dtos.BlogDtos
{
    public class BlogCreateDto
    {
        [Required]
        [StringLength(maximumLength: 150)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 500)]
        public string MinDescription { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 500)]
        public string MaxDescription { get; set; } = null!;
        public int ChefId { get; set; }
        [NotMapped]
        [MaxSizeAttribute(2 * 1024 * 1024)]
        [AllowedTypes("image/jpeg", "image/png")]
        public IFormFile Image { get; set; } = null!;
        public List<int> TopicIds { get; set; } = new();
    }
}
