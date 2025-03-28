using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Helpers;
using Restaurant_Reservation_System_.DataAccess.Repositories;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.ViewModels.AboutVM;
using Restaurant_Reservation_System_.Service.ViewModels.CategoryVM;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _context;
        private readonly IAboutRepository _aboutRepository;
        private readonly IWebHostEnvironment _env;
        public AboutService( AppDbContext context,IAboutRepository aboutRepository, IWebHostEnvironment env)
        {
            _context = context;
            _aboutRepository = aboutRepository;
            _env = env;
        }

        public async Task CreateAsync(AboutCreateVM aboutCreateVM)
        {
            if (!aboutCreateVM.Image.CheckType(new string[] { "image/jpeg", "image/png" }))
            {
                throw new Exception("Şəklin formatı yalnız JPEG və ya PNG ola bilər.");
            }
            if (aboutCreateVM.Image.CheckSize(2 * 1024 * 1024))
            {
                throw new Exception("Şəklin ölçüsü 2 MB-dan çox ola bilməz.");
            }

            string folderPath = Path.Combine(_env.WebRootPath, "assets/images/home");
            string imageName = aboutCreateVM.Image.SaveImage(_env.WebRootPath, "assets/images/home");
        
            var about = new About
            {
                Title = aboutCreateVM.Title,
                Desc = aboutCreateVM.Description,
                Image = imageName
            };

           await  _aboutRepository.CreateAsync(about);
        }

        public async Task DeleteAsync(int id)
        {
            var about = _aboutRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (about == null)
            {
                throw new Exception("About tapılmadı");
            }


            string imagePath = Path.Combine(_env.WebRootPath, "assets/images/home", about.Image);
            FileManager.DeleteFile(imagePath);

            _aboutRepository.Delete(about);
        }

        public async Task<About> DetailAsync(int id)
        {
            var about = _aboutRepository.GetAll()
                .Where(s => s.Id == id)
                .Select(s => new About
                {
                    Id = s.Id,
                    Title = s.Title,
                    Desc = s.Desc,
                    Image = s.Image,
                    // CreatedDate = s.CreatedDate.ToString("dd-MM-yyyy")
                })
                .FirstOrDefault();

            if (about == null)
            {
                throw new Exception("Slider tapılmadı");
            }

            return about;
        }

        public async Task EditAsync(int id, AboutEditVM aboutEditVM)
        {

            var about = _aboutRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (about == null)
            {
                throw new Exception("About tapılmadı");
            }

            if (aboutEditVM.Image != null)
            {
                if (!aboutEditVM.Image.CheckType(new string[] { "image/jpeg", "image/png" }))
                {
                    throw new Exception("Şəklin formatı yalnız JPEG və ya PNG ola bilər.");
                }
                if (aboutEditVM.Image.CheckSize(2*1024*1024))
                {
                    throw new Exception("Şəklin ölçüsü 2 MB-dan çox ola bilməz.");
                }


                string oldImagePath = Path.Combine(_env.WebRootPath, "assets/images/home", about.Image);
                FileManager.DeleteFile(oldImagePath);

                string newImageName = aboutEditVM.Image.SaveImage(_env.WebRootPath, "assets/images/home");
                about.Image = newImageName;
            }


            about.Title = aboutEditVM.Title;
            about.Desc = aboutEditVM.Description;

            _aboutRepository.Update(about);
        }

        public async Task<List<About>> GetAllAsync()
        {
            var abouts = await _aboutRepository.GetAll().ToListAsync();
            return abouts.Select(s => new About
            {
                Id = s.Id,
                Title = s.Title,
                Desc = s.Desc,
                Image = s.Image,
                // CreatedDate = s.CreatedDate.ToString("yyyy-MM-dd")
            }).ToList();
        }
    }
}
