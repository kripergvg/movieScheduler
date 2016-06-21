using System.Threading.Tasks;
using MovieSheduler.Application.Movie.Dtos;

namespace MovieSheduler.Application.Movie
{
    public interface IMovieService
    {
        Task<GetAllMoviesOutput> GetAllMovies();
    }
}
