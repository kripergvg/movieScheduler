using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MovieSheduler.Application.Infrastructure;
using MovieSheduler.Application.Movie.Dtos;
using MovieSheduler.Domain.Infrastructure;
using MovieSheduler.Domain.Movie;

namespace MovieSheduler.Application.Movie
{
    public class MovieService : BaseService, IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository, IUnitOfWorkFactory unitOfWorkFactory, IValidationDictionary validationDictionary)
            : base(unitOfWorkFactory, validationDictionary)
        {
            _movieRepository = movieRepository;
        }

        public async Task<GetAllMoviesOutput> GetAllMovies()
        {
            using (UnitOfWorkFactory.Create())
            {
                return new GetAllMoviesOutput
                {
                    Movies = Mapper.Map<IReadOnlyCollection<MovieDto>>(await _movieRepository.GetAllMoviesAsync())
                };
            }
        }

        public async Task<MovieDto> GetMovieById(int movieId)
        {
            using (UnitOfWorkFactory.Create())
            {
                return Mapper.Map<MovieDto>(await _movieRepository.GetByIdAsync(movieId));
            }
        }
    }
}
