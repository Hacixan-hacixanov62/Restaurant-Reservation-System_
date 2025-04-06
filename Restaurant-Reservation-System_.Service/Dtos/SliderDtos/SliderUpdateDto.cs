
using Microsoft.AspNetCore.Http;
using Restaurant_Reservation_System_.Core.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Reservation_System_.Service.Dtos.SliderDtos
{
    public class SliderUpdateDto
    {
        public string Name { get; set; } = null!;

        [NotMapped]
        [MaxSizeAttribute(2 * 1024 * 1024)]
        [AllowedTypes("image/jpeg", "image/png")]
        public IFormFile File { get; set; } = null!;
    }
}
