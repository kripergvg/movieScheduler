using System;
using System.Collections.Generic;

namespace MovieSheduler.Domain
{
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }

        /// <summary>
        /// Checks if this entity is transient (it has not an Id).
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// True, if this entity is transient
        /// </returns>
        public virtual bool IsTransient()
        {
            return EqualityComparer<TPrimaryKey>.Default.Equals(this.Id, default(TPrimaryKey));
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity<TPrimaryKey>))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            Entity<TPrimaryKey> entity = (Entity<TPrimaryKey>)obj;
            if (IsTransient() && entity.IsTransient())
                return false;
            Type type1 = GetType();
            Type type2 = entity.GetType();
            if (!type1.IsAssignableFrom(type2) && !type2.IsAssignableFrom(type1))
                return false;
            return Id.Equals(entity.Id);
        }
    }
}
