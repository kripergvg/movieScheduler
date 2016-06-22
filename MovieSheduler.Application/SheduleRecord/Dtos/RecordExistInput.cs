﻿using System;

namespace MovieSheduler.Application.SheduleRecord.Dtos
{
    public class RecordExistInput
    {
        public RecordExistInput(int movieId, int cinemaId, DateTime date)
        {
            MovieId = movieId;
            CinemaId = cinemaId;
            Date = date;
        }

        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public DateTime Date { get; set; }
    }
}
