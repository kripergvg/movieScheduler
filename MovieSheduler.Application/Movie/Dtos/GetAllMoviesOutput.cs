using System.Collections.Generic;

namespace MovieSheduler.Application.Movie.Dtos
{
    public class GetAllMoviesOutput
    {
        public IReadOnlyCollection<MovieDto> Movies { get; set; }
    }
}
