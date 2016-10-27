using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MovieSheduler.Application.Cinema;
using MovieSheduler.Domain.Cinema;
using Shouldly;
using Xunit;

namespace MovieSheduler.Application.Tests
{
    public class CinemaServiceTests : ServiceTestBase
    {
        [Fact]
        public void GetAllCinema_Should_Contains_Correct_Cinema()
        {
            //Arrange        
            var cinemaList = new List<Domain.Cinema.Cinema>
            {
                new Domain.Cinema.Cinema
                {
                    Id = 1,
                    Name = "5 звезд"
                },
                new Domain.Cinema.Cinema
                {
                    Id = 2,
                    Name = "Звезда"
                }
            }.AsReadOnly();
            var сinemaRepositoryMock = new Mock<ICinemaRepository>();
            сinemaRepositoryMock.Setup(c => c.GetAllCinemaAsync()).Returns(Task.FromResult((IReadOnlyCollection<Domain.Cinema.Cinema>)cinemaList));

            var cinemaService = new CinemaService(сinemaRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var cinemaListFromService = cinemaService.GetAllCinema();

            //Assert            
            cinemaListFromService.Result.Cinemas.Count.ShouldBe(2);
            cinemaListFromService.Result.Cinemas.ShouldContain(c => c.Id == 1);
            cinemaListFromService.Result.Cinemas.ShouldContain(c => c.Id == 2);
            cinemaListFromService.Result.Cinemas.ShouldContain(c => c.Name == "5 звезд");
            cinemaListFromService.Result.Cinemas.ShouldContain(c => c.Name == "Звезда");
            cinemaListFromService.Result.Cinemas.ShouldNotContain(c => c.Id == 3);
        }

        [Fact]
        public void GetAllCinema_Should_Not_Return_Null()
        {
            //Arrange
            var сinemaRepositoryMock = new Mock<ICinemaRepository>();
            сinemaRepositoryMock.Setup(c => c.GetAllCinemaAsync()).Returns(Task.FromResult((IReadOnlyCollection<Domain.Cinema.Cinema>)new List<Domain.Cinema.Cinema>()));
            var cinemaService = new CinemaService(сinemaRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var cinemaListFromService = cinemaService.GetAllCinema();

            //Assert  
            cinemaListFromService.Result.ShouldNotBeNull();
            cinemaListFromService.Result.Cinemas.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAllCinema_Should_Call_UnitOfWork()
        {
            //Arrange
            var сinemaRepositoryMock = new Mock<ICinemaRepository>();
            сinemaRepositoryMock.Setup(c => c.GetAllCinemaAsync()).Returns(Task.FromResult((IReadOnlyCollection<Domain.Cinema.Cinema>)new List<Domain.Cinema.Cinema>()));
            var cinemaService = new CinemaService(сinemaRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            await cinemaService.GetAllCinema();

            //Assert  
            UnitOfWorkFactoryMock.Verify(uf => uf.Create());
            UnitOfWorkMock.Verify(uf => uf.Dispose());
        }

        [Fact]
        public void GetCinemaById_Should_Return_Cinema()
        {
            //Arrange
            var сinemaRepositoryMock = new Mock<ICinemaRepository>();
            сinemaRepositoryMock.Setup(c => c.GetByIdAsync(2)).Returns(Task.FromResult(new Domain.Cinema.Cinema
            {
                Id = 2,
                Name = "Звезда"
            }));
            var cinemaService = new CinemaService(сinemaRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var cinema = cinemaService.GetCinemaById(2);

            //Assert
            cinema.Result.Name.ShouldBe("Звезда");
            cinema.Result.Id.ShouldBe(2);
        }

        [Fact]
        public void GetCinemaById_Should_Return_Null()
        {
            //Arrange
            var сinemaRepositoryMock = new Mock<ICinemaRepository>();
            сinemaRepositoryMock.Setup(c => c.GetByIdAsync(2)).Returns(Task.FromResult((Domain.Cinema.Cinema)null));
            var cinemaService = new CinemaService(сinemaRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var cinema = cinemaService.GetCinemaById(2);

            //Assert  
            cinema.Result.ShouldBeNull();
        }

        [Fact]
        public async Task GetCinemaById_Should_Call_UnitOfWork()
        {
            //Arrange
            var сinemaRepositoryMock = new Mock<ICinemaRepository>();
            сinemaRepositoryMock.Setup(c => c.GetByIdAsync(1)).Returns(Task.FromResult(new Domain.Cinema.Cinema
            {
                Id = 2,
                Name = "Звезда"
            }));
            var cinemaService = new CinemaService(сinemaRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            await cinemaService.GetCinemaById(1);

            //Assert  
            UnitOfWorkFactoryMock.Verify(uf => uf.Create());
            UnitOfWorkMock.Verify(uf => uf.Dispose());
        }
    }
}
