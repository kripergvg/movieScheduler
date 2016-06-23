using System.Threading.Tasks;
using MovieSheduler.Domain.Cinema;
using MovieSheduler.EntityFramework.Repositories;
using Shouldly;
using Xunit;

namespace MovieSheduler.EntityFramework.Tests
{
    public class CinemaRepositoryTests
    {
        [Fact]
        public async Task GetAllCinemaAsync_Should_Be_Empty()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var cinemRepository = new CinemaRepository(context);

            //Act
            var cinemList = await cinemRepository.GetAllCinemaAsync();

            //Assert            
            cinemList.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetAllCinemaAsync_Should_Contains_Cinema()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.SaveChanges();
            var cinemRepository = new CinemaRepository(context);

            //Act
            var cinemList = await cinemRepository.GetAllCinemaAsync();

            //Assert            
            cinemList.ShouldContain(c => c.Id == 1 && c.Name == "Звезда");
        }

        [Fact]
        public void Update_Should_Update_Cinema()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var cinemRepository = new CinemaRepository(context);
            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда" 
            });
            context.SaveChanges();
            context.DeatachAllEntities();
            //Act
            cinemRepository.Update(new Cinema
            {
                Id = 1,
                Name = "Звезда 2"
            });
            context.SaveChanges();
            var updatedCinema = context.Cinemas.Find(1);

            //Assert            
            updatedCinema.Name.ShouldBe("Звезда 2");
        }

        [Fact]
        public void Insert_Should_Insert_Cinema()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var cinemRepository = new CinemaRepository(context);

            //Act
            cinemRepository.Insert(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.SaveChanges();
            var newCinema = context.Cinemas.Find(1);

            //Assert            
            newCinema.Name.ShouldBe("Звезда");
        }

        [Fact]
        public void GetById_Should_Return_Cinema()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var cinemRepository = new CinemaRepository(context);
            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.SaveChanges();

            //Act
            var cinema = cinemRepository.GetById(1);

            //Assert            
            cinema.ShouldNotBeNull();
        }

        [Fact]
        public void GetById_Should_Return_Null()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var cinemRepository = new CinemaRepository(context);
            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.SaveChanges();

            //Act
            var cinema = cinemRepository.GetById(2);

            //Assert            
            cinema.ShouldBeNull();
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Null()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var cinemRepository = new CinemaRepository(context);
            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.SaveChanges();

            //Act
            var cinema = await cinemRepository.GetByIdAsync(2);

            //Assert            
            cinema.ShouldBeNull();
        }

        [Fact]
        public void GetByIdAsync_Should_Return_Cinema()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var cinemRepository = new CinemaRepository(context);
            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.SaveChanges();

            //Act
            var cinema = cinemRepository.GetByIdAsync(1);

            //Assert            
            cinema.ShouldNotBeNull();
        }

        [Fact]
        public void Delete_Should_Delete_Cinema()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var cinemRepository = new CinemaRepository(context);
            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.SaveChanges();

            //Act
            cinemRepository.Delete(1);
            context.SaveChanges();

            var deletedCinema = context.Cinemas.Find(1);

            //Assert            
            deletedCinema.ShouldBeNull();
        }

        [Fact]
        public void Delete_Should_Not_Delete_Cinema()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var cinemRepository = new CinemaRepository(context);
            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.SaveChanges();

            //Act
            cinemRepository.Delete(2);
            context.SaveChanges();

            var deletedCinema = context.Cinemas.Find(1);

            //Assert            
            deletedCinema.ShouldNotBeNull();
        }
    }
}
