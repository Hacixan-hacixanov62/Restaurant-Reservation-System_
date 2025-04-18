

using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;

namespace Restaurant_Reservation_System_.DataAccess.Repositories
{
    public class QrCoderRepository : Repository<Reservation>, IQrCoderRepository
    {
        public QrCoderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
