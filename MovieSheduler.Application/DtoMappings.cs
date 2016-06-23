using AutoMapper;
using MovieSheduler.Application.Cinema.Dtos;
using MovieSheduler.Application.Movie.Dtos;
using MovieSheduler.Application.SheduleRecord.Dtos;

namespace MovieSheduler.Application
{
    public static class DtoMappings 
    {
        public static void CreateMap()
        {
            Mapper.CreateMap<Domain.SheduleRecord.SheduleRecord, SheduleRecordDto>()
                .ForMember(d => d.Movie, m => m.MapFrom(s => s.Movie.Name))
                .ForMember(d => d.Cinema, d => d.MapFrom(s => s.Cinema.Name))
                .ForMember(d => d.SheduleRecordId, d => d.MapFrom(s => s.Id));
            Mapper.CreateMap<Domain.Movie.Movie, MovieDto>();
            Mapper.CreateMap<Domain.Cinema.Cinema, CinemaDto>();
        }
    }
}
