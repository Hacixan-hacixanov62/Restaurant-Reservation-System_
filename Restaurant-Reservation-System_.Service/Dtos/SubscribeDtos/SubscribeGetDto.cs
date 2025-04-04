using Restaurant_Reservation_System_.Service.Abstractions.Dtos;


namespace Restaurant_Reservation_System_.Service.Dtos.SubscribeDtos
{
    public class SubscribeGetDto:IDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
    }
}
