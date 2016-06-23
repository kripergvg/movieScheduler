using System.Data.Entity;

namespace MovieSheduler.EntityFramework.Tests
{
    public static class DbContextExtensions
    {
        public static void DeatachAllEntities(this DbContext context)
        {
            foreach (var dbEntityEntry in context.ChangeTracker.Entries())
            {
                context.Entry(dbEntityEntry.Entity).State = EntityState.Detached;
            }
        }
    }
}
