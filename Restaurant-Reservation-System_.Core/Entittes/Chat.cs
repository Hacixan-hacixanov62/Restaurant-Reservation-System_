

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Chat:BaseAuditableEntity
    {
        public string? Name { get; set; }
        public List<AppUserChat> AppUserChats { get; set; } = [];
        public List<Message> Messages { get; set; } = [];
        //public string? UserId { get; set; }

        //public AppUser User { get; set; } = null!;
    }
}
