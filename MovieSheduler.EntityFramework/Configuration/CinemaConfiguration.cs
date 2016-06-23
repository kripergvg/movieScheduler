using System.Data.Entity.ModelConfiguration;
using MovieSheduler.Domain.Cinema;

namespace MovieSheduler.EntityFramework.Configuration
{
    public class CinemaConfiguration:EntityTypeConfiguration<Cinema>
    {
        public CinemaConfiguration()
        {
            Property(c => c.Name).HasMaxLength(255).IsRequired();
        }
    }
}
