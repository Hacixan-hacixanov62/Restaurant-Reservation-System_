

using Restaurant_Reservation_System_.Service.Abstractions.Dtos;

namespace Restaurant_Reservation_System_.Service.Dtos.BlogCommentDtos;
    public class BlogCommentUpdateDto : IDto
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Text { get; set; } = null!;
        public int Rating { get; set; }
    }

