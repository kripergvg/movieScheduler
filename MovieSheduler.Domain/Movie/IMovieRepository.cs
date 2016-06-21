using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSheduler.Domain.Movie
{
    public interface IMovieRepository
    {
        Task<IReadOnlyCollection<Movie>> GetAllMoviesAsync();
    }
}
