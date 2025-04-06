
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Service.UI.Dtos
{
    public class ContactDto:IDto
    {
        [Required(ErrorMessage = "Name sahəsi boş ola bilməz.")]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Göndəriləcək e-poçt sahəsi boş ola bilməz.")]
        [EmailAddress(ErrorMessage = "Düzgün e-poçt ünvanı daxil edin.")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Phone sahəsi boş ola bilməz.")]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "Mövzu sahəsi boş ola bilməz.")]
        [MaxLength(200)]
        public string Message { get; set; } = null!;
    }
}
