using Restaurant_Reservation_System_.Core.Entittes.Comman;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Category: BaseAuditableEntity
    {
        [Required]
        [StringLength(maximumLength: 200)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 200)]
        public string SubTitle { get; set; } = null!;
        public int Order { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
