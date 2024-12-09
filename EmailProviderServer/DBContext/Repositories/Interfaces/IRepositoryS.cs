//Includes

namespace EmailProviderServer.DBContext.Repositories.Base
{
    //------------------------------------------------------
    //	IRepositoryS
    //------------------------------------------------------
    public interface IRepositoryS<TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> All();

        IQueryable<TEntity> AllAsNoTracking();

        Task<TEntity> GetByID(int nId);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();

        Task<bool> CheckIfExists(int id);
    }
}
