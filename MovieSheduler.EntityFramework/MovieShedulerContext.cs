using System.Data.Common;
using System.Data.Entity;
using MovieSheduler.Domain.Cinema;
using MovieSheduler.Domain.Movie;
using MovieSheduler.Domain.SheduleRecord;
using MovieSheduler.EntityFramework.Configuration;

namespace MovieSheduler.EntityFramework
{
    public class MovieShedulerContext : DbContext
    {
        public MovieShedulerContext()
            : base("name=MovieShedulerContext")
        {
        }

        public MovieShedulerContext(DbConnection connection)
            : base(connection, true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SheduleRecordConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Cinema> Cinemas { get; set; }
        public virtual DbSet<SheduleRecord> SheduleRecords { get; set; }
    }
}