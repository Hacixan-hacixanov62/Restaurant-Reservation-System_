

using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Service.Dtos.CommentDtos
{
    public class CommentCreateDto:IDto
    {
        public int ProductId { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Text Duzgun daxil edin.")]
        public string Text { get; set; } = null!;
        public int Rating { get; set; }
    }
}
