using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using MovieSheduler.Domain;
using MovieSheduler.Domain.Infrastructure;

namespace MovieSheduler.EntityFramework.Infrastructure
{
    public abstract class RepositoryBase<TContext, TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TContext : DbContext
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected RepositoryBase(TContext dbContext)
        {
            Context = dbContext;
        }

        protected TContext Context { get; set; }

        protected DbSet<TEntity> Table => Context.Set<TEntity>();

        public virtual TEntity Insert(TEntity entity)
        {
            return Table.Add(entity);
        }

        public virtual TEntity GetById(TPrimaryKey id)
        {
            TEntity entity = GetFromLocal(id);
            if (entity == null)
            {
                entity = Table.FirstOrDefault(ent => EqualityComparer<TPrimaryKey>.Default.Equals(ent.Id, id));
            }
            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual void Delete(TPrimaryKey id)
        {
            TEntity entity = GetFromLocal(id);
            if (entity == null)
            {
                entity = Table.FirstOrDefault(ent => EqualityComparer<TPrimaryKey>.Default.Equals(ent.Id, id));
                if (entity == null)
                    return;
            }
            Table.Remove(entity);
        }

        private void AttachIfNot(TEntity entity)
        {
            var entry = Context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }

        private TEntity GetFromLocal(TPrimaryKey id)
        {
            var entry = Context.ChangeTracker.Entries()
                .FirstOrDefault(ent =>
                       ent.Entity is TEntity &&
                       EqualityComparer<TPrimaryKey>.Default.Equals(id, ((TEntity)ent.Entity).Id));

            return entry?.Entity as TEntity;
        }
    }
}
