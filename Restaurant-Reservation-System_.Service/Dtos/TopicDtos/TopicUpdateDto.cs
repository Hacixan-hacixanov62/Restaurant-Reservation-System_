using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Service.Dtos.TopicDtos
{
    public class TopicUpdateDto
    {

        [Required]
        [StringLength(maximumLength: 200)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Name Duzgun daxil edin.")]
        public string Name { get; set; } = null!;
    }
}
