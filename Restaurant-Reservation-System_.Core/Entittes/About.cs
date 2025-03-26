using Restaurant_Reservation_System_.Core.Entittes.Comman;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class About :BaseEntity
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(50)]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(500)]
        public string Desc { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}
