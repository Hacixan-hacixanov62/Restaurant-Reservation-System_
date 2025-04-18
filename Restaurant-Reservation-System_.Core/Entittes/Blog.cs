using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Blog:BaseAuditableEntity
    {
        [Required]
        [StringLength(maximumLength: 150)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Title Duzgun daxil edin.")]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 500)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "MinDescription Duzgun daxil edin.")]
        public string MinDescription { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 500)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "MaxDescription Duzgun daxil edin.")]
        public string MaxDescription { get; set; } = null!;
        [Required]
        public string ImageUrl { get; set; } = null!;

        public int ChefId { get; set; }
        public Chef Chef { get; set; } = null!;

        public ICollection<BlogTopic> BlogTopics { get; set; } = new List<BlogTopic>();
    }
}
