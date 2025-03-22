namespace Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Add(TEntity entity);

    }
}
