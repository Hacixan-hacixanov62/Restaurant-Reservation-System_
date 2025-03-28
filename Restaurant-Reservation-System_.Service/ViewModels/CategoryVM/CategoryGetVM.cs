

namespace Restaurant_Reservation_System_.Service.ViewModels.CategoryVM
{
    public class CategoryGetVM
    {
        public int Id { get; set; }
       
        public string Name { get; set; } = null!;
        
        public List<ProductGetVM> Products { get; set; } = [];
    }
}
