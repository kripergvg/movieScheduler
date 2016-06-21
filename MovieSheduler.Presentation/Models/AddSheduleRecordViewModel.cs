using System.Collections.Generic;
using System.Web.Mvc;
using MovieSheduler.Application.Cinema.Dtos;
using MovieSheduler.Application.Movie.Dtos;

namespace MovieSheduler.Presentation.Models
{
    public class AddSheduleRecordViewModel : AddSheduleRecord
    {
        public AddSheduleRecordViewModel(IReadOnlyCollection<MovieDto> movies, IReadOnlyCollection<CinemaDto> cinemas)
        {
            Movies = new SelectList(movies, "Id", "Name");
            Cinemas = new SelectList(cinemas, "Id", "Name");
        }

        public SelectList Movies { get; set; }
        public SelectList Cinemas { get; set; }
    }
}