using System.Collections.Generic;
using System.Threading.Tasks;
using MovieSheduler.Domain.Infrastructure;

namespace MovieSheduler.Domain.Cinema
{
    public interface ICinemaRepository: IRepository<Cinema, int>
    {
        Task<IReadOnlyCollection<Cinema>> GetAllCinemaAsync();
    }
}
