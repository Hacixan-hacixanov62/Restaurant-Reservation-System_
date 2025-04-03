using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Repositories;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.BlogDtos;
using Restaurant_Reservation_System_.Service.Exceptions;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;
        public BlogService(AppDbContext context,IBlogRepository blogRepository,IWebHostEnvironment env,ICloudinaryService cloudinaryService,IMapper mapper,IConfiguration configuration)
        {
            _context = context;
            _blogRepository = blogRepository;
            _env = env;
            _configuration = configuration;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
        }
        public async Task CreateAsync(BlogCreateDto blogCreateDto)
        {
           Blog blog = _mapper.Map<Blog>(blogCreateDto);
            blog.ImageUrl = await _cloudinaryService.FileCreateAsync(blogCreateDto.Image);

            blog.BlogTopics = new List<BlogTopic>();


            foreach (var topicId in blogCreateDto.TopicIds)
            {
                BlogTopic blogTopic = new()
                {
                    Blog = blog,
                    TopicId = topicId
                };
                blog.BlogTopics.Add(blogTopic);
            }

            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product =  _blogRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (product == null)
            {
                throw new Exception("Blog tapılmadı");
            }

            _blogRepository.Delete(product);
            await _blogRepository.SaveChangesAsync();
        }

        public async Task<Blog> DetailAsync(int id)
        {
            var product = _blogRepository.GetAll()
                 .Include(m => m.Chef)
                 .Include(m => m.BlogTopics)              
                 .FirstOrDefault(m => m.Id == id);

            await _blogRepository.SaveChangesAsync();
            if (product == null)
            {
                throw new Exception("Blog tapılmadı");
            }
            return product;
        }

        public async Task EditAsync(int id, BlogUpdateDto blogUpdateDto)
        {
            var blog = await _context.Blogs.Include(x => x.BlogTopics)
                                           .Include(x => x.Chef)
                                           .FirstOrDefaultAsync(x => x.Id == id);

            if (blog == null)
                throw new Exception("Blog tapılmadı");


            BlogUpdateDto dto = _mapper.Map<BlogUpdateDto>(blog);

            dto.TopicIds = blog.BlogTopics.Select(x => x.TopicId).ToList();

            var existBlog = await _context.Blogs
                                          .Include(x => x.BlogTopics)
                                          .Include(x => x.Chef)
                                          .FirstOrDefaultAsync(x => x.Id == id);


            if (existBlog is null)
            {
                throw new NotFoundException();
            }         


            foreach (var blogTopic in existBlog.BlogTopics)
            {
                _context.BlogTopics.Remove(blogTopic);
            }

            foreach (var topic in dto.TopicIds)
            {
                BlogTopic blogTopic = new() { Blog = existBlog, TopicId = topic };
                existBlog.BlogTopics.Add(blogTopic);
            }




            existBlog = _mapper.Map(dto, existBlog);

            if (dto.Image is not null)
            {
                existBlog.ImageUrl = await _cloudinaryService.FileCreateAsync(dto.Image);
            }

            _blogRepository.Update(existBlog);
            await _blogRepository.SaveChangesAsync();

        }

        public Task<List<Blog>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
