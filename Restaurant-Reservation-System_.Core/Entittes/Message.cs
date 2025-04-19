
namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Message : BaseAuditableEntity
    {
        public string Text { get; set; } = null!;
        public AppUser? Sender { get; set; }
        public string? SenderId { get; set; }
        public Chat? Chats { get; set; }
        public int ChatId { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
