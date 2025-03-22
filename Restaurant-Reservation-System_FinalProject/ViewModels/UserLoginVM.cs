using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_FinalProject.ViewModels
{
    public class UserLoginVM
    {
        [Required]
        public string? UserNameOrEmail { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
