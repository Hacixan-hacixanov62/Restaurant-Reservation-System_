using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class TopicService : ITopicService
    {
        public Task CreateAsync(Topic topic)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Topic> DetailAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(int id, Topic topic)
        {
            throw new NotImplementedException();
        }

        public Task<List<Topic>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
