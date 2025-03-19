using Restaurant_Reservation_System_.Service.ViewModels.SliderVM;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface ISliderService
    {
        Task CreateAsync(SliderCreateVM request);
        Task DeleteAsync(int id);
        Task<SliderVM> DetailAsync(int id);

    }
}
