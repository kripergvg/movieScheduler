using System;
using System.Collections.Generic;
using System.Linq;
using MovieSheduler.Application.SheduleRecord.Dtos;

namespace MovieSheduler.Presentation.Models
{
    public class SheduleListViewModel
    {
        public SheduleListViewModel(DateTime date, IReadOnlyCollection<SheduleRecordDto> sheduleRecords)
        {
            Date = date;
            Cinemas = GroupSheduleList(sheduleRecords);
        }

        public DateTime Date { get; set; }
        public IReadOnlyCollection<Cinema> Cinemas { get; set; }

        private IReadOnlyCollection<Cinema> GroupSheduleList(IReadOnlyCollection<SheduleRecordDto> sheduleRecords)
        {
            var cinemaList = new List<Cinema>();

            var cinemas = sheduleRecords.GroupBy(r => r.CinemaId);
            foreach (var groupedCinema in cinemas)
            {
                var cinemasByMovie = groupedCinema.GroupBy(c => c.MovieId);

                string cinemaName = groupedCinema.First().Cinema;
                int cinemaId = groupedCinema.Key;
                List<Movie> movieList = (from movie in cinemasByMovie
                                         let timeList = movie.Select(m => m.Date.ToShortTimeString()).ToList().AsReadOnly()
                                         let movieName = movie.First().Movie
                                         let movieId = movie.Key
                                         select new Movie(movieId, movieName, timeList)).ToList();

                cinemaList.Add(new Cinema(cinemaId, cinemaName, movieList));
            }
            return cinemaList.AsReadOnly();
        }

        public class Cinema
        {
            public Cinema(int id, string name, IReadOnlyCollection<Movie> movies)
            {
                if (String.IsNullOrEmpty(name))
                    throw new ArgumentException("Paramater cant be null or empty", nameof(name));
                if (movies == null)
                    throw new ArgumentNullException(nameof(movies));

                Id = id;
                Name = name;
                Movies = movies;
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public IReadOnlyCollection<Movie> Movies { get; set; }
        }

        public class Movie
        {
            public Movie(int id, string name, IReadOnlyCollection<string> timeList)
            {
                if (String.IsNullOrEmpty(name))
                    throw new ArgumentException("Paramater cant be null or empty", nameof(name));
                if (timeList == null)
                    throw new ArgumentNullException(nameof(timeList));

                Id = id;
                Name = name;
                TimeList = timeList;
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public IReadOnlyCollection<string> TimeList { get; set; }
        }
    }
}