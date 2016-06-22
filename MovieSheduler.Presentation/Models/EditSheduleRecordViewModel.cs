using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MovieSheduler.Application.Cinema.Dtos;
using MovieSheduler.Application.Movie.Dtos;

namespace MovieSheduler.Presentation.Models
{
    public class EditSheduleRecordViewModel : EditSheduleRecord
    {
        public EditSheduleRecordViewModel(CinemaDto cinema, MovieDto movie, DateTime date, IReadOnlyCollection<TimeSpan> seansList, EditSheduleRecord baseModel = null)
        {
            CinemaName = cinema.Name;
            MovieName = movie.Name;
            SeansList = seansList.ToList();
            CinemaId = cinema.Id;
            MovieId = movie.Id;
            Date = date;

            if (baseModel != null)
            {
                SeansList = baseModel.SeansList;
            }
        }

        [Display(Name = "Кинотеатр")]
        public string CinemaName { get; set; }

        [Display(Name = "Фильм")]
        public string MovieName { get; set; }
    }
}