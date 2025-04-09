

using Restaurant_Reservation_System_.Service.Abstractions.Dtos;

namespace Restaurant_Reservation_System_.Service.Dtos.CommentDtos
{
    public class CommentCreateDto:IDto
    {
        public int ProductId { get; set; }
        public string Text { get; set; } = null!;
        public int Rating { get; set; }
    }
}
