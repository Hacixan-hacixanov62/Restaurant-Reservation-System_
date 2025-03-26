using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Service.ViewModels.SliderVM
{
    public class SliderCreateVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public IFormFile Logo { get; set; }
    }
}
