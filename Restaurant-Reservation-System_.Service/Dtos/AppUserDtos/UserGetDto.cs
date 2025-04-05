

using Restaurant_Reservation_System_.Service.Abstractions.Dtos;

namespace Restaurant_Reservation_System_.Service.Dtos.AppUserDtos
{
    public class UserGetDto:IDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
