using System.Threading.Tasks;
using MovieSheduler.Application.Cinema.Dtos;

namespace MovieSheduler.Application.Cinema
{
    public interface ICinemaService
    {
        Task<GetAllCinemaOutput> GetAllCinema();
        Task<CinemaDto> GetCinemaById(int cinemaId);
    }
}
