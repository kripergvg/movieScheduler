using System;

namespace MovieSheduler.Domain.Infrastructure
{
    public interface IUnitOfWork: IDisposable
    {
        /// <summary>
        /// Save changes to source
        /// </summary>
        void SaveChanges();
    }
}
