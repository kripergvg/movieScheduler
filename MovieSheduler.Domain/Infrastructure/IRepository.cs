namespace MovieSheduler.Domain.Infrastructure
{
    public interface IRepository<TEntity, TPrimaryKey>
    {
        TEntity Update(TEntity entity);
        void Delete(TPrimaryKey id);
        TEntity Insert(TEntity entity);
        TEntity GetById(TPrimaryKey id);
    }
}
