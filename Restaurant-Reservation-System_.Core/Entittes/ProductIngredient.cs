using Restaurant_Reservation_System_.Core.Entittes.Comman;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class ProductIngredient:BaseEntity
    {
        public int IngredientId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
