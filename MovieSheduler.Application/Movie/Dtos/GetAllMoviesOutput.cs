using System;
using System.Collections.Generic;

namespace MovieSheduler.Application.Movie.Dtos
{
    public class GetAllMoviesOutput
    {
        public GetAllMoviesOutput(IReadOnlyCollection<MovieDto> movies)
        {
            if (movies == null)
                throw new ArgumentNullException(nameof(movies));

            Movies = movies;
        }
        public IReadOnlyCollection<MovieDto> Movies { get; set; }
    }
}
