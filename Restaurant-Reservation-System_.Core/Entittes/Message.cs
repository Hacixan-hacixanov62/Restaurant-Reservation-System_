
namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Message:BaseAuditableEntity
    {
        public string Text { get; set; } = null!;
        public AppUser Sender { get; set; } = null!;
        public string SenderId { get; set; }=null!;
        public AppUser Receiver { get; set; } = null!;
        public string ReceiverId { get; set; } = null!;
    }
}
