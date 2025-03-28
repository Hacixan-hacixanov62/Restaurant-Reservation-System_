

namespace Restaurant_Reservation_System_.Service.Dtos.ProductDetailDtos
{
    public class ProductDetailCreateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int LanguageId { get; set; }
    }
}
