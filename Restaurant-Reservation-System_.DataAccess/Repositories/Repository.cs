using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Restaurant_Reservation_System_.Core.Entittes.Comman;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using System.Linq.Expressions;

namespace Restaurant_Reservation_System_.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _context;
        private DbSet<TEntity> Table { get; set; } // service hissesinde _appDbContext in yerine Tbale isletmek ucun istifade olunur
        public Repository(AppDbContext context)
        {
            _context = context;
            Table = _context.Set<TEntity>();
        }
        
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var entityEntry = await Table.AddAsync(entity);

            return entityEntry.Entity;
        }

        public void Delete(TEntity entity)
        {
            Table.Remove(entity);
            _context.SaveChanges();
        }

        public IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            return Table;
        }

        public void Update(TEntity entity)
        {
            Table.Update(entity);
            _context.SaveChanges();
        }

        public IQueryable<TEntity> GetFilter(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            var query = _getQueryWithIncludes(include);
                
            query = query.Where(expression);

            return query;
        }

        public async  Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            var query = _getQueryWithIncludes(include);

            var entity = await query.FirstOrDefaultAsync(expression);

            return entity;
        }

        public async Task<TEntity?> GetAsync(int id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            var query = _getQueryWithIncludes(include);

            var entity = await query.FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            var query = _getQueryWithIncludes(include);

            var result = await query.AnyAsync(expression);

            return result;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> OrderBy(IQueryable<TEntity> query, Expression<Func<TEntity, object>> expression)
        {
            IQueryable<TEntity> result = query.OrderBy(expression);
            return result;
        }

        public IQueryable<TEntity> OrderByDescending(IQueryable<TEntity> query, Expression<Func<TEntity, object>> expression)
        {
            IQueryable<TEntity> result = query.OrderByDescending(expression);
            return result;
        }

        private IQueryable<TEntity> _getQueryWithIncludes(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            var query = Table.AsQueryable();

            if (include is { })
                query = include(query);

            return query;
        }
    }
}
