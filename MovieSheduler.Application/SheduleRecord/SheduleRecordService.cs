using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MovieSheduler.Application.Infrastructure;
using MovieSheduler.Application.SheduleRecord.Dtos;
using MovieSheduler.Domain.Infrastructure;
using MovieSheduler.Domain.SheduleRecord;

namespace MovieSheduler.Application.SheduleRecord
{
    public class SheduleRecordService : BaseService, ISheduleRecordService
    {
        private readonly ISheduleRecordRepository _sheduleRecordRepository;

        public SheduleRecordService(ISheduleRecordRepository sheduleRecordRepository, IUnitOfWorkFactory unitOfWorkFactory, IValidationDictionary validationDictionary)
            : base(unitOfWorkFactory, validationDictionary)
        {
            _sheduleRecordRepository = sheduleRecordRepository;
        }

        public async Task<GetSheduleByDateOutput> GetShedule(DateTime date)
        {
            using (UnitOfWorkFactory.Create())
            {
                return new GetSheduleByDateOutput
                {
                    SheduleRecords = Mapper.Map<IReadOnlyCollection<SheduleRecordDto>>(await _sheduleRecordRepository.GetSheduleByDateAsync(date))
                };
            }
        }

        public async Task<DateTime?> GetFirstAvailableDate()
        {
            using (UnitOfWorkFactory.Create())
            {
                return await _sheduleRecordRepository.GetFirstAvailableDateAsync();
            }
        }

        public  IValidationDictionary AddRecord(AddRecordInput record)
        {
            using (var unitOfWork = UnitOfWorkFactory.Create())
            {
                var records = record.TimeList.Select(date => new Domain.SheduleRecord.SheduleRecord
                {
                    MovieId = record.CinemaId,
                    CinemaId = record.CinemaId,
                    Date = date
                }).ToList();

                var checkExistRecords = records.Select(r => new
                {
                    r.Date,
                    Exist = _sheduleRecordRepository.ExistByProperties(r)
                });

                var existRecords = checkExistRecords.Where(c =>  c.Exist);
                foreach (var existRecord in existRecords)
                {
                    ValidationDictionary.AddError($"Расписание на {existRecord.Date.ToShortTimeString()} уже существует");
                }

                if (ValidationDictionary.IsValid)
                {
                    _sheduleRecordRepository.AddRecords(records);
                    unitOfWork.SaveChanges();
                }

                return ValidationDictionary;
            }
        }
    }
}
