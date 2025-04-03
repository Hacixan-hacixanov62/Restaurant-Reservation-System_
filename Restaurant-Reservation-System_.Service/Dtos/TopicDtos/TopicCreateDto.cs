using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Service.Dtos.TopicDtos
{
    public class TopicCreateDto
    {

        [Required]
        [StringLength(maximumLength: 200)]
        public string Name { get; set; } = null!;
    }
}
