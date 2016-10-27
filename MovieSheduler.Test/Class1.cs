//using System;
//using System.Data.Common;
//using System.Data.Entity.Migrations;
//using MovieSheduler.Domain.Cinema;
//using MovieSheduler.Domain.Movie;
//using MovieSheduler.Domain.SheduleRecord;
//using MovieSheduler.EntityFramework;

//namespace MovieSheduler.Test
//{
//    public class Class1
//    {
//        protected void Initialize()
//        {
//            DbConnection connection = Effort.DbConnectionFactory.CreateTransient();
//            var context = new MovieShedulerContext(connection);

//            var movie1 = new Movie { Name = "Побег из Шоушенка" };
//            var movie2 = new Movie { Name = "Зеленая миля" };
//            var movie3 = new Movie { Name = "Форрест Гамп" };
//            var movie4 = new Movie { Name = "Список Шиндлера" };
//            var movie5 = new Movie { Name = "1+1" };
//            var movie6 = new Movie { Name = "Начало" };
//            context.Movies.AddOrUpdate(c => c.Name, movie1, movie2, movie3, movie4, movie5, movie6);

//            var cinema1 = new Cinema { Name = "Звезда" };
//            var cinema2 = new Cinema { Name = "Маяк" };
//            var cinema3 = new Cinema { Name = "5 звезд" };
//            var cinema4 = new Cinema { Name = "Луксор" };
//            context.Cinemas.AddOrUpdate(c => c.Name, cinema1, cinema2, cinema3, cinema4);

//            var sheduleRecors = new[]
//            {
//                new SheduleRecord {Cinema = cinema1, Movie = movie1, Date = new DateTime(2016, 06, 21, 13, 00, 00)},
//                new SheduleRecord {Cinema = cinema1, Movie = movie1, Date = new DateTime(2016, 06, 21, 14, 00, 00)},
//                new SheduleRecord {Cinema = cinema1, Movie = movie1, Date = new DateTime(2016, 06, 21, 15, 00, 00)},
//                new SheduleRecord {Cinema = cinema1, Movie = movie1, Date = new DateTime(2016, 06, 21, 16, 00, 00)},
//                new SheduleRecord {Cinema = cinema1, Movie = movie1, Date = new DateTime(2016, 06, 21, 17, 00, 00)},
//                new SheduleRecord {Cinema = cinema1, Movie = movie1, Date = new DateTime(2016, 06, 21, 18, 00, 00)},


//                new SheduleRecord {Cinema = cinema1, Movie = movie2, Date = new DateTime(2016, 06, 21, 11, 00, 00)},
//                new SheduleRecord {Cinema = cinema1, Movie = movie2, Date = new DateTime(2016, 06, 21, 14, 00, 00)},
//                new SheduleRecord {Cinema = cinema1, Movie = movie2, Date = new DateTime(2016, 06, 21, 16, 00, 00)},
//                new SheduleRecord {Cinema = cinema1, Movie = movie2, Date = new DateTime(2016, 06, 21, 22, 00, 00)},


//                new SheduleRecord {Cinema = cinema1, Movie = movie3, Date = new DateTime(2016, 06, 21, 12, 00, 00)},
//                new SheduleRecord {Cinema = cinema1, Movie = movie3, Date = new DateTime(2016, 06, 21, 13, 00, 00)},
//                new SheduleRecord {Cinema = cinema1, Movie = movie3, Date = new DateTime(2016, 06, 21, 14, 00, 00)},
//                new SheduleRecord {Cinema = cinema1, Movie = movie3, Date = new DateTime(2016, 06, 21, 15, 00, 00)},


//                new SheduleRecord {Cinema = cinema1, Movie = movie4, Date = new DateTime(2016, 06, 21, 13, 00, 00)},
//                new SheduleRecord {Cinema = cinema1, Movie = movie4, Date = new DateTime(2016, 06, 21, 13, 30, 00)},
//                new SheduleRecord {Cinema = cinema1, Movie = movie4, Date = new DateTime(2016, 06, 21, 15, 00, 00)},
//                new SheduleRecord {Cinema = cinema1, Movie = movie4, Date = new DateTime(2016, 06, 21, 17, 00, 00)},



//                new SheduleRecord {Cinema = cinema2, Movie = movie4, Date = new DateTime(2016, 06, 21, 13, 00, 00)},
//                new SheduleRecord {Cinema = cinema2, Movie = movie4, Date = new DateTime(2016, 06, 21, 14, 00, 00)},
//                new SheduleRecord {Cinema = cinema2, Movie = movie4, Date = new DateTime(2016, 06, 21, 15, 00, 00)},
//                new SheduleRecord {Cinema = cinema2, Movie = movie4, Date = new DateTime(2016, 06, 21, 16, 00, 00)},
//                new SheduleRecord {Cinema = cinema2, Movie = movie4, Date = new DateTime(2016, 06, 21, 17, 00, 00)},
//                new SheduleRecord {Cinema = cinema2, Movie = movie4, Date = new DateTime(2016, 06, 21, 18, 00, 00)},


//                new SheduleRecord {Cinema = cinema2, Movie = movie5, Date = new DateTime(2016, 06, 21, 11, 00, 00)},
//                new SheduleRecord {Cinema = cinema2, Movie = movie5, Date = new DateTime(2016, 06, 21, 14, 00, 00)},
//                new SheduleRecord {Cinema = cinema2, Movie = movie5, Date = new DateTime(2016, 06, 21, 16, 00, 00)},
//                new SheduleRecord {Cinema = cinema2, Movie = movie5, Date = new DateTime(2016, 06, 21, 22, 00, 00)},


//                new SheduleRecord {Cinema = cinema2, Movie = movie6, Date = new DateTime(2016, 06, 21, 12, 00, 00)},
//                new SheduleRecord {Cinema = cinema2, Movie = movie6, Date = new DateTime(2016, 06, 21, 13, 00, 00)},
//                new SheduleRecord {Cinema = cinema2, Movie = movie6, Date = new DateTime(2016, 06, 21, 14, 00, 00)},
//                new SheduleRecord {Cinema = cinema2, Movie = movie6, Date = new DateTime(2016, 06, 21, 15, 00, 00)},


//                new SheduleRecord {Cinema = cinema2, Movie = movie1, Date = new DateTime(2016, 06, 21, 13, 00, 00)},
//                new SheduleRecord {Cinema = cinema2, Movie = movie1, Date = new DateTime(2016, 06, 21, 13, 30, 00)},
//                new SheduleRecord {Cinema = cinema2, Movie = movie1, Date = new DateTime(2016, 06, 21, 15, 00, 00)},
//                new SheduleRecord {Cinema = cinema2, Movie = movie1, Date = new DateTime(2016, 06, 21, 17, 00, 00)},



//                new SheduleRecord {Cinema = cinema3, Movie = movie4, Date = new DateTime(2016, 06, 21, 13, 00, 00)},
//                new SheduleRecord {Cinema = cinema3, Movie = movie4, Date = new DateTime(2016, 06, 21, 14, 00, 00)},
//                new SheduleRecord {Cinema = cinema3, Movie = movie4, Date = new DateTime(2016, 06, 21, 15, 00, 00)},
//                new SheduleRecord {Cinema = cinema3, Movie = movie4, Date = new DateTime(2016, 06, 21, 16, 00, 00)},
//                new SheduleRecord {Cinema = cinema3, Movie = movie4, Date = new DateTime(2016, 06, 21, 17, 00, 00)},
//                new SheduleRecord {Cinema = cinema3, Movie = movie4, Date = new DateTime(2016, 06, 21, 18, 00, 00)},


//                new SheduleRecord {Cinema = cinema3, Movie = movie5, Date = new DateTime(2016, 06, 21, 11, 00, 00)},
//                new SheduleRecord {Cinema = cinema3, Movie = movie5, Date = new DateTime(2016, 06, 21, 14, 00, 00)},
//                new SheduleRecord {Cinema = cinema3, Movie = movie5, Date = new DateTime(2016, 06, 21, 16, 00, 00)},
//                new SheduleRecord {Cinema = cinema3, Movie = movie5, Date = new DateTime(2016, 06, 21, 22, 00, 00)},


//                new SheduleRecord {Cinema = cinema3, Movie = movie6, Date = new DateTime(2016, 06, 21, 12, 00, 00)},
//                new SheduleRecord {Cinema = cinema3, Movie = movie6, Date = new DateTime(2016, 06, 21, 13, 00, 00)},
//                new SheduleRecord {Cinema = cinema3, Movie = movie6, Date = new DateTime(2016, 06, 21, 14, 00, 00)},
//                new SheduleRecord {Cinema = cinema3, Movie = movie6, Date = new DateTime(2016, 06, 21, 15, 00, 00)},


//                new SheduleRecord {Cinema = cinema3, Movie = movie1, Date = new DateTime(2016, 06, 21, 13, 00, 00)},
//                new SheduleRecord {Cinema = cinema3, Movie = movie1, Date = new DateTime(2016, 06, 21, 13, 30, 00)},
//                new SheduleRecord {Cinema = cinema3, Movie = movie1, Date = new DateTime(2016, 06, 21, 15, 00, 00)},
//                new SheduleRecord {Cinema = cinema3, Movie = movie1, Date = new DateTime(2016, 06, 21, 17, 00, 00)},
//            };
//            context.SheduleRecords.AddOrUpdate(sheduleRecors);
//            context.SaveChanges();
//        }
//    }
//}
