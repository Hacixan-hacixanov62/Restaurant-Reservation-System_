

using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.AppUserDtos;

namespace Restaurant_Reservation_System_.Service.Dtos.CommentDtos
{
    public class CommentGetDto : IDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        //public int BlogId { get; set; }
        public string Text { get; set; } = null!;
        public int Rating { get; set; }
        public UserGetDto AppUser { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public List<CommentGetDto> Children { get; set; } = [];
    }
}
