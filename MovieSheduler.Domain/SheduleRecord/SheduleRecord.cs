using System;

namespace MovieSheduler.Domain.SheduleRecord
{
    public class SheduleRecord : Entity
    {
        public DateTime Date { get; set; }
        
        public virtual Movie.Movie Movie { get; set; }
        public int MovieId { get; set; }

        public virtual Cinema.Cinema Cinema { get; set; }
        public int CinemaId { get; set; }
    }
}
