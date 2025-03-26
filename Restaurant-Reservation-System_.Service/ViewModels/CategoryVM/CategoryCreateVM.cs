using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Service.ViewModels.CategoryVM
{
    public class CategoryCreateVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(50)]
        public string Name { get; set; }

    }
}
