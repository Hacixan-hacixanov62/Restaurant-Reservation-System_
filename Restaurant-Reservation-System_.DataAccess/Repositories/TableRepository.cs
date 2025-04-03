using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Reservation_System_.DataAccess.Repositories
{
    public class TableRepository : Repository<Table>, ITableRepository
    {
        public TableRepository(AppDbContext context) : base(context)
        {
        }
    }
}
