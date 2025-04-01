using Restaurant_Reservation_System_.Core.Entittes.Comman;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class BaseAuditableEntity:BaseEntity
    {
        public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public string CreatedBy { get; set; } = null!;
		public string? UpdatedBy { get; set; }
		public bool IsDeleted { get; set; } = false;
    }
}
