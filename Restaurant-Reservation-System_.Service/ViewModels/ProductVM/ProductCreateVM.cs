using Microsoft.AspNetCore.Http;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.ViewModels.CategoryVM;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Reservation_System_.Service.ViewModels.ProductVM
{
    public class ProductCreateVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(500)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public IFormFile MainImage { get; set; } = null!;
        public int CategoryId { get; set; }
    }
}
