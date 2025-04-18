using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Service.ViewModels.AboutVM
{
    public class AboutCreateVM
    {

        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(50)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Title Duzgun daxil edin.")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(500)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Desc Duzgun daxil edin.")]
        public string Description { get; set; } = null!;
        [Required]
        public IFormFile? Image { get; set; } = null!;
    }
}
