
using Microsoft.AspNetCore.Http;
using Restaurant_Reservation_System_.Core.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Core.Entittes;

namespace Restaurant_Reservation_System_.Service.Dtos.ProductDtos
{
    public class ProductGetDto:IDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 150)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string Desc { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Ingredients { get; set; } = null!;
        public int Porsion { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; }
        public int CategoryId { get; set; }
        public List<CategoryGetDto> Categories { get; set; } = null!;
        public int SalesCount { get; set; }
        public string MainImage { get; set; } = null!;
        public List<string> ImagePaths { get; set; } = [];

        [NotMapped]
        [MaxSizeAttribute(2 * 1024 * 1024)]
        [AllowedTypes("image/jpeg", "image/png")]
        public IFormFile MainFile { get; set; } = null!;
        [NotMapped]
        [MaxSizeAttribute(2 * 1024 * 1024)]
        [AllowedTypes("image/jpeg", "image/png")]
        public List<IFormFile> AdditionalFiles { get; set; } = new();

        public List<ProductGetDto> Products { get; set; } = [];


        //Bunu baskete gore yazmisam sile bilerem
        public List<ProductImage> ProductImages { get; set; } = null!;
  

    }
}
