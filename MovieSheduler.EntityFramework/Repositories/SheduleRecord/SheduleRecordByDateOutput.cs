using System;

namespace MovieSheduler.EntityFramework.Repositories.SheduleRecord
{
    public class SheduleRecordByDateOutput
    {
        public DateTime Date { get; set; }
        public int SheduleRecordId { get; set; }
        public string Cinema { get; set; }
        public int CinemaId { get; set; }
        public string Movie { get; set; }
        public int MovieId { get; set; }
    }
}
