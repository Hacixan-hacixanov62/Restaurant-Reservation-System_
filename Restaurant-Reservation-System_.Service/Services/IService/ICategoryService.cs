using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.ViewModels.CategoryVM;
using Restaurant_Reservation_System_.Service.ViewModels.SliderVM;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryCreateVM categoryCreateVM);
        Task DeleteAsync(int id);
        Task<Category> DetailAsync(int id);
        Task<List<Category>> GetAllAsync();
        Task EditAsync(int id, CategoryEditVM categoryEditVM);
    }
}
