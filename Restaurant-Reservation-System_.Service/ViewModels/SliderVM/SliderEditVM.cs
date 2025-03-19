using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Service.ViewModels.SliderVM
{
    public class SliderEditVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage = "This input can't be empty")]
        public string Description { get; set; }
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
