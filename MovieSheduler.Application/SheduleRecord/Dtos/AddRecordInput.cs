﻿using System;
using System.Collections.Generic;

namespace MovieSheduler.Application.SheduleRecord.Dtos
{
    public class AddRecordInput
    {
        public AddRecordInput(int movieId, int cinemaId, IReadOnlyCollection<TimeSpan> timeList)
        {
            MovieId = movieId;
            CinemaId = cinemaId;
            TimeList = timeList;
        }

        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public DateTime Date { get; set; }
        public IReadOnlyCollection<TimeSpan> TimeList { get; set; }

    }
}
