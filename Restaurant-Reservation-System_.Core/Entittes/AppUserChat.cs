

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class AppUserChat:BaseAuditableEntity
    {
        public int ChatId { get; set; }
        public Chat Chat { get; set; } = null!;
        public string AppUserId { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
    }
}
