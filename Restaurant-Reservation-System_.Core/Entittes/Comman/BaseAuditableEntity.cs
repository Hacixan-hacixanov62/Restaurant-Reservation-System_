using Restaurant_Reservation_System_.Core.Entittes.Comman;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class BaseAuditableEntity:BaseEntity
    {
        public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
        [Required]
		public string CreatedBy { get; set; } = null!;
        [Required]
		public string? UpdatedBy { get; set; }
		public bool IsDeleted { get; set; } = false;
    }
}
