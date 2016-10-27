using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MovieSheduler.Application.Movie;
using MovieSheduler.Domain.Movie;
using Shouldly;
using Xunit;

namespace MovieSheduler.Application.Tests
{
    public class MovieServiceTests : ServiceTestBase
    {
        [Fact]
        public void GetAllMovies_Should_Contains_Correct_Movie()
        {
            //Arrange
            var movieList = new List<Domain.Movie.Movie>
            {
                new Domain.Movie.Movie
                {
                    Id = 1,
                    Name = "Касабланка"
                },
                new Domain.Movie.Movie
                {
                    Id = 2,
                    Name = "Шрэк"
                }
            }.AsReadOnly();
            var movieRepositoryMock = new Mock<IMovieRepository>();
            movieRepositoryMock.Setup(c => c.GetAllMoviesAsync()).Returns(Task.FromResult((IReadOnlyCollection<Domain.Movie.Movie>) movieList));

            var movieService = new MovieService(movieRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var movieListFromService = movieService.GetAllMovies();

            //Assert            
            movieListFromService.Result.Movies.Count.ShouldBe(2);
            movieListFromService.Result.Movies.ShouldContain(c => c.Id == 1);
            movieListFromService.Result.Movies.ShouldContain(c => c.Id == 2);
            movieListFromService.Result.Movies.ShouldContain(c => c.Name == "Касабланка");
            movieListFromService.Result.Movies.ShouldContain(c => c.Name == "Шрэк");
            movieListFromService.Result.Movies.ShouldNotContain(c => c.Id == 3);
        }

        [Fact]
        public void GetAllMovies_Should_Not_Return_Null()
        {
            //Arrange
            var movieRepositoryMock = new Mock<IMovieRepository>();
            movieRepositoryMock.Setup(c => c.GetAllMoviesAsync()).Returns(Task.FromResult((IReadOnlyCollection<Domain.Movie.Movie>)new List<Domain.Movie.Movie>()));
            var movieService = new MovieService(movieRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var movieListFromService = movieService.GetAllMovies();

            //Assert  
            movieListFromService.Result.ShouldNotBeNull();
            movieListFromService.Result.Movies.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAllMovies_Should_Call_UnitOfWork()
        {
            //Arrange
            var movieRepositoryMock = new Mock<IMovieRepository>();
            movieRepositoryMock.Setup(c => c.GetAllMoviesAsync()).Returns(Task.FromResult((IReadOnlyCollection<Domain.Movie.Movie>)new List<Domain.Movie.Movie>()));
            var movieService = new MovieService(movieRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            await movieService.GetAllMovies();

            //Assert  
            UnitOfWorkFactoryMock.Verify(uf => uf.Create());
            UnitOfWorkMock.Verify(uf => uf.Dispose());
        }

        [Fact]
        public void GetMovieById_Should_Return_Cinema()
        {
            //Arrange
            var movieRepositoryMock = new Mock<IMovieRepository>();
            movieRepositoryMock.Setup(c => c.GetByIdAsync(2)).Returns(Task.FromResult(new Domain.Movie.Movie
            {
                Id = 2,
                Name = "Касабланка"
            }));
            var movieService = new MovieService(movieRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var movie = movieService.GetMovieById(2);

            //Assert
            movie.Result.Name.ShouldBe("Касабланка");
            movie.Result.Id.ShouldBe(2);
        }

        [Fact]
        public void GetMovieById_Should_Return_Null()
        {
            //Arrange
            var movieRepositoryMock = new Mock<IMovieRepository>();
            movieRepositoryMock.Setup(c => c.GetByIdAsync(2)).Returns(Task.FromResult((Domain.Movie.Movie)null));
            var movieService = new MovieService(movieRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var movie = movieService.GetMovieById(2);

            //Assert  
            movie.Result.ShouldBeNull();
        }

        [Fact]
        public async Task GetMovieById_Should_Call_UnitOfWork()
        {
            //Arrange
            var movieRepositoryMock = new Mock<IMovieRepository>();
            movieRepositoryMock.Setup(c => c.GetByIdAsync(1)).Returns(Task.FromResult(new Domain.Movie.Movie
            {
                Id = 2,
                Name = "Касабланка"
            }));
            var movieService = new MovieService(movieRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            await movieService.GetMovieById(1);

            //Assert  
            UnitOfWorkFactoryMock.Verify(uf => uf.Create());
            UnitOfWorkMock.Verify(uf => uf.Dispose());
        }
    }

}
