using System.Threading.Tasks;

namespace MovieSheduler.Domain.Infrastructure
{
    public interface IRepository<TEntity, TPrimaryKey>
    {
        TEntity Update(TEntity entity);
        void Delete(TPrimaryKey id);
        void Delete(TEntity entity);
        TEntity Insert(TEntity entity);
        TEntity GetById(TPrimaryKey id);
        Task<TEntity> GetByIdAsync(TPrimaryKey id);
    }
}
