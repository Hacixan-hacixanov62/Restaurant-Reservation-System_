
using System.ComponentModel.DataAnnotations;


namespace Restaurant_Reservation_System_.Service.Dtos.TableDtos
{
    public class TableCreateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Table duzgun daxil edin.")]
        public int TableNo { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Şəxslərin sayı 1 ilə 10 arasında olmalıdır.")]
        public int PersonCount { get; set; }
    }
}
