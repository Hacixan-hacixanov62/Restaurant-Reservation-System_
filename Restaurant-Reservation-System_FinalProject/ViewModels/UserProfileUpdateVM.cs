using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_FinalProject.ViewModels
{
    public class UserProfileUpdateVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FullName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
    }
}
