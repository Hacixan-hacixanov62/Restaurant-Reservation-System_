using Restaurant_Reservation_System_.Core.Entittes.Comman;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Contact:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
