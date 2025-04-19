using Microsoft.EntityFrameworkCore.Query;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Core.Entittes.Comman;
using System.Linq.Expressions;

namespace Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity> CreateAsync(TEntity entity);
        IQueryable<TEntity> GetFilter(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null); // Filtirle Olunmus melumatlari geri qaytarmaq
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null); // Tek bir obyekt getirmek
        Task<TEntity?> GetAsync(int id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null); // Obyektin olub olmamagini yoxlayir
        Task<int> SaveChangesAsync(); 
        IQueryable<TEntity> OrderBy(IQueryable<TEntity> query, Expression<Func<TEntity, object>> expression); // Artan Qaydada siraya salmaq
        IQueryable<TEntity> OrderByDescending(IQueryable<TEntity> query, Expression<Func<TEntity, object>> expression); // Azalan Qaydada siraya salmaq

        // Asagdakilar Message ye aidir
        Task<Chat?> GetChatWithUsersAndMessagesAsync(int chatId, string userId);
        Task<Message> AddMessageAsync(Message message);
        Task<List<Chat>> GetUserChatsAsync(string userId);
    }
}
