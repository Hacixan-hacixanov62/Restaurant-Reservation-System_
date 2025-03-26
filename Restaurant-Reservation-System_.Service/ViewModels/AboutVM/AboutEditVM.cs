
using Microsoft.AspNetCore.Http;

namespace Restaurant_Reservation_System_.Service.ViewModels.AboutVM
{
    public class AboutEditVM
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImagePath { get; set; } = null!;
        public IFormFile? Image { get; set; }   
    }
}
