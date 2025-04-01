using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Chef:BaseAuditableEntity
    {
        [Required]
        [StringLength(maximumLength: 50)]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 50)]
        public string Surname { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 500)]
        public string Description { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 100)]
        public string Biographia { get; set; } = null!;
        [Required]
        public string ImageUrl { get; set; } = null!;
        [NotMapped]
        public string Fullname { get => $"{Name} {Surname} "; }
        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
