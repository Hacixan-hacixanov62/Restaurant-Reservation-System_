using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.ViewModels.SliderVM;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface ISliderService
    {
        Task CreateAsync(SliderCreateVM request);
        Task DeleteAsync(int id);
        Task<Slider> DetailAsync(int id);
        Task<List<Slider>> GetAllAsync();
        Task UpdateAsync(int id, SliderEditVM request);
    }
}
