using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.Repositories;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Dtos.ChefDtos;
using Restaurant_Reservation_System_.Service.Dtos.TopicDtos;
using Restaurant_Reservation_System_.Service.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TopicService(ITopicRepository topicRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task CreateAsync(TopicCreateDto topicCreateDto)
        {
            Topic topic = _mapper.Map<Topic>(topicCreateDto);

            var usernsme = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            topic.CreatedBy = usernsme;
            topic.UpdatedBy = usernsme;
            topic.CreatedAt = DateTime.UtcNow;
            topic.UpdatedAt = DateTime.UtcNow;
            await _topicRepository.CreateAsync(topic);
            await _topicRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chef = _topicRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (chef == null)
            {
                throw new Exception("Topic tapılmadı");
            }

         await   _topicRepository.Delete(chef);
            await _topicRepository.SaveChangesAsync();
        }

        public async Task<Topic> DetailAsync(int id)
        {
            var topic = await _topicRepository.GetAll()
                  .Where(s => s.Id == id)
                  .Select(s => new Topic
                  {
                      Id = s.Id,
                      Name = s.Name                     
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
            
            topic =_mapper.Map(topicUpdateDto,topic);

             _topicRepository.Update(topic);
            await _topicRepository.SaveChangesAsync();

        }

        public async Task<List<Topic>> GetAllAsync()
        {
            var topics = await _topicRepository.GetAll().ToListAsync();
            return topics.Select(s => new Topic
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }
    }
}
