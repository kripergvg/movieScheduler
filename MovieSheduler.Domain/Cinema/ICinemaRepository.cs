using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieSheduler.Domain.Cinema
{
    public interface ICinemaRepository
    {
        Task<IReadOnlyCollection<Cinema>> GetAllCinemaAsync();
    }
}
