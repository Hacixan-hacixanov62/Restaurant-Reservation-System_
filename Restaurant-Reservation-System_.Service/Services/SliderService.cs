using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Helpers;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.ViewModels.SliderVM;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderService(AppDbContext context,
                          IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task CreateAsync(SliderCreateVM request)
        {
            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;

            string path = _env.GenerateFilePath("images", fileName);

            await request.Image.SaveFileToLocalAsync(path);

            await _context.Sliders.AddAsync(new Slider
            {
                Title = request.Title,
                Description = request.Description,
                Image = fileName
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            string imgPath = _env.GenerateFilePath("images", slider.Image);
            imgPath.DeleteFileFromLocal();

            _context.Sliders.Remove(slider);

            await _context.SaveChangesAsync();
        }



        public async Task<SliderVM> DetailAsync(int id)
        {

            Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            return new SliderVM
            {
                Id = slider.Id,
                Title = slider.Title,
                Description = slider.Description,
                Image = slider.Image,

            };
        }

    }
}
