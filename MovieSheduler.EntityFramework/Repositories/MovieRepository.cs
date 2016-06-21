using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using MovieSheduler.Domain.Movie;

namespace MovieSheduler.EntityFramework.Repositories
{
    public class MovieRepository : MovieShedulerRepositoryBase<Movie, int>, IMovieRepository
    {
        public MovieRepository(MovieShedulerContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IReadOnlyCollection<Movie>> GetAllMoviesAsync()
        {
            return (await Table.ToListAsync()).AsReadOnly();
        }
    }
}
