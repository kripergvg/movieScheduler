using System;
using System.Collections.Generic;

namespace MovieSheduler.Application.SheduleRecord.Dtos
{
    public class EditRecordInput
    {
        public EditRecordInput(int cinemaId, int movieId, DateTime date, IReadOnlyCollection<TimeSpan> timeList)
        {
            if (timeList == null)
                throw new ArgumentNullException(nameof(timeList));

            CinemaId = cinemaId;
            MovieId = movieId;
            Date = date;
            TimeList = timeList;
        }

        public int CinemaId { get; set; }
        public int MovieId { get; set; }
        public DateTime Date { get; set; }
        public IReadOnlyCollection<TimeSpan> TimeList { get; set; }
    }
}
