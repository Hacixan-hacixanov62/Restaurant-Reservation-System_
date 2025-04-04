using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Comment:BaseAuditableEntity
    {
        public string Text { get; set; } = null!;
        public string AppUserId { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
        public int? ProductId { get; set; }
        public Product? Product { get; set; } = null!;
        public int Rating { get; set; }
        public int? ParentId { get; set; }
        public Comment? Parent { get; set; } = null!;
        public List<Comment> Children { get; set; } = [];
    }
}
