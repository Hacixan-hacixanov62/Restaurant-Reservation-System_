

using Restaurant_Reservation_System_.Service.Abstractions.Dtos;

namespace Restaurant_Reservation_System_.Service.Dtos.CommentDtos
{
    public class CommentReplyDto:IDto
    {
        public int ParentId { get; set; }
        public int ProductId { get; set; }

        public string Text { get; set; } = null!;
    }
}
