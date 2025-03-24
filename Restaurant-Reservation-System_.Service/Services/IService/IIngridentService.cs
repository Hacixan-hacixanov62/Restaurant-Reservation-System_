using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.ViewModels.CategoryVM;
using Restaurant_Reservation_System_.Service.ViewModels.IngrideantVM;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface IIngridentService
    {
        Task CreateAsync(IngredientCreateVM ıngredientCreateVM);
        Task DeleteAsync(int id);
        Task<Ingredient> DetailAsync(int id);
        Task<List<Ingredient>> GetAllAsync();
        Task EditAsync(int id, IngredientEditVM ıngredientEditVM);
    }
}
