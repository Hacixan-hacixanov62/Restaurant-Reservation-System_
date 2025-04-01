using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.Dtos.ChefDtos;
using Restaurant_Reservation_System_.Service.ViewModels.AboutVM;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface IChefService
    {
        Task CreateAsync(ChefCreateDto chefCreateDto);
        Task DeleteAsync(int id);
        Task<Chef> DetailAsync(int id);
        Task<List<Chef>> GetAllAsync();
        Task EditAsync(int id, ChefUpdateDto chefUpdateDto);
    }
}
