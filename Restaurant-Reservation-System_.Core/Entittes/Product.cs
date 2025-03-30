using Microsoft.AspNetCore.Http;
using Restaurant_Reservation_System_.Core.Attributes;
using Restaurant_Reservation_System_.Core.Entittes.Comman;
using Restaurant_Reservation_System_.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Product:BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 150)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string Desc { get; set; }
        public string Image { get; set; } = null!;
        public int CategoryId { get; set; }
        public DeliciousStatus Delicious { get; set; }
        public int Weight { get; set; }

        public List<ProductImage> ProductImages { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }

        [NotMapped]
        [MaxSizeAttribute(2 * 1024 * 1024)]
        [AllowedTypes("image/jpeg", "image/png")]
        public List<IFormFile> Photos { get; set; }

        // Many To Many olanlar
        public List<ProductIngredient> ProductIngredients { get; set; }

        public Category Category { get; set; }
    }
}
