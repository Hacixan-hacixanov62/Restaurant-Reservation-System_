

using Restaurant_Reservation_System_.Service.Abstractions.Dtos;

namespace Restaurant_Reservation_System_.Service.Dtos.BlogCommentDtos
{
    public class BlogCommentReplyDto : IDto
    {
        public int ParentId { get; set; }
        public int ProductId { get; set; }

        public string Text { get; set; } = null!;
    }
}
