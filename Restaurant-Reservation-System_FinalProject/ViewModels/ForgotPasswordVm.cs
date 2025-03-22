using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_FinalProject.ViewModels
{
    public class ForgotPasswordVm
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
