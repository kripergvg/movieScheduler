using System;
using MovieSheduler.Domain.Infrastructure;

namespace MovieSheduler.Domain.SheduleRecord
{
    public class SheduleRecord : Entity
    {
        /// <summary>
        /// Date of record
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Movie of record
        /// </summary>
        public virtual Movie.Movie Movie { get; set; }

        /// <summary>
        /// Movie id of record
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// Cinema of record
        /// </summary>
        public virtual Cinema.Cinema Cinema { get; set; }

        /// <summary>
        /// CinemaId of record
        /// </summary>
        public int CinemaId { get; set; }
    }
}
