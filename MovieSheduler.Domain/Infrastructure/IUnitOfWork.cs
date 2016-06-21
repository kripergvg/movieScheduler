using System;

namespace MovieSheduler.Domain.Infrastructure
{
    public interface IUnitOfWork: IDisposable
    {
        void SaveChanges();
    }
}
