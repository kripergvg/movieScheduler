using System;

namespace MovieSheduler.Application.SheduleRecord.Dtos
{
    public class SheduleRecordDto
    {
        public DateTime Date { get; set; }
        public int SheduleRecordId { get; set; }
        public string Cinema { get; set; }
        public int CinemaId { get; set; }
        public string Movie { get; set; }
        public int MovieId { get; set; }
    }
}
