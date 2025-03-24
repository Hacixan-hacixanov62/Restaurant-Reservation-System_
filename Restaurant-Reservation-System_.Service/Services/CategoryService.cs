using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Repositories;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.ViewModels.CategoryVM;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(AppDbContext context,ICategoryRepository categoryRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
        }
        public async Task CreateAsync(CategoryCreateVM categoryCreateVM)
        {
            var category = new Category
            {
                Name = categoryCreateVM.Name
            };

            _categoryRepository.Add(category);
        }

        public async Task DeleteAsync(int id)
        {
           var category = await _categoryRepository.GetAll().FirstOrDefaultAsync(s => s.Id == id);
            if(category == null)
            {
                throw new Exception("Category tapılmadı");
            }

            _categoryRepository.Delete(category);
        }

        public async Task<Category> DetailAsync(int id)
        {
            var category = await _categoryRepository.GetAll()
                 .Where(s => s.Id == id)
                 .Select(s => new Category
                 {
                     Id = s.Id,
                     Name = s.Name
                 })
                 .FirstOrDefaultAsync();

            if (category == null)
            {
                throw new Exception("Category tapılmadı");
            }

            return category;
        }

        public async Task EditAsync(int id, CategoryEditVM categoryEditVM)
        {
            var category =await _categoryRepository.GetAll().FirstOrDefaultAsync(s => s.Id == id);

            if (category == null)
            {
                throw new Exception("Category tapılmadı");
            }

            category.Name = categoryEditVM.Name;

            _categoryRepository.Update(category);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAll().ToListAsync();
            return categories.Select(s => new Category
            {
                Id = s.Id,
                Name= s.Name
            }).ToList();
        }
    }
}
