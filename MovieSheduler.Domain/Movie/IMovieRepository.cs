using System.Collections.Generic;
using System.Threading.Tasks;
using MovieSheduler.Domain.Infrastructure;

namespace MovieSheduler.Domain.Movie
{
    public interface IMovieRepository : IRepository<Movie, int>
    {
        /// <summary>
        /// Get all movies async
        /// </summary>
        /// <returns>Movies collection</returns>
        Task<IReadOnlyCollection<Movie>> GetAllMoviesAsync();
    }
}
