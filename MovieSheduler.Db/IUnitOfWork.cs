using System;

namespace MovieSheduler.Db
{
    public interface IUnitOfWork: IDisposable
    {
        void SaveChanges();
    }
}
