using System;
using System.Collections.Generic;

namespace MovieSheduler.Application.SheduleRecord.Dtos
{
    public class AddRecordInput
    {
        public AddRecordInput(int movieId, int cinemaId, IReadOnlyCollection<TimeSpan> timeList, DateTime date)
        {
            if (timeList == null)
                throw new ArgumentNullException(nameof(timeList));

            MovieId = movieId;
            CinemaId = cinemaId;
            TimeList = timeList;
            Date = date;
        }

        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public DateTime Date { get; set; }
        public IReadOnlyCollection<TimeSpan> TimeList { get; set; }

    }
}
