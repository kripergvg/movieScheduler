using System;
using System.Data.Entity;
using MovieSheduler.Domain.Infrastructure;

namespace MovieSheduler.EntityFramework.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly DbContextTransaction _transaction;

        public UnitOfWork(DbContext context)
        {
            _context = context;
            _transaction = context.Database.BeginTransaction();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
            }
        }
    }
}
