using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Helpers;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;
using Restaurant_Reservation_System_.Service.Dtos.SliderDtos;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.ViewModels.SliderVM;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class SliderService : ISliderService
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        private readonly ISliderRepository _sliderRepository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        public SliderService(IWebHostEnvironment env, AppDbContext context, ISliderRepository sliderRepository,IMapper mapper,ICloudinaryService cloudinaryService )
        {
            _env = env;
            _context = context;
            _sliderRepository = sliderRepository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;

        }
        public async Task CreateAsync(SliderCreateDto sliderCreateDto)
        {
            Slider slider = _mapper.Map<Slider>(sliderCreateDto);
            slider.Image = await _cloudinaryService.FileCreateAsync(sliderCreateDto.File);

          await  _sliderRepository.CreateAsync(slider);
            await _sliderRepository.SaveChangesAsync();
        }
        
        public async Task EditAsync(int id, SliderUpdateDto sliderUpdateDto)
        {
            var slider = _sliderRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (slider == null)
            {
                throw new Exception("Slider tapılmadı");
            }
            
            if (sliderUpdateDto.File != null)
            {
                if (!sliderUpdateDto.File.CheckType(new string[] { "image/jpeg", "image/png" }))
                {
                    throw new Exception("Şəklin formatı yalnız JPEG və ya PNG ola bilər.");
                }
                if (sliderUpdateDto.File.CheckSize(2*1024*1024))
                {
                    throw new Exception("Şəklin ölçüsü 2 MB-dan çox ola bilməz.");
                }


                string oldImagePath = Path.Combine(_env.WebRootPath, "assets/images/home", slider.Image);
                FileManager.DeleteFile(oldImagePath);

                string newImageName = sliderUpdateDto.File.SaveImage(_env.WebRootPath, "assets/images/home");
                slider.Image = newImageName;
            }


          
            _sliderRepository.Update(slider);
        }

        public async Task DeleteAsync(int id)
        {
            var slider = _sliderRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (slider == null)
            {
                throw new Exception("Slider tapılmadı");
            }


            string imagePath = Path.Combine(_env.WebRootPath, "assets/images/home", slider.Image);
            FileManager.DeleteFile(imagePath);

           await _sliderRepository.Delete(slider);
        }



        public async Task<Slider> DetailAsync(int id)
        {
            var slider = _sliderRepository.GetAll()
                .Where(s => s.Id == id)
                .Select(s => new Slider
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    Image = s.Image,
                   // CreatedDate = s.CreatedDate.ToString("dd-MM-yyyy")
                })
                .FirstOrDefault();

            if (slider == null)
            {
                throw new Exception("Slider tapılmadı");
            }

            return slider;
        }

        public async Task<List<Slider>> GetAllAsync()
        {
            var sliders = await _sliderRepository.GetAll().ToListAsync();
            return sliders.Select(s => new Slider
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Image = s.Image,
               // CreatedDate = s.CreatedDate.ToString("yyyy-MM-dd")
            }).ToList();
        }
    }

}
