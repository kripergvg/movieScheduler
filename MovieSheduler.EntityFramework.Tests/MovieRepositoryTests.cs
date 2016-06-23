using System.Threading.Tasks;
using MovieSheduler.Domain.Movie;
using MovieSheduler.EntityFramework.Repositories;
using Shouldly;
using Xunit;

namespace MovieSheduler.EntityFramework.Tests
{
    public class MovieRepositoryTests
    {
        [Fact]
        public async Task GetAllMoviesAsync_Should_Be_Empty()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var movieRepository = new MovieRepository(context);

            //Act
            var movieList = await movieRepository.GetAllMoviesAsync();

            //Assert            
            movieList.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetAllMoviesAsync_Should_Contains_Movie()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SaveChanges();
            var movieRepository = new MovieRepository(context);

            //Act
            var movieList = await movieRepository.GetAllMoviesAsync();

            //Assert            
            movieList.ShouldContain(c => c.Id == 1 && c.Name == "Касабланка");
        }

        [Fact]
        public void Update_Should_Update_Movie()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var movieRepository = new MovieRepository(context);
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SaveChanges();
            context.DeatachAllEntities();
            //Act
            movieRepository.Update(new Movie
            {
                Id = 1,
                Name = "Касабланка 2"
            });
            context.SaveChanges();
            var updatedMovie = context.Movies.Find(1);

            //Assert            
            updatedMovie.Name.ShouldBe("Касабланка 2");
        }

        [Fact]
        public void Insert_Should_Insert_Movie()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var movieRepository = new MovieRepository(context);

            //Act
            movieRepository.Insert(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SaveChanges();
            var newMovie = context.Movies.Find(1);

            //Assert            
            newMovie.Name.ShouldBe("Касабланка");
        }

        [Fact]
        public void GetById_Should_Return_Movie()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var movieRepository = new MovieRepository(context);
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SaveChanges();

            //Act
            var movie = movieRepository.GetById(1);

            //Assert            
            movie.ShouldNotBeNull();
        }

        [Fact]
        public void GetById_Should_Return_Null()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var movieRepository = new MovieRepository(context);
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SaveChanges();

            //Act
            var movie = movieRepository.GetById(2);

            //Assert            
            movie.ShouldBeNull();
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Null()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var movieRepository = new MovieRepository(context);
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SaveChanges();

            //Act
            var movie = await movieRepository.GetByIdAsync(2);

            //Assert            
            movie.ShouldBeNull();
        }

        [Fact]
        public void GetByIdAsync_Should_Return_Movie()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var movieRepository = new MovieRepository(context);
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SaveChanges();

            //Act
            var movie = movieRepository.GetByIdAsync(1);

            //Assert            
            movie.ShouldNotBeNull();
        }

        [Fact]
        public void Delete_Should_Delete_Movie()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var movieRepository = new MovieRepository(context);
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SaveChanges();

            //Act
            movieRepository.Delete(1);
            context.SaveChanges();

            var deletedMovie = context.Movies.Find(1);

            //Assert            
            deletedMovie.ShouldBeNull();
        }

        [Fact]
        public void Delete_Should_Not_Delete_Movie()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var movieRepository = new MovieRepository(context);
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SaveChanges();

            //Act
            movieRepository.Delete(2);
            context.SaveChanges();

            var deletedMovie = context.Movies.Find(1);

            //Assert            
            deletedMovie.ShouldNotBeNull();
        }
    }
}
