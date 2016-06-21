using System.Collections.Generic;

namespace MovieSheduler.Application.Cinema.Dtos
{
    public class GetAllCinemaOutput
    {
        public IReadOnlyCollection<CinemaDto> Cinemas { get; set; }
    }
}
