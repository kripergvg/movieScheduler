using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MovieSheduler.Application.Cinema.Dtos;
using MovieSheduler.Application.Infrastructure;
using MovieSheduler.Domain.Cinema;
using MovieSheduler.Domain.Infrastructure;

namespace MovieSheduler.Application.Cinema
{
    public class CinemaService : BaseService, ICinemaService
    {
        private readonly ICinemaRepository _cinemaRepository;

        public CinemaService(ICinemaRepository cinemaRepository, IUnitOfWorkFactory unitOfWorkFactory, IValidationDictionary validationDictionary)
            : base(unitOfWorkFactory, validationDictionary)
        {
            _cinemaRepository = cinemaRepository;
        }

        public async Task<GetAllCinemaOutput> GetAllCinema()
        {
            using (UnitOfWorkFactory.Create())
            {
                return new GetAllCinemaOutput
                {
                    Cinemas = Mapper.Map<IReadOnlyCollection<CinemaDto>>(await _cinemaRepository.GetAllCinemaAsync())
                };
            }
        }

        public async Task<CinemaDto> GetCinemaById(int cinemaId)
        {
            return Mapper.Map<CinemaDto>(await _cinemaRepository.GetByIdAsync(cinemaId));
        }
    }
}
