

using Restaurant_Reservation_System_.Service.ViewModels.CategoryVM;

namespace Restaurant_Reservation_System_.Service.ViewModels;

public class ProductGetVM 
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public List<CategoryGetVM> Categories { get; set; } = null!;
    public string MainImagePath { get; set; } = null!;
    public List<string> ImagePaths { get; set; } = [];
    public int CategoryId { get; set; } 


    public List<ProductGetVM> Products { get; set; } = [];


}
