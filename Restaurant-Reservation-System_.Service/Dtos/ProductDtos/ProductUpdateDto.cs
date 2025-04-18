using Microsoft.AspNetCore.Http;
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDetailDtos;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Restaurant_Reservation_System_.Core.Attributes;


namespace Restaurant_Reservation_System_.Service.Dtos.ProductDtos
{
    public class ProductUpdateDto:IDto
    {

        [Required]
        [StringLength(maximumLength: 150)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Name Duzgun daxil edin.")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Price mənfi ola bilməz.")]
        public decimal Price { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Desc Duzgun daxil edin.")]
        public string Desc { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Ingredients Duzgun daxil edin.")]
        public string Ingredients { get; set; } = null!;
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 100, ErrorMessage = "Prosion Duzgun daxil edin.")]
        public int Porsion { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 100, ErrorMessage = "Discount Duzgun daxil edin.")]
        public decimal Discount { get; set; }
        public int CategoryId { get; set; }
        public int SalesCount { get; set; }


        public string? MainFileUrl { get; set; }
        [NotMapped]
        [MaxSizeAttribute(2 * 1024 * 1024)]
        [AllowedTypes("image/jpeg", "image/png")]
        public IFormFile? MainFile { get; set; } = null!;
        [NotMapped]
        [MaxSizeAttribute(2 * 1024 * 1024)]
        [AllowedTypes("image/jpeg", "image/png")]
        public List<IFormFile> AdditionalFiles { get; set; } = new();
        public List<string> ImagePaths { get; set; } = new();
        public List<int> ImageIds { get; set; } = new();
    }
}
