using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_FinalProject.Areas.Admin.ViewModels
{
    public class AdminLoginVM
    {
        [Required(ErrorMessage = "UserName is requred")]
        public string UsreName { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Minimum 6 simbol olmalidir")]
        [MaxLength(10)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
