using System;
using System.Collections.Generic;

namespace MovieSheduler.Application.SheduleRecord.Dtos
{
    public class AddRecordInput
    {
        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public List<DateTime> TimeList { get; set; }
    }
}
