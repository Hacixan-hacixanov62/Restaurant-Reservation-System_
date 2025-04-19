

using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Comment : BaseAuditableEntity
    {
        public string Text { get; set; } = null!;
        public string? AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
        public int? ProductId { get; set; }
        public Product? Product { get; set; } = null!;
        public int Rating { get; set; }
        public int? ParentId { get; set; }
        //[NotMapped] 
        //public double AverageRating { get; set; }
        public Comment? Parent { get; set; } = null!;
        public ICollection<Comment> Children { get; set; } = [];

    }
}
