using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieSheduler.Domain.Cinema;
using MovieSheduler.Domain.Movie;
using MovieSheduler.Domain.SheduleRecord;
using MovieSheduler.EntityFramework.Repositories;
using MovieSheduler.EntityFramework.Repositories.SheduleRecord;
using Shouldly;
using Xunit;

namespace MovieSheduler.EntityFramework.Tests
{
    public class SheduleRecordRepositoryTests
    {
        [Fact]
        public void Update_Should_Update_SheduleRecord()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var sheduleRecordRepository = new SheduleRecordRepository(context);

            var oldDate = DateTime.Now;
            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SheduleRecords.Add(new SheduleRecord
            {
                Id = 1,
                CinemaId = 1,
                MovieId = 1,
                Date = oldDate
            });
            context.SaveChanges();
            context.DeatachAllEntities();

            //Act
            var newDate = oldDate.AddDays(1);
            sheduleRecordRepository.Update(new SheduleRecord
            {
                Id = 1,
                CinemaId = 1,
                MovieId = 1,
                Date = newDate
            });
            context.SaveChanges();
            var updatedSheduleRecord = context.SheduleRecords.Find(1);

            //Assert            
            updatedSheduleRecord.Date.ShouldBe(newDate);
        }

        [Fact]
        public void Insert_Should_Insert_SheduleRecord()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var sheduleRecordRepository = new SheduleRecordRepository(context);

            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });

            //Act
            sheduleRecordRepository.Insert(new SheduleRecord
            {
                Id = 1,
                CinemaId = 1,
                MovieId = 1,
                Date = DateTime.Now
            });
            context.SaveChanges();
            var newSheduleRecord = context.SheduleRecords.Find(1);

            //Assert            
            newSheduleRecord.ShouldNotBeNull();
        }

        [Fact]
        public void GetById_Should_Return_SheduleRecord()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var sheduleRecordRepository = new SheduleRecordRepository(context);

            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SheduleRecords.Add(new SheduleRecord
            {
                Id = 1,
                CinemaId = 1,
                MovieId = 1,
                Date = DateTime.Now
            });
            context.SaveChanges();

            //Act
            var sheduleRecord = sheduleRecordRepository.GetById(1);

            //Assert            
            sheduleRecord.ShouldNotBeNull();
        }

        [Fact]
        public void GetById_Should_Return_Null()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var sheduleRecordRepository = new SheduleRecordRepository(context);

            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SheduleRecords.Add(new SheduleRecord
            {
                Id = 1,
                CinemaId = 1,
                MovieId = 1,
                Date = DateTime.Now
            });
            context.SaveChanges();

            //Act
            var sheduleRecord = sheduleRecordRepository.GetById(2);

            //Assert            
            sheduleRecord.ShouldBeNull();
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Null()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var sheduleRecordRepository = new SheduleRecordRepository(context);

            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SheduleRecords.Add(new SheduleRecord
            {
                Id = 1,
                CinemaId = 1,
                MovieId = 1,
                Date = DateTime.Now
            });
            context.SaveChanges();

            //Act
            var sheduleRecord = await sheduleRecordRepository.GetByIdAsync(2);

            //Assert            
            sheduleRecord.ShouldBeNull();
        }

        [Fact]
        public void GetByIdAsync_Should_Return_Movie()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var sheduleRecordRepository = new SheduleRecordRepository(context);

            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SheduleRecords.Add(new SheduleRecord
            {
                Id = 1,
                CinemaId = 1,
                MovieId = 1,
                Date = DateTime.Now
            });
            context.SaveChanges();

            //Act
            var sheduleRecord = sheduleRecordRepository.GetByIdAsync(1);

            //Assert            
            sheduleRecord.ShouldNotBeNull();
        }

        [Fact]
        public void Delete_Should_Delete_Movie()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var sheduleRecordRepository = new SheduleRecordRepository(context);

            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SheduleRecords.Add(new SheduleRecord
            {
                Id = 1,
                CinemaId = 1,
                MovieId = 1,
                Date = DateTime.Now
            });
            context.SaveChanges();

            //Act
            sheduleRecordRepository.Delete(1);
            context.SaveChanges();

            var deletedSheduleRecord = context.SheduleRecords.Find(1);

            //Assert            
            deletedSheduleRecord.ShouldBeNull();
        }

        [Fact]
        public void Delete_Should_Not_Delete_Movie()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var sheduleRecordRepository = new SheduleRecordRepository(context);

            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SheduleRecords.Add(new SheduleRecord
            {
                Id = 1,
                CinemaId = 1,
                MovieId = 1,
                Date = DateTime.Now
            });
            context.SaveChanges();

            //Act
            sheduleRecordRepository.Delete(2);
            context.SaveChanges();

            var deletedSheduleRecord = context.SheduleRecords.Find(1);

            //Assert            
            deletedSheduleRecord.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetFirstAvailableDateAsync_Should_Return_Null()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var sheduleRecordRepository = new SheduleRecordRepository(context);

            //Act
            var date = await sheduleRecordRepository.GetFirstAvailableDateAsync();

            //Assert            
            date.ShouldBeNull();
        }

        [Fact]
        public async Task GetFirstAvailableDateAsync_Should_Not_Be_Null()
        {
            //Arrange 
            var firstDate = DateTime.Now;

            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var sheduleRecordRepository = new SheduleRecordRepository(context);

            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SheduleRecords.Add(new SheduleRecord
            {
                Id = 1,
                CinemaId = 1,
                MovieId = 1,
                Date = firstDate
            });
            context.SaveChanges();

            //Act
            var date = await sheduleRecordRepository.GetFirstAvailableDateAsync();

            //Assert            
            date.ShouldBe(firstDate);
        }

        [Fact]
        public async Task GetSheduleByDateAsync_Should_Return_Records()
        {
            //Arrange 
            var recordDate = DateTime.Now;

            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var sheduleRecordRepository = new SheduleRecordRepository(context);

            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SheduleRecords.Add(new SheduleRecord
            {
                Id = 1,
                CinemaId = 1,
                MovieId = 1,
                Date = recordDate
            });
            context.SheduleRecords.Add(new SheduleRecord
            {
                Id = 1,
                CinemaId = 1,
                MovieId = 1,
                Date = recordDate.AddDays(-1)
            });
            context.SaveChanges();

            //Act
            var records = await sheduleRecordRepository.GetSheduleByDateAsync(recordDate);

            //Assert    
            records.ShouldNotBeEmpty();
            records.ShouldAllBe(r => r.Date == recordDate);
        }

        [Fact]
        public void AddRecords_Should_Add_Records()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var sheduleRecordRepository = new SheduleRecordRepository(context);

            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SaveChanges();

            var records = new List<SheduleRecord>
            {
                new SheduleRecord
                {
                    Id = 1,
                    CinemaId = 1,
                    MovieId = 1,
                    Date = DateTime.Now
                },
                new SheduleRecord
                {
                    Id = 2,
                    CinemaId = 1,
                    MovieId = 1,
                    Date = DateTime.Now.AddDays(1)
                }
            };

            //Act
            sheduleRecordRepository.AddRecords(records);
            context.SaveChanges();

            //Assert   
            context.SheduleRecords.ToList().ShouldContain(r => r.Id == 1);
            context.SheduleRecords.ToList().ShouldContain(r => r.Id == 2);
        }

        [Fact]
        public void ExistByProperties_Should_Return_Exist()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var sheduleRecordRepository = new SheduleRecordRepository(context);

            var sheduleRecord = new SheduleRecord
            {
                Id = 1,
                CinemaId = 1,
                MovieId = 1,
                Date = DateTime.Now
            };
            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SheduleRecords.Add(sheduleRecord);
            context.SaveChanges();


            //Act
            var exist = sheduleRecordRepository.ExistByProperties(sheduleRecord);

            //Assert   
            exist.ShouldBeTrue();
        }

        [Fact]
        public void ExistByProperties_Should_Return_Not_Exist()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var sheduleRecordRepository = new SheduleRecordRepository(context);

            var sheduleRecord = new SheduleRecord
            {
                Id = 1,
                CinemaId = 1,
                MovieId = 1,
                Date = DateTime.Now
            };
            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });
            context.SheduleRecords.Add(sheduleRecord);
            context.SaveChanges();


            //Act
            var exist = sheduleRecordRepository.ExistByProperties(new SheduleRecord
            {
                Id = 1,
                CinemaId = 1,
                MovieId = 1,
                Date = DateTime.Now.AddDays(1)
            });

            //Assert   
            exist.ShouldBeFalse();
        }

        [Fact]
        public async Task GetAvailableDates_Should_Return_Distinct_Dates()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var sheduleRecordRepository = new SheduleRecordRepository(context);

            var firstDate = DateTime.Now;
            var secondDate = firstDate.AddDays(1);

            var records = new List<SheduleRecord>
            {
                new SheduleRecord
                {
                    Id = 1,
                    CinemaId = 1,
                    MovieId = 1,
                    Date = firstDate
                },
                new SheduleRecord
                {
                    Id = 1,
                    CinemaId = 2,
                    MovieId = 1,
                    Date = firstDate
                },
                new SheduleRecord
                {
                    Id = 1,
                    CinemaId = 1,
                    MovieId = 1,
                    Date = secondDate
                }
            };
            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.Cinemas.Add(new Cinema
            {
                Id = 2,
                Name = "Звезда 2"
            });
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });

            context.SheduleRecords.AddRange(records);
            context.SaveChanges();


            //Act
            var availableDates =await sheduleRecordRepository.GetAvailableDates();

            //Assert   
            availableDates.Count.ShouldBe(2);
            availableDates.ShouldContain(d => d.Year == firstDate.Year && d.Month == firstDate.Month && d.Day == firstDate.Day);
            availableDates.ShouldContain(d => d.Year == secondDate.Year && d.Month == secondDate.Month && d.Day == secondDate.Day);
        }

        [Fact]
        public async Task GetRecords_Should_Return_Records()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var sheduleRecordRepository = new SheduleRecordRepository(context);

            var firstDate = DateTime.Now;
            var secondDate = firstDate.AddHours(1);

            var records = new List<SheduleRecord>
            {
                new SheduleRecord
                {
                    Id = 1,
                    CinemaId = 1,
                    MovieId = 1,
                    Date = firstDate
                },
                new SheduleRecord
                {
                    Id = 1,
                    CinemaId = 2,
                    MovieId = 1,
                    Date = firstDate
                },
                new SheduleRecord
                {
                    Id = 1,
                    CinemaId = 1,
                    MovieId = 1,
                    Date = secondDate
                }
            };
            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.Cinemas.Add(new Cinema
            {
                Id = 2,
                Name = "Звезда 2"
            });
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });

            context.SheduleRecords.AddRange(records);
            context.SaveChanges();


            //Act
            var recordsFromRepository = await sheduleRecordRepository.GetRecords(1, 1, firstDate);

            //Assert   
            recordsFromRepository.Count.ShouldBe(2);
        }

        [Fact]
        public async Task RecordExist_Should_Return_Records()
        {
            //Arrange 
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new MovieShedulerContext(connection);
            var sheduleRecordRepository = new SheduleRecordRepository(context);

            var firstDate = DateTime.Now;
            var secondDate = firstDate.AddHours(1);

            var records = new List<SheduleRecord>
            {
                new SheduleRecord
                {
                    Id = 1,
                    CinemaId = 1,
                    MovieId = 1,
                    Date = firstDate
                },
                new SheduleRecord
                {
                    Id = 1,
                    CinemaId = 2,
                    MovieId = 1,
                    Date = firstDate
                },
                new SheduleRecord
                {
                    Id = 1,
                    CinemaId = 1,
                    MovieId = 1,
                    Date = secondDate
                }
            };
            context.Cinemas.Add(new Cinema
            {
                Id = 1,
                Name = "Звезда"
            });
            context.Cinemas.Add(new Cinema
            {
                Id = 2,
                Name = "Звезда 2"
            });
            context.Movies.Add(new Movie
            {
                Id = 1,
                Name = "Касабланка"
            });

            context.SheduleRecords.AddRange(records);
            context.SaveChanges();


            //Act
            var recordsFromRepository = await sheduleRecordRepository.GetRecords(1, 1, firstDate);

            //Assert   
            recordsFromRepository.Count.ShouldBe(2);
        }
    }
}
