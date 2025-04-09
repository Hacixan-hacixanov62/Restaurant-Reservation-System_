using Restaurant_Reservation_System_.Service.Abstractions.Dtos;


namespace Restaurant_Reservation_System_.Service.Dtos.CommentDtos
{
    public class CommentUpdateDto:IDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        //public int BlogId { get; set; }
        public string Text { get; set; } = null!;
        public int Rating { get; set; }
    }
}
