using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Repositories;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Dtos.TopicDtos;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class TopicService : ITopicService
    {
        private readonly AppDbContext _context;
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;
        public TopicService(AppDbContext context, IMapper mapper,ITopicRepository topicRepository)
        {
            _context = context;
            _mapper = mapper;
            _topicRepository = topicRepository;
        }
        public async Task CreateAsync(TopicCreateDto topicCreateDto)
        {
            Topic topic  = _mapper.Map<Topic>(topicCreateDto);  

            await _topicRepository.CreateAsync(topic);
            await _topicRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {

            var category = await _topicRepository.GetAll().FirstOrDefaultAsync(s => s.Id == id);
            if (category == null)
            {
                throw new Exception("Topic tapılmadı");
            }

            _topicRepository.Delete(category);
            await _topicRepository.SaveChangesAsync();
        }

        public async Task<Topic> DetailAsync(int id)
        {
            var topic = await _topicRepository.GetAll()
                .Where(s => s.Id == id)
                .Select(s => new Topic
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .FirstOrDefaultAsync();

            if (topic == null)
            {
                throw new Exception("Topic tapılmadı");
            }

            return topic;
        }

        public async Task EditAsync(int id, TopicUpdateDto topicUpdateDto)
        {
            var topic = await _topicRepository.GetAll().FirstOrDefaultAsync(s => s.Id == id);

            if (topic == null)
            {
                throw new Exception("Topic tapılmadı");
            }

            TopicUpdateDto dto = _mapper.Map<TopicUpdateDto>(topic);

            _topicRepository.Update(topic);
            await _topicRepository.SaveChangesAsync();
        }

        public async Task<List<Topic>> GetAllAsync()
        {
            var categories = await _topicRepository.GetAll().ToListAsync();
            return categories.Select(s => new Topic
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }
    }
}
