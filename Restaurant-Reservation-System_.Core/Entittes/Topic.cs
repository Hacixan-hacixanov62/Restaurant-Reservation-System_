using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Topic:BaseAuditableEntity
    {
        [Required]
        [StringLength(maximumLength: 50)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Name Duzgun daxil edin.")]
        public string Name { get; set; } = null!;
        public ICollection<BlogTopic> BlogTopics { get; set; } = new List<BlogTopic>();
    }
}
