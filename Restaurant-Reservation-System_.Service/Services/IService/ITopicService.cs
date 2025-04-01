using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface ITopicService
    {
        Task CreateAsync(Topic topic);
        Task DeleteAsync(int id);
        Task<Topic> DetailAsync(int id);
        Task<List<Topic>> GetAllAsync();
        Task EditAsync(int id, Topic topic);
    }
}
