using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Helpers;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.ViewModels.SliderVM;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class SliderService : ISliderService
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        private readonly ISliderRepository _sliderRepository;
        private readonly CloudinaryService _cloudinaryService;
        public SliderService(IWebHostEnvironment env, AppDbContext context, ISliderRepository sliderRepository, CloudinaryService cloudinaryService)
        {
            _env = env;
            _context = context;
            _sliderRepository = sliderRepository;
            _cloudinaryService = cloudinaryService;
        }
        public async Task CreateAsync(SliderCreateVM request)
        {
            if (!request.Image.CheckType(new string[] { "image/jpeg", "image/png" }) && !request.Logo.CheckType(new string[] { "image/jpeg", "image/png" }))
            {
                throw new Exception("Şəklin formatı yalnız JPEG və ya PNG ola bilər.");
            }
            if (request.Image.CheckSize(2 * 1024 * 1024) && request.Logo.CheckSize(2 * 1024 * 1024)) 
            {
                throw new Exception("Şəklin ölçüsü 2 MB-dan çox ola bilməz.");
            }

            string folderPath = Path.Combine(_env.WebRootPath, "assets/images/home");
            string imageName = request.Image.SaveImage(_env.WebRootPath, "assets/images/home");
            string logoName = request.Logo.SaveImage(_env.WebRootPath, "assets/images/home");

            var slider = new Slider
            {
                Title = request.Title,
                Description = request.Description,
                Image = imageName, 
                Logo =logoName
            };

            _sliderRepository.Add(slider);
        }
        
        public async Task EditAsync(int id, SliderEditVM request)
        {
            var slider = _sliderRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (slider == null)
            {
                throw new Exception("Slider tapılmadı");
            }
            
            if (request.Image != null)
            {
                if (!request.NewImage.CheckType(new string[] { "image/jpeg", "image/png" }))
                {
                    throw new Exception("Şəklin formatı yalnız JPEG və ya PNG ola bilər.");
                }
                if (request.NewImage.CheckSize(500000))
                {
                    throw new Exception("Şəklin ölçüsü 500 KB-dan çox ola bilməz.");
                }


                string oldImagePath = Path.Combine(_env.WebRootPath, "assets/images/home", slider.Image);
                FileManager.DeleteFile(oldImagePath);

                string newImageName = request.NewImage.SaveImage(_env.WebRootPath, "assets/images/home");
                slider.Image = newImageName;
            }


            slider.Title = request.Title;
            slider.Description = request.Description;

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

            _sliderRepository.Delete(slider);
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
