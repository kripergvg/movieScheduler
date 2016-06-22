using System;

namespace MovieSheduler.Application.SheduleRecord.Dtos
{   
    public class DeleteRecordsInput
    {
        public DeleteRecordsInput(int cinemaId, int movieId, DateTime date)
        {
            CinemaId = cinemaId;
            MovieId = movieId;
            Date = date;
        }

        public int CinemaId { get; set; }
        public int MovieId { get; set; }
        public DateTime Date { get; set; }
    }
}
