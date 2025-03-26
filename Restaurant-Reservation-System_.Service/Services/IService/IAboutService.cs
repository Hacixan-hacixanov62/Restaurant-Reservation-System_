using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.ViewModels.AboutVM;
using Restaurant_Reservation_System_.Service.ViewModels.CategoryVM;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface IAboutService
    {
        Task CreateAsync(AboutCreateVM aboutCreateVM);
        Task DeleteAsync(int id);
        Task<About> DetailAsync(int id);
        Task<List<About>> GetAllAsync();
        Task EditAsync(int id, AboutEditVM aboutEditVM);
    }
}
