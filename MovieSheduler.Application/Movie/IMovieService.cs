using System.Threading.Tasks;
using MovieSheduler.Application.Movie.Dtos;

namespace MovieSheduler.Application.Movie
{
    public interface IMovieService
    {
        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns>GetAllMoviesOutput with movie list</returns>
        Task<GetAllMoviesOutput> GetAllMovies();

        /// <summary>
        /// Get movie by id
        /// </summary>
        /// <param name="movieId">Movie id</param>
        /// <returns>Movie</returns>
        Task<MovieDto> GetMovieById(int movieId);
    }
}
