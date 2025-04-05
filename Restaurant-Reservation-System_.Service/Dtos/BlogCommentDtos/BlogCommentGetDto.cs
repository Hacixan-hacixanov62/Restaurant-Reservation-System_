
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.AppUserDtos;

namespace Restaurant_Reservation_System_.Service.Dtos.BlogCommentDtos
{
    public class BlogCommentGetDto : IDto
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Text { get; set; } = null!;
        public int Rating { get; set; }
        public UserGetDto AppUser { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public List<BlogComment> Children { get; set; } = [];

    }
}
