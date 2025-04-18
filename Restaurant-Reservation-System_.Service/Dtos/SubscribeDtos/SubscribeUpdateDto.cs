using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using System.ComponentModel.DataAnnotations;


namespace Restaurant_Reservation_System_.Service.Dtos.SubscribeDtos
{
    public class SubscribeUpdateDto:IDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Email daxil edilməlidir.")]
        [EmailAddress(ErrorMessage = "Email formatı düzgün deyil.")]
        [StringLength(100, ErrorMessage = "Email maksimum 100 simvol ola bilər.")]
        public string Email { get; set; } = null!;
    }
}
