using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MovieSheduler.Application.SheduleRecord;
using MovieSheduler.Application.SheduleRecord.Dtos;
using MovieSheduler.Domain.SheduleRecord;
using Shouldly;
using Xunit;

namespace MovieSheduler.Application.Tests
{
    public class SheduleRecordServiceTests : ServiceTestBase
    {
        [Fact]
        public void GetShedule_Should_Return_Empty_Collection()
        {
            //Arrange
            var sheduleDate = DateTime.Now;

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(c => c.GetSheduleByDateAsync(sheduleDate))
                .Returns(Task.FromResult((IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>)new List<Domain.SheduleRecord.SheduleRecord>().AsReadOnly()));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var sheduleRecords = sheduleRecordService.GetShedule(sheduleDate);

            //Assert  
            sheduleRecords.Result.SheduleRecords.ShouldBeEmpty();
        }

        [Fact]
        public void GetShedule_Should_Return_Not_Empty_Collection()
        {
            //Arrange
            var sheduleDate = DateTime.Now;

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(c => c.GetSheduleByDateAsync(sheduleDate))
                .Returns(Task.FromResult((IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>)new List<Domain.SheduleRecord.SheduleRecord>
                {
                     new Domain.SheduleRecord.SheduleRecord
                    {
                        Id = 1,
                        Cinema = new Domain.Cinema.Cinema
                        {
                            Id = 1,
                            Name = "Звезда"
                        },
                        CinemaId = 1,
                        Date = DateTime.Now,
                        Movie = new Domain.Movie.Movie
                        {
                            Id = 1,
                            Name = "Касабланка"
                        },
                        MovieId = 1
                    }
                }.AsReadOnly()));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var sheduleRecords = sheduleRecordService.GetShedule(sheduleDate);

            //Assert  
            sheduleRecords.Result.SheduleRecords.ShouldNotBeEmpty();
        }

        [Fact]
        public void GetShedule_Should_Contains_SheduleRecord()
        {
            //Arrange
            var sheduleDate = DateTime.Now;

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(c => c.GetSheduleByDateAsync(sheduleDate))
                .Returns(Task.FromResult((IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>)new List<Domain.SheduleRecord.SheduleRecord>
                {
                    new Domain.SheduleRecord.SheduleRecord
                    {
                        Id = 1,
                        Cinema = new Domain.Cinema.Cinema
                        {
                            Id = 1,
                            Name = "Звезда"
                        },
                        CinemaId = 1,
                        Date = DateTime.Now,
                        Movie = new Domain.Movie.Movie
                        {
                            Id = 1,
                            Name = "Касабланка"
                        },
                        MovieId = 1
                    }
                }.AsReadOnly()));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var sheduleRecords = sheduleRecordService.GetShedule(sheduleDate);

            //Assert  
            sheduleRecords.Result.SheduleRecords.ShouldContain(r => r.SheduleRecordId == 1);
        }


        [Fact]
        public async Task GetShedule_Should_Call_UnitOfWork()
        {
            //Arrange
            var sheduleDate = DateTime.Now;

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(c => c.GetSheduleByDateAsync(sheduleDate))
                .Returns(Task.FromResult((IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>)new List<Domain.SheduleRecord.SheduleRecord>().AsReadOnly()));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            await sheduleRecordService.GetShedule(sheduleDate);

            //Assert  
            UnitOfWorkFactoryMock.Verify(uf => uf.Create());
            UnitOfWorkMock.Verify(uf => uf.Dispose());
        }

        [Fact]
        public void GetFirstAvailableDate_Should_Return_Null()
        {
            //Arrange
            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(c => c.GetFirstAvailableDateAsync())
                .Returns(Task.FromResult((DateTime?)null));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var firstAvailableDate = sheduleRecordService.GetFirstAvailableDate();

            //Assert  
            firstAvailableDate.Result.ShouldBeNull();
        }


        [Fact]
        public void GetFirstAvailableDate_Should_Return_Date()
        {
            //Arrange
            var firstAvailableDateFromRepository = DateTime.Now;
            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(c => c.GetFirstAvailableDateAsync())
                .Returns(Task.FromResult((DateTime?)firstAvailableDateFromRepository));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var firstAvailableDate = sheduleRecordService.GetFirstAvailableDate();

            //Assert  
            firstAvailableDate.Result.ShouldBe(firstAvailableDateFromRepository);
        }

        [Fact]
        public async Task GetFirstAvailableDate_Should_Call_UnitOfWork()
        {
            //Arrange
            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(c => c.GetFirstAvailableDateAsync())
                .Returns(Task.FromResult((DateTime?)null));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            await sheduleRecordService.GetFirstAvailableDate();

            //Assert  
            UnitOfWorkFactoryMock.Verify(uf => uf.Create());
            UnitOfWorkMock.Verify(uf => uf.Dispose());
        }

        [Fact]
        public void AddRecord_Should_Be_Not_Valid()
        {
            //Arrange
            var sheduleRecordRepositoryMock = Mock.Of<ISheduleRecordRepository>(r => r.ExistByProperties(It.IsAny<Domain.SheduleRecord.SheduleRecord>()));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            sheduleRecordService.AddRecord(new AddRecordInput(1, 1, new List<TimeSpan>
            {
                new TimeSpan(1, 30, 0)
            }.AsReadOnly(), DateTime.Now));

            //Assert  
            ValidationDictionaryMock.Verify(d => d.AddError("time", It.IsAny<string>()));
            //result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void AddRecord_Should_Be_Valid()
        {
            //Arrange
            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.ExistByProperties(It.IsAny<Domain.SheduleRecord.SheduleRecord>())).Returns(false);
            sheduleRecordRepositoryMock.Setup(r => r.AddRecords(It.IsAny<IEnumerable<Domain.SheduleRecord.SheduleRecord>>()));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            sheduleRecordService.AddRecord(new AddRecordInput(1, 1, new List<TimeSpan>
            {
                new TimeSpan(1, 30, 0)
            }.AsReadOnly(), DateTime.Now));

            //Assert  
            ValidationDictionaryMock.Verify(d => d.AddError("time", It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void AddRecord_Should_Call_Add_Repository()
        {
            //Arrange
            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.ExistByProperties(It.IsAny<Domain.SheduleRecord.SheduleRecord>())).Returns(false);
            sheduleRecordRepositoryMock.Setup(r => r.AddRecords(It.IsAny<IEnumerable<Domain.SheduleRecord.SheduleRecord>>()));
            ValidationDictionaryMock.Setup(d => d.IsValid).Returns(true);

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            sheduleRecordService.AddRecord(new AddRecordInput(1, 1, new List<TimeSpan>
            {
                new TimeSpan(1, 30, 0)
            }.AsReadOnly(), DateTime.Now));

            //Assert  
            sheduleRecordRepositoryMock.Verify(r => r.AddRecords(It.IsAny<IEnumerable<Domain.SheduleRecord.SheduleRecord>>()));
        }

        [Fact]
        public void AddRecord_Should_Call_UnitOfWork()
        {
            //Arrange
            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.ExistByProperties(It.IsAny<Domain.SheduleRecord.SheduleRecord>())).Returns(false);
            sheduleRecordRepositoryMock.Setup(r => r.AddRecords(It.IsAny<IEnumerable<Domain.SheduleRecord.SheduleRecord>>()));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            sheduleRecordService.AddRecord(new AddRecordInput(1, 1, new List<TimeSpan>
            {
                new TimeSpan(1, 30, 0)
            }.AsReadOnly(), DateTime.Now));

            //Assert  
            UnitOfWorkFactoryMock.Verify(uf => uf.Create());
            UnitOfWorkMock.Verify(uf => uf.Dispose());
        }

        [Fact]
        public void AddRecord_Should_Call_SaveChanges()
        {
            //Arrange
            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.ExistByProperties(It.IsAny<Domain.SheduleRecord.SheduleRecord>())).Returns(false);
            sheduleRecordRepositoryMock.Setup(r => r.AddRecords(It.IsAny<IEnumerable<Domain.SheduleRecord.SheduleRecord>>()));
            ValidationDictionaryMock.Setup(d => d.IsValid).Returns(true);

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            sheduleRecordService.AddRecord(new AddRecordInput(1, 1, new List<TimeSpan>
            {
                new TimeSpan(1, 30, 0)
            }.AsReadOnly(), DateTime.Now));

            //Assert  
            UnitOfWorkMock.Verify(u => u.SaveChanges(), Times.AtLeastOnce);
        }

        [Fact]
        public void GetAvailableDates_Should_Return_Two_Dates()
        {
            //Arrange
            var availableDate1 = DateTime.Now;
            var availableDate2 = availableDate1.AddDays(1);

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.GetAvailableDates()).Returns(Task.FromResult((IReadOnlyCollection<DateTime>)new List<DateTime>
            {
                availableDate1,
                availableDate2
            }.AsReadOnly()));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var availableDates = sheduleRecordService.GetAvailableDates();

            //Assert  
            availableDates.Result.Count.ShouldBe(2);
            availableDates.Result.ShouldContain(availableDate1);
        }

        [Fact]
        public void GetAvailableDates_Should_Return_Empty_Collection()
        {
            //Arrange
            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.GetAvailableDates()).Returns(Task.FromResult((IReadOnlyCollection<DateTime>)new List<DateTime>().AsReadOnly()));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var availableDates = sheduleRecordService.GetAvailableDates();

            //Assert  
            availableDates.Result.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetAvailableDates_Should_Call_UnitOfWork()
        {
            //Arrange
            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.GetAvailableDates()).Returns(Task.FromResult((IReadOnlyCollection<DateTime>)new List<DateTime>().AsReadOnly()));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            await sheduleRecordService.GetAvailableDates();

            //Assert  
            UnitOfWorkFactoryMock.Verify(uf => uf.Create());
            UnitOfWorkMock.Verify(uf => uf.Dispose());
        }

        [Fact]
        public void GetSeansList_Should_Return_Time_From_Date()
        {
            //Arrange
            var records = new List<Domain.SheduleRecord.SheduleRecord>
            {
                new Domain.SheduleRecord.SheduleRecord
                {
                    Date = new DateTime(2016, 1, 1, 13, 00, 00),
                },
                new Domain.SheduleRecord.SheduleRecord
                {
                    Date = new DateTime(2016, 1, 1, 14, 00, 00),
                }
            }.AsReadOnly();

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.GetRecords(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(Task.FromResult((IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>)records));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var seansList = sheduleRecordService.GetSeansList(new GetSeansonsInput(1, 1, DateTime.Now));

            //Assert  
            seansList.Result.ShouldContain(new TimeSpan(13, 0, 0));
            seansList.Result.ShouldContain(new TimeSpan(14, 0, 0));
        }

        [Fact]
        public async Task GetSeansList_Should_Call_UnitOfWork()
        {
            //Arrange
            var records = new List<Domain.SheduleRecord.SheduleRecord>
            {
                new Domain.SheduleRecord.SheduleRecord
                {
                    Date = new DateTime(2016, 1, 1, 13, 00, 00),
                },
                new Domain.SheduleRecord.SheduleRecord
                {
                    Date = new DateTime(2016, 1, 1, 14, 00, 00),
                }
            }.AsReadOnly();

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.GetRecords(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(Task.FromResult((IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>)records));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            await sheduleRecordService.GetSeansList(new GetSeansonsInput(1, 1, DateTime.Now));

            //Assert  
            UnitOfWorkFactoryMock.Verify(uf => uf.Create());
            UnitOfWorkMock.Verify(uf => uf.Dispose());
        }

        [Fact]
        public async Task DeleteRecords_Should_Call_Delete()
        {
            //Arrange
            var records = new List<Domain.SheduleRecord.SheduleRecord>
            {
                new Domain.SheduleRecord.SheduleRecord
                {
                    Date = new DateTime(2016, 1, 1, 13, 00, 00),
                },
                new Domain.SheduleRecord.SheduleRecord
                {
                    Date = new DateTime(2016, 1, 1, 14, 00, 00),
                }
            }.AsReadOnly();

            var sheduleDate = DateTime.Now;

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.GetRecords(1, 1, sheduleDate))
                .Returns(Task.FromResult((IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>)records));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            await sheduleRecordService.DeleteRecords(new DeleteRecordsInput(1, 1, sheduleDate));

            //Assert  
            sheduleRecordRepositoryMock.Verify();
        }

        [Fact]
        public async Task DeleteRecords_Should_Not_Add_Error()
        {
            //Arrange
            var records = new List<Domain.SheduleRecord.SheduleRecord>
            {
                new Domain.SheduleRecord.SheduleRecord
                {
                    Date = new DateTime(2016, 1, 1, 13, 00, 00),
                },
                new Domain.SheduleRecord.SheduleRecord
                {
                    Date = new DateTime(2016, 1, 1, 14, 00, 00),
                }
            }.AsReadOnly();

            var sheduleDate = DateTime.Now;

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.GetRecords(1, 1, sheduleDate))
                .Returns(Task.FromResult((IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>)records));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            await sheduleRecordService.DeleteRecords(new DeleteRecordsInput(1, 1, sheduleDate));

            //Assert  
            ValidationDictionaryMock.Verify(d => d.AddError(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task DeleteRecords_Should_Call_SaveChange()
        {
            //Arrange
            var records = new List<Domain.SheduleRecord.SheduleRecord>
            {
                new Domain.SheduleRecord.SheduleRecord
                {
                    Date = new DateTime(2016, 1, 1, 13, 00, 00),
                },
                new Domain.SheduleRecord.SheduleRecord
                {
                    Date = new DateTime(2016, 1, 1, 14, 00, 00),
                }
            }.AsReadOnly();

            var sheduleDate = DateTime.Now;

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.GetRecords(1, 1, sheduleDate))
                .Returns(Task.FromResult((IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>)records));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            await sheduleRecordService.DeleteRecords(new DeleteRecordsInput(1, 1, sheduleDate));

            //Assert  
            UnitOfWorkMock.Verify(d => d.SaveChanges(), Times.AtLeastOnce);
        }

        [Fact]
        public async Task DeleteRecords_Should_Call_UnitOfWork()
        {
            //Arrange
            var records = new List<Domain.SheduleRecord.SheduleRecord>
            {
                new Domain.SheduleRecord.SheduleRecord
                {
                    Date = new DateTime(2016, 1, 1, 13, 00, 00),
                },
                new Domain.SheduleRecord.SheduleRecord
                {
                    Date = new DateTime(2016, 1, 1, 14, 00, 00),
                }
            }.AsReadOnly();

            var sheduleDate = DateTime.Now;

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.GetRecords(1, 1, sheduleDate))
                .Returns(Task.FromResult((IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>)records));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            await sheduleRecordService.DeleteRecords(new DeleteRecordsInput(1, 1, sheduleDate));

            //Assert  
            UnitOfWorkFactoryMock.Verify(uf => uf.Create());
            UnitOfWorkMock.Verify(uf => uf.Dispose());
        }

        [Fact]
        public void RecordExist_Should_Return_True()
        {
            //Arrange
            var sheduleDate = DateTime.Now;

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.RecordExist(1, 1, sheduleDate))
                .Returns(Task.FromResult(true));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var recordExist = sheduleRecordService.RecordExist(new RecordExistInput(1, 1, sheduleDate));

            //Assert  
            recordExist.Result.ShouldBeTrue();
        }

        [Fact]
        public void RecordExist_Should_Return_False()
        {
            //Arrange
            var sheduleDate = DateTime.Now;

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.RecordExist(1, 1, sheduleDate))
                .Returns(Task.FromResult(false));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            var recordExist = sheduleRecordService.RecordExist(new RecordExistInput(1, 1, sheduleDate));

            //Assert  
            recordExist.Result.ShouldBeFalse();
        }

        [Fact]
        public async Task RecordExist_Should_Call_UnitOfWork()
        {
            //Arrange
            var sheduleDate = DateTime.Now;

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.RecordExist(1, 1, sheduleDate))
                .Returns(Task.FromResult(false));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);

            //Act
            await sheduleRecordService.RecordExist(new RecordExistInput(1, 1, sheduleDate));

            //Assert  
            UnitOfWorkFactoryMock.Verify(uf => uf.Create());
            UnitOfWorkMock.Verify(uf => uf.Dispose());
        }

        [Fact]
        public async Task EditRecord_Should_DeleteRecords()
        {
            //Arrange
            var sheduleDate = DateTime.Now;

            var recordToDelete = new Domain.SheduleRecord.SheduleRecord
            {
                Date = new DateTime(sheduleDate.Year, sheduleDate.Month, sheduleDate.Day, 14, 00, 00),
            };
            var records = new List<Domain.SheduleRecord.SheduleRecord>
            {
                new Domain.SheduleRecord.SheduleRecord
                {
                    Date = new DateTime(sheduleDate.Year, sheduleDate.Month, sheduleDate.Day, 13, 00, 00),
                },
                recordToDelete

            }.AsReadOnly();

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.GetRecords(1, 1, sheduleDate))
                .Returns(Task.FromResult((IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>)records));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);
            var timeList = new List<TimeSpan>
            {
                new TimeSpan(13, 0, 0)
            }.AsReadOnly();

            //Act
            await sheduleRecordService.EditRecord(new EditRecordInput(1, 1, sheduleDate, timeList));

            //Assert  
            sheduleRecordRepositoryMock.Verify(r => r.Delete(It.Is<Domain.SheduleRecord.SheduleRecord>(d => d.Date == recordToDelete.Date)));
        }

        [Fact]
        public async Task EditRecord_Should_AddRecords()
        {
            //Arrange
            var sheduleDate = DateTime.Now;

            var recordToDelete = new Domain.SheduleRecord.SheduleRecord
            {
                Date = new DateTime(sheduleDate.Year, sheduleDate.Month, sheduleDate.Day, 14, 00, 00),
            };
            var records = new List<Domain.SheduleRecord.SheduleRecord>
            {
                new Domain.SheduleRecord.SheduleRecord
                {
                    Date = new DateTime(sheduleDate.Year, sheduleDate.Month, sheduleDate.Day, 13, 00, 00),
                },
                recordToDelete

            }.AsReadOnly();

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.GetRecords(1, 1, sheduleDate))
                .Returns(Task.FromResult((IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>)records));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);
            var timeList = new List<TimeSpan>
            {
                new TimeSpan(13, 0, 0),
                new TimeSpan(14, 0, 0),
                new TimeSpan(15, 0, 0)
            }.AsReadOnly();

            //Act
            await sheduleRecordService.EditRecord(new EditRecordInput(1, 1, sheduleDate, timeList));

            //Assert  
            sheduleRecordRepositoryMock.Verify(
                r => r.AddRecords(It.Is<IEnumerable<Domain.SheduleRecord.SheduleRecord>>(d => d.Any(addRecord => addRecord.Date == sheduleDate.Add(new TimeSpan(15, 0, 0))))));
        }

        [Fact]
        public async Task EditRecord_Should_Call_UnitOfWork()
        {
            //Arrange
            var sheduleDate = DateTime.Now;

            var recordToDelete = new Domain.SheduleRecord.SheduleRecord
            {
                Date = new DateTime(sheduleDate.Year, sheduleDate.Month, sheduleDate.Day, 14, 00, 00),
            };
            var records = new List<Domain.SheduleRecord.SheduleRecord>
            {
                new Domain.SheduleRecord.SheduleRecord
                {
                    Date = new DateTime(sheduleDate.Year, sheduleDate.Month, sheduleDate.Day, 13, 00, 00),
                },
                recordToDelete

            }.AsReadOnly();

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.GetRecords(1, 1, sheduleDate))
                .Returns(Task.FromResult((IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>)records));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);
            var timeList = new List<TimeSpan>
            {
                new TimeSpan(13, 0, 0),
                new TimeSpan(14, 0, 0),
                new TimeSpan(15, 0, 0)
            }.AsReadOnly();

            //Act
            await sheduleRecordService.EditRecord(new EditRecordInput(1, 1, sheduleDate, timeList));

            //Assert  
            UnitOfWorkFactoryMock.Verify(uf => uf.Create());
            UnitOfWorkMock.Verify(uf => uf.Dispose());
        }

        [Fact]
        public async Task EditRecord_Should_Call_SaveChange()
        {
            //Arrange
            var sheduleDate = DateTime.Now;

            var recordToDelete = new Domain.SheduleRecord.SheduleRecord
            {
                Date = new DateTime(sheduleDate.Year, sheduleDate.Month, sheduleDate.Day, 14, 00, 00),
            };
            var records = new List<Domain.SheduleRecord.SheduleRecord>
            {
                new Domain.SheduleRecord.SheduleRecord
                {
                    Date = new DateTime(sheduleDate.Year, sheduleDate.Month, sheduleDate.Day, 13, 00, 00),
                },
                recordToDelete

            }.AsReadOnly();

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.GetRecords(1, 1, sheduleDate))
                .Returns(Task.FromResult((IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>)records));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);
            var timeList = new List<TimeSpan>
            {
                new TimeSpan(13, 0, 0),
                new TimeSpan(14, 0, 0),
                new TimeSpan(15, 0, 0)
            }.AsReadOnly();

            //Act
            await sheduleRecordService.EditRecord(new EditRecordInput(1, 1, sheduleDate, timeList));

            //Assert  
            UnitOfWorkMock.Verify(uf => uf.SaveChanges());
        }

        [Fact]
        public async Task EditRecord_Should_Not_Add_Error()
        {
            //Arrange
            var sheduleDate = DateTime.Now;

            var recordToDelete = new Domain.SheduleRecord.SheduleRecord
            {
                Date = new DateTime(sheduleDate.Year, sheduleDate.Month, sheduleDate.Day, 14, 00, 00),
            };
            var records = new List<Domain.SheduleRecord.SheduleRecord>
            {
                new Domain.SheduleRecord.SheduleRecord
                {
                    Date = new DateTime(sheduleDate.Year, sheduleDate.Month, sheduleDate.Day, 13, 00, 00),
                },
                recordToDelete

            }.AsReadOnly();

            var sheduleRecordRepositoryMock = new Mock<ISheduleRecordRepository>();
            sheduleRecordRepositoryMock.Setup(r => r.GetRecords(1, 1, sheduleDate))
                .Returns(Task.FromResult((IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>)records));

            var sheduleRecordService = new SheduleRecordService(sheduleRecordRepositoryMock.Object, UnitOfWorkFactoryMock.Object, ValidationDictionaryMock.Object);
            var timeList = new List<TimeSpan>
            {
                new TimeSpan(13, 0, 0),
                new TimeSpan(14, 0, 0),
                new TimeSpan(15, 0, 0)
            }.AsReadOnly();

            //Act
            await sheduleRecordService.EditRecord(new EditRecordInput(1, 1, sheduleDate, timeList));

            //Assert   
            ValidationDictionaryMock.Verify(d => d.AddError(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

    }
}
