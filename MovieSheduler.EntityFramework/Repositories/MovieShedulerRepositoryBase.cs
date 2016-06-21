using MovieSheduler.Domain;
using MovieSheduler.EntityFramework.Infrastructure;

namespace MovieSheduler.EntityFramework.Repositories
{
    public abstract class MovieShedulerRepositoryBase<TEntity, TPrimaryKey> : RepositoryBase<MovieShedulerContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected MovieShedulerRepositoryBase(MovieShedulerContext dbContext)
            : base(dbContext)
        {
        }
    }
}
