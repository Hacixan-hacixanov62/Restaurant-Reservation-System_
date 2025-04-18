using Restaurant_Reservation_System_.Core.Entittes.Comman;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Subscribe:BaseEntity
    {
        [Required(ErrorMessage = "Email daxil edilməlidir.")]
        [EmailAddress(ErrorMessage = "Email formatı düzgün deyil.")]
        [StringLength(100, ErrorMessage = "Email maksimum 100 simvol ola bilər.")]
        public string Email { get; set; } = null!;
        public bool IsSubscribed { get; set; } = false;
    }
}
