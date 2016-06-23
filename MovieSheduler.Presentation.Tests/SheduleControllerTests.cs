using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Moq;
using MovieSheduler.Application.Cinema;
using MovieSheduler.Application.Cinema.Dtos;
using MovieSheduler.Application.Infrastructure;
using MovieSheduler.Application.Movie;
using MovieSheduler.Application.Movie.Dtos;
using MovieSheduler.Application.SheduleRecord;
using MovieSheduler.Application.SheduleRecord.Dtos;
using MovieSheduler.Presentation.Controllers;
using MovieSheduler.Presentation.Core.Messager;
using MovieSheduler.Presentation.Models;
using Shouldly;
using Xunit;

namespace MovieSheduler.Presentation.Tests
{
    public class SheduleControllerTests
    {
        [Fact]
        public async Task Add_Should_Return_Redirect_To_Add()
        {
            //Arrange     
            var notifer = new Mock<INotifier>();
            var sheduleRecordSevice = new Mock<ISheduleRecordService>();
            var movieService = new Mock<IMovieService>();
            var cinemaService = new Mock<ICinemaService>();

            var controller = new SheduleController(sheduleRecordSevice.Object, movieService.Object, cinemaService.Object, notifer.Object);
            controller.ModelState.AddModelError("test", "test");

            //Act
            var result = (RedirectToRouteResult)(await controller.Add(new AddSheduleRecord()));

            //Assert            
            result.RouteValues["action"].ShouldBe("Add");
            notifer.Verify(n => n.AddMessage(MessageType.Danger, It.IsAny<string>()));
        }

        [Fact]
        public async Task Add_Should_Return_View()
        {
            //Arrange     
            var notifer = new Mock<INotifier>();
            var sheduleRecordSevice = new Mock<ISheduleRecordService>();
            sheduleRecordSevice.Setup(s => s.RecordExist(It.IsAny<RecordExistInput>())).Returns(Task.FromResult(true));

            var movieService = new Mock<IMovieService>();
            movieService.Setup(m => m.GetAllMovies()).Returns(Task.FromResult(new GetAllMoviesOutput(new List<MovieDto>())));

            var cinemaService = new Mock<ICinemaService>();
            cinemaService.Setup(m => m.GetAllCinema()).Returns(Task.FromResult(new GetAllCinemaOutput(new List<CinemaDto>())));

            var controller = new SheduleController(sheduleRecordSevice.Object, movieService.Object, cinemaService.Object, notifer.Object);
            var record = new AddSheduleRecord
            {
                SeansList = new List<TimeSpan>
                {
                    new TimeSpan(11, 0, 0),
                    new TimeSpan(11, 0, 0)
                }
            };

            //Act
            var result = await controller.Add(record);

            //Assert            
            result.ShouldBeOfType<ViewResult>();
        }

        [Fact]
        public async Task Add_Should_Return_Redirect_To_Index()
        {
            //Arrange     
            var notifer = new Mock<INotifier>();

            var validationDictionary = Mock.Of<IValidationDictionary>(v => v.IsValid);

            var sheduleRecordSevice = new Mock<ISheduleRecordService>();
            sheduleRecordSevice.Setup(s => s.AddRecord(It.IsAny<AddRecordInput>())).Returns(validationDictionary);
            sheduleRecordSevice.Setup(s => s.RecordExist(It.IsAny<RecordExistInput>())).Returns(Task.FromResult(true));

            var movieService = new Mock<IMovieService>();
            var cinemaService = new Mock<ICinemaService>();

            var controller = new SheduleController(sheduleRecordSevice.Object, movieService.Object, cinemaService.Object, notifer.Object);

            var record = new AddSheduleRecord
            {
                SeansList = new List<TimeSpan>
                {
                    new TimeSpan(11, 0, 0),
                    new TimeSpan(12, 0, 0)
                }
            };

            //Act
            var result = (RedirectToRouteResult)(await controller.Add(record));

            //Assert            
            result.RouteValues["action"].ShouldBe("Index");
            notifer.Verify(n => n.AddMessage(MessageType.Success, It.IsAny<string>()));
        }

        [Fact]
        public async Task Add_Should_Return_Not_Found()
        {
            //Arrange     
            var notifer = new Mock<INotifier>();

            var sheduleRecordSevice = new Mock<ISheduleRecordService>();
            sheduleRecordSevice.Setup(s => s.RecordExist(It.IsAny<RecordExistInput>())).Returns(Task.FromResult(false));

            var movieService = new Mock<IMovieService>();
            var cinemaService = new Mock<ICinemaService>();

            var controller = new SheduleController(sheduleRecordSevice.Object, movieService.Object, cinemaService.Object, notifer.Object);

            //Act
            var result = await controller.Add(new AddSheduleRecord());

            //Assert 
            result.ShouldBeOfType<HttpNotFoundResult>();
        }

        [Fact]
        public async Task EditGet_Should_Return_Not_Found()
        {
            //Arrange     
            var notifer = new Mock<INotifier>();

            var sheduleRecordSevice = new Mock<ISheduleRecordService>();
            sheduleRecordSevice.Setup(s => s.RecordExist(It.IsAny<RecordExistInput>())).Returns(Task.FromResult(false));

            var movieService = new Mock<IMovieService>();
            var cinemaService = new Mock<ICinemaService>();

            var controller = new SheduleController(sheduleRecordSevice.Object, movieService.Object, cinemaService.Object, notifer.Object);

            //Act
            var result = await controller.Edit(1, 1, DateTime.Now);

            //Assert 
            result.ShouldBeOfType<HttpNotFoundResult>();
        }

        [Fact]
        public async Task EditGet_Should_Return_View()
        {
            //Arrange     
            var notifer = new Mock<INotifier>();

            var sheduleRecordSevice = new Mock<ISheduleRecordService>();
            sheduleRecordSevice.Setup(s => s.RecordExist(It.IsAny<RecordExistInput>())).Returns(Task.FromResult(true));
            sheduleRecordSevice.Setup(s => s.GetSeansList(It.IsAny<GetSeansonsInput>()))
                .Returns(Task.FromResult((IReadOnlyCollection<TimeSpan>)new List<TimeSpan>().AsReadOnly()));

            var movieService = new Mock<IMovieService>();
            movieService.Setup(m => m.GetMovieById(It.IsAny<int>())).Returns(Task.FromResult(new MovieDto()));

            var cinemaService = new Mock<ICinemaService>();
            cinemaService.Setup(m => m.GetCinemaById(It.IsAny<int>())).Returns(Task.FromResult(new CinemaDto()));

            var controller = new SheduleController(sheduleRecordSevice.Object, movieService.Object, cinemaService.Object, notifer.Object);

            //Act
            var result = await controller.Edit(1, 1, DateTime.Now);

            //Assert 
            result.ShouldBeOfType<ViewResult>();
        }

        [Fact]
        public async Task EditPost_Should_Return_Redirect_To_Edit()
        {
            //Arrange     
            var notifer = new Mock<INotifier>();
            var sheduleRecordSevice = new Mock<ISheduleRecordService>();
            var movieService = new Mock<IMovieService>();
            var cinemaService = new Mock<ICinemaService>();

            var controller = new SheduleController(sheduleRecordSevice.Object, movieService.Object, cinemaService.Object, notifer.Object);
            controller.ModelState.AddModelError("test", "test");

            //Act
            var result = (RedirectToRouteResult)(await controller.Edit(new EditSheduleRecord()));

            //Assert            
            result.RouteValues["action"].ShouldBe("Edit");
            notifer.Verify(n => n.AddMessage(MessageType.Danger, It.IsAny<string>()));
        }

        [Fact]
        public async Task EditPost_Should_Return_Not_Found()
        {
            //Arrange     
            var notifer = new Mock<INotifier>();

            var sheduleRecordSevice = new Mock<ISheduleRecordService>();
            sheduleRecordSevice.Setup(s => s.RecordExist(It.IsAny<RecordExistInput>())).Returns(Task.FromResult(false));

            var movieService = new Mock<IMovieService>();
            var cinemaService = new Mock<ICinemaService>();

            var controller = new SheduleController(sheduleRecordSevice.Object, movieService.Object, cinemaService.Object, notifer.Object);

            //Act
            var result = await controller.Edit(new EditSheduleRecord());

            //Assert 
            result.ShouldBeOfType<HttpNotFoundResult>();
        }

        [Fact]
        public async Task EditPost_Should_Return_View()
        {
            //Arrange     
            var notifer = new Mock<INotifier>();
            var sheduleRecordSevice = new Mock<ISheduleRecordService>();
            sheduleRecordSevice.Setup(s => s.RecordExist(It.IsAny<RecordExistInput>())).Returns(Task.FromResult(true));

            var movieService = new Mock<IMovieService>();
            movieService.Setup(m => m.GetMovieById(It.IsAny<int>())).Returns(Task.FromResult(new MovieDto()));

            var cinemaService = new Mock<ICinemaService>();
            cinemaService.Setup(m => m.GetCinemaById(It.IsAny<int>())).Returns(Task.FromResult(new CinemaDto()));

            var controller = new SheduleController(sheduleRecordSevice.Object, movieService.Object, cinemaService.Object, notifer.Object);
            var record = new EditSheduleRecord
            {
                SeansList = new List<TimeSpan>
                {
                    new TimeSpan(11, 0, 0),
                    new TimeSpan(11, 0, 0)
                }
            };

            //Act
            var result = await controller.Edit(record);

            //Assert            
            result.ShouldBeOfType<ViewResult>();
        }

        [Fact]
        public async Task EditPost_Should_Return_Redirect_To_Index()
        {
            //Arrange     
            var notifer = new Mock<INotifier>();

            var validationDictionary = Mock.Of<IValidationDictionary>(v => v.IsValid);

            var sheduleRecordSevice = new Mock<ISheduleRecordService>();
            sheduleRecordSevice.Setup(s => s.EditRecord(It.IsAny<EditRecordInput>())).Returns(Task.FromResult(validationDictionary));
            sheduleRecordSevice.Setup(s => s.RecordExist(It.IsAny<RecordExistInput>())).Returns(Task.FromResult(true));

            var movieService = new Mock<IMovieService>();
            var cinemaService = new Mock<ICinemaService>();

            var controller = new SheduleController(sheduleRecordSevice.Object, movieService.Object, cinemaService.Object, notifer.Object);

            var record = new EditSheduleRecord
            {
                SeansList = new List<TimeSpan>
                {
                    new TimeSpan(11, 0, 0),
                    new TimeSpan(12, 0, 0)
                }
            };

            //Act
            var result = (RedirectToRouteResult)(await controller.Edit(record));

            //Assert            
            result.RouteValues["action"].ShouldBe("Index");
            notifer.Verify(n => n.AddMessage(MessageType.Success, It.IsAny<string>()));
        }

        [Fact]
        public async Task Delete_Should_Return_Not_Found()
        {
            //Arrange     
            var notifer = new Mock<INotifier>();

            var sheduleRecordSevice = new Mock<ISheduleRecordService>();
            sheduleRecordSevice.Setup(s => s.RecordExist(It.IsAny<RecordExistInput>())).Returns(Task.FromResult(false));

            var movieService = new Mock<IMovieService>();
            var cinemaService = new Mock<ICinemaService>();

            var controller = new SheduleController(sheduleRecordSevice.Object, movieService.Object, cinemaService.Object, notifer.Object);

            //Act
            var result = await controller.Delete(1, 1, DateTime.Now);

            //Assert 
            result.ShouldBeOfType<HttpNotFoundResult>();
        }

        [Fact]
        public async Task Delete_Should_Return_Redirect_To_Index()
        {
            //Arrange     
            var notifer = new Mock<INotifier>();

            var validationDictionary = Mock.Of<IValidationDictionary>(v => v.IsValid);

            var sheduleRecordSevice = new Mock<ISheduleRecordService>();
            sheduleRecordSevice.Setup(s => s.RecordExist(It.IsAny<RecordExistInput>())).Returns(Task.FromResult(true));
            sheduleRecordSevice.Setup(s => s.DeleteRecords(It.IsAny<DeleteRecordsInput>())).Returns(Task.FromResult(validationDictionary));

            var movieService = new Mock<IMovieService>();
            var cinemaService = new Mock<ICinemaService>();

            var controller = new SheduleController(sheduleRecordSevice.Object, movieService.Object, cinemaService.Object, notifer.Object);

            //Act
            var result = (RedirectToRouteResult)(await controller.Delete(1, 1, DateTime.Now));

            //Assert            
            result.RouteValues["action"].ShouldBe("Index");
            notifer.Verify(n => n.AddMessage(MessageType.Success, It.IsAny<string>()));
        }


        [Fact]
        public async Task Delete_Should_Return_Redirect_View()
        {
            //Arrange     
            var notifer = new Mock<INotifier>();

            var validationDictionary = new Mock<IValidationDictionary>();
            validationDictionary.Setup(v => v.IsValid).Returns(false);
            validationDictionary.Setup(v => v.Errors).Returns(new Dictionary<string, string>());

            var sheduleRecordSevice = new Mock<ISheduleRecordService>();
            sheduleRecordSevice.Setup(s => s.RecordExist(It.IsAny<RecordExistInput>())).Returns(Task.FromResult(true));
            sheduleRecordSevice.Setup(s => s.DeleteRecords(It.IsAny<DeleteRecordsInput>())).Returns(Task.FromResult(validationDictionary.Object));

            sheduleRecordSevice.Setup(s => s.GetSeansList(It.IsAny<GetSeansonsInput>()))
                .Returns(Task.FromResult((IReadOnlyCollection<TimeSpan>)new List<TimeSpan>().AsReadOnly()));

            var movieService = new Mock<IMovieService>();
            movieService.Setup(m => m.GetMovieById(It.IsAny<int>())).Returns(Task.FromResult(new MovieDto()));

            var cinemaService = new Mock<ICinemaService>();
            cinemaService.Setup(m => m.GetCinemaById(It.IsAny<int>())).Returns(Task.FromResult(new CinemaDto()));

            var controller = new SheduleController(sheduleRecordSevice.Object, movieService.Object, cinemaService.Object, notifer.Object);

            //Act
            var result = await controller.Delete(1, 1, DateTime.Now);

            //Assert            
            result.ShouldBeOfType<ViewResult>();
        }
    }
}
