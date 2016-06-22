using System.Collections.Generic;
using System.Threading.Tasks;
using MovieSheduler.Domain.Infrastructure;

namespace MovieSheduler.Domain.Movie
{
    public interface IMovieRepository : IRepository<Movie, int>
    {
        Task<IReadOnlyCollection<Movie>> GetAllMoviesAsync();
    }
}
