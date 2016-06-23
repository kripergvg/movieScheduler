using System.Data.Entity.ModelConfiguration;
using MovieSheduler.Domain.Movie;

namespace MovieSheduler.EntityFramework.Configuration
{
    public class MovieConfiguration : EntityTypeConfiguration<Movie>
    {
        public MovieConfiguration()
        {
            Property(c => c.Name).HasMaxLength(255).IsRequired();
        }
    }
}
