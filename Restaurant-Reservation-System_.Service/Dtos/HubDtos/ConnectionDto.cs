

using Restaurant_Reservation_System_.Service.Abstractions.Dtos;

namespace Restaurant_Reservation_System_.Service.Dtos.HubDtos
{
    public class ConnectionDto:IDto
    {
        public string UserId { get; set; } = null!;
        public List<string> ConnectionIds { get; set; } = [];
    }
}
