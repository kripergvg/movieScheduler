using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using MovieSheduler.Domain.Cinema;

namespace MovieSheduler.EntityFramework.Repositories
{
    public class CinemaRepository : MovieShedulerRepositoryBase<Cinema, int>, ICinemaRepository
    {
        public CinemaRepository(MovieShedulerContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyCollection<Cinema>> GetAllCinemaAsync()
        {
            return (await Table.ToListAsync()).AsReadOnly();
        }
    }
}
