using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;

namespace Restaurant_Reservation_System_.DataAccess.Repositories
{
    public class IngridientRepository : Repository<Ingredient>, IIngridentRepository
    {
        public IngridientRepository(AppDbContext context) : base(context)
        {
        }
    }
}
