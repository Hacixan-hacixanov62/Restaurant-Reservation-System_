using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Chef:BaseAuditableEntity
    {
        [Required]
        [StringLength(maximumLength: 50)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Name Duzgun daxil edin.")]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 50)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "SurName Duzgun daxil edin.")]
        public string Surname { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 500)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Desc Duzgun daxil edin.")]
        public string Description { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 100)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Biographia Duzgun daxil edin.")]
        public string Biographia { get; set; } = null!;
        [Required]
        public string ImageUrl { get; set; } = null!;
        [NotMapped]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "FullName Duzgun daxil edin.")]
        public string Fullname { get => $"{Name} {Surname} "; }
        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
