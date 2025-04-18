

using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Service.Dtos.CommentDtos
{
    public class CommentReplyDto:IDto
    {
        public int ParentId { get; set; }
        public int ProductId { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Text Duzgun daxil edin.")]
        public string Text { get; set; } = null!;
    }
}
