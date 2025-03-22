using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;

namespace Restaurant_Reservation_System_.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        private DbSet<TEntity> Table { get; set; } // service hissesinde _appDbContext in yerine Tbale isletmek ucun istifade olunur
        public Repository(AppDbContext context)
        {
            _context = context;
            Table = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            Table.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            Table.Remove(entity);
            _context.SaveChanges();
        }

        public IQueryable<TEntity> GetAll()
        {
            return Table;
        }

        public void Update(TEntity entity)
        {
            Table.Update(entity);
            _context.SaveChanges();
        }
    }
}
