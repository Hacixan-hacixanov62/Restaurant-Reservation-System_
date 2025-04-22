using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_FinalProject.ViewModels
{
    public class UserLoginVM
    {
        [Required]
        public string UserNameOrEmail { get; set; } = null!;
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; } =null!;
        public bool RememberMe { get; set; } = false;
    }
}
