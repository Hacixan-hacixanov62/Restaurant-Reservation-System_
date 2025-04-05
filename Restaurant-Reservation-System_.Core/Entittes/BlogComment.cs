

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class BlogComment:BaseAuditableEntity
    {
        public string Text { get; set; } = null!;
        public string AppUserId { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
        public int Rating { get; set; }
        public int? BlogId { get; set; }
        public Blog? Blog { get; set; } = null!;
        public int? ParentId { get; set; }
        public BlogComment? Parent { get; set; } = null!;
        public List<BlogComment> Children { get; set; } = [];
    }
}
