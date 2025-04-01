using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Repositories;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Dtos.ChefDtos;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class ChefService : IChefService
    {
        private readonly AppDbContext _context;
        private readonly IChefRepository _chefRepository;
        private readonly IWebHostEnvironment _env;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;
        public ChefService(AppDbContext context, IChefRepository chefRepository,IWebHostEnvironment env, ICloudinaryService cloudinaryService,IMapper mapper)
        {
            _context = context;
            _chefRepository = chefRepository;  
            _env = env;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
        }
        public async Task CreateAsync(ChefCreateDto chefCreateDto)
        {
          
            Chef chef = _mapper.Map<Chef>(chefCreateDto);
            chef.ImageUrl = await _cloudinaryService.FileCreateAsync(chefCreateDto.Image);

            await _context.Chefs.AddAsync(chef);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = _chefRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (product == null)
            {
                throw new Exception("Chef tapılmadı");
            }

            _chefRepository.Delete(product);
            await _chefRepository.SaveChangesAsync();
        }

        public async Task<Chef> DetailAsync(int id)
        {
            var chef = await _chefRepository.GetAll()
                   .Where(s => s.Id == id)
                   .Select(s => new Chef
                   {
                       Id = s.Id,
                       Name = s.Name,
                       Surname = s.Surname,
                       Description = s.Description,
                       Biographia = s.Biographia,
                       ImageUrl = s.ImageUrl
                   })
                   .FirstOrDefaultAsync();

            if (chef == null)
            {
                throw new Exception("Chef tapılmadı");
            }

            return chef;
        }

        public async Task EditAsync(int id, ChefUpdateDto chefUpdateDto)
        {
            var chef = _chefRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (chef == null)
            {
                throw new Exception("Chef tapılmadı");
            }

            ChefUpdateDto dto = _mapper.Map<ChefUpdateDto>(chef);

            chef = _mapper.Map(chefUpdateDto, chef);
            if (chefUpdateDto is not null)
            {
                chef.ImageUrl = await _cloudinaryService.FileCreateAsync(chefUpdateDto.Image);
            }

            _chefRepository.Update(chef);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Chef>> GetAllAsync()
        {
            var chefs = await _chefRepository.GetAll().ToListAsync();
            return chefs.Select(s => new Chef
            {
                Id = s.Id,
               Name = s.Name,
               Surname = s.Surname,
               Description = s.Description,
               Biographia = s.Biographia,
               ImageUrl = s.ImageUrl

            }).ToList();
        }
    }
}
