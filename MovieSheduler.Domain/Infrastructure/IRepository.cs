using System.Threading.Tasks;

namespace MovieSheduler.Domain.Infrastructure
{
    public interface IRepository<TEntity, in TPrimaryKey>
    {
        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <returns>Updated entity</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">Id of entity</param>
        void Delete(TPrimaryKey id);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        /// <returns>Inserted entity</returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Id of entity</param>
        /// <returns></returns>
        TEntity GetById(TPrimaryKey id);

        /// <summary>
        /// Get entity by id async
        /// </summary>
        /// <param name="id">Id of entity</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(TPrimaryKey id);
    }
}
