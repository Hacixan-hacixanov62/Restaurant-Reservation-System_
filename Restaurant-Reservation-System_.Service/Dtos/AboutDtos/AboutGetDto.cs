namespace Restaurant_Reservation_System_.Service.Dtos.AboutDtos
{
    public class AboutGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImagePath { get; set; } = null!;
    }
}
