using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Repositories;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.ViewModels.CategoryVM;
using Restaurant_Reservation_System_.Service.ViewModels.IngrideantVM;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class IngridentService:IIngridentService
    {
        private readonly AppDbContext _context;
        private readonly IIngridentRepository _ıngridentRepository;
        public IngridentService(IIngridentRepository ıngridentRepository,AppDbContext context)
        {
            _ıngridentRepository = ıngridentRepository;
            _context = context;
        }

        public async Task CreateAsync(IngredientCreateVM ıngredientCreateVM)
        {
            var ingredient = new Ingredient
            {
                Name = ıngredientCreateVM.Name
            };

            _ıngridentRepository.Add(ingredient);
        }

        public async Task DeleteAsync(int id)
        {
            var ingredient = await _ıngridentRepository.GetAll().FirstOrDefaultAsync(s => s.Id == id);
            if (ingredient == null)
            {
                throw new Exception("Ingredient tapılmadı");
            }

            _ıngridentRepository.Delete(ingredient);
        }

        public async Task<Ingredient> DetailAsync(int id)
        {
            var ingredient = await _ıngridentRepository.GetAll()
                .Where(s => s.Id == id)
                .Select(s => new Ingredient
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .FirstOrDefaultAsync();

            if (ingredient == null)
            {
                throw new Exception("Ingredient tapılmadı");
            }

            return ingredient;
        }

        public async Task EditAsync(int id, IngredientEditVM ıngredientEditVM)
        {
            var ingredient = await _ıngridentRepository.GetAll().FirstOrDefaultAsync(s => s.Id == id);

            if (ingredient == null)
            {
                throw new Exception("Category tapılmadı");
            }

            ingredient.Name = ıngredientEditVM.Name;

            _ıngridentRepository.Update(ingredient);
        }

        public async Task<List<Ingredient>> GetAllAsync()
        {
            var ingredients = await _ıngridentRepository.GetAll().ToListAsync();
            return ingredients.Select(s => new Ingredient
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }
    }
}
