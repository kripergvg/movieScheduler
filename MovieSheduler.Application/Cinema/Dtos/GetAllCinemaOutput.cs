using System;
using System.Collections.Generic;

namespace MovieSheduler.Application.Cinema.Dtos
{
    public class GetAllCinemaOutput
    {
        public GetAllCinemaOutput(IReadOnlyCollection<CinemaDto> cinemas)
        {
            if (cinemas == null)
                throw new ArgumentNullException(nameof(cinemas));

            Cinemas = cinemas;
        }

        public IReadOnlyCollection<CinemaDto> Cinemas { get; set; }
    }
}
