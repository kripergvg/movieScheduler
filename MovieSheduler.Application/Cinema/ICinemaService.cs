using System.Threading.Tasks;
using MovieSheduler.Application.Cinema.Dtos;

namespace MovieSheduler.Application.Cinema
{
    public interface ICinemaService
    {
        /// <summary>
        /// Get all cinema
        /// </summary>
        /// <returns>GetAllCinemaOutput with cinema list</returns>
        Task<GetAllCinemaOutput> GetAllCinema();

        /// <summary>
        /// Get cinema by id
        /// </summary>
        /// <param name="cinemaId">Cinema id</param>
        /// <returns>Cinema</returns>
        Task<CinemaDto> GetCinemaById(int cinemaId);
    }
}
