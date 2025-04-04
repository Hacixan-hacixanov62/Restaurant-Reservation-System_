using Restaurant_Reservation_System_.Service.Abstractions.Dtos;


namespace Restaurant_Reservation_System_.Service.Dtos.SubscribeDtos
{
    public class SubscribeCreateDto:IDto
    {
        public string Email { get; set; } = null!;
    }
}
