using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Dtos.TopicDtos;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface ITopicService
    {
        Task CreateAsync(TopicCreateDto topicCreateDto);
        Task DeleteAsync(int id);
        Task<Topic> DetailAsync(int id);
        Task<List<Topic>> GetAllAsync();
        Task EditAsync(int id, TopicUpdateDto topicUpdateDto);
    }
}
