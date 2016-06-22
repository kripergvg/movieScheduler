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

        public IValidationDictionary AddRecord(AddRecordInput record)
        {
            using (var unitOfWork = UnitOfWorkFactory.Create())
            {
                var records = record.TimeList.Select(date => new Domain.SheduleRecord.SheduleRecord
                {
                    MovieId = record.CinemaId,
                    CinemaId = record.CinemaId,
                    Date = record.Date.Add(date)
                }).ToList();

                var checkExistRecords = records.Select(r => new
                {
                    r.Date,
                    Exist = _sheduleRecordRepository.ExistByProperties(r)
                });

                var existRecords = checkExistRecords.Where(c => c.Exist);
                foreach (var existRecord in existRecords)
                {
                    ValidationDictionary.AddError("time", $"Расписание на {existRecord.Date.ToShortTimeString()} уже существует");
                }

                if (ValidationDictionary.IsValid)
                {
                    _sheduleRecordRepository.AddRecords(records);
                    unitOfWork.SaveChanges();
                }

                return ValidationDictionary;
            }
        }

        public async Task<IReadOnlyCollection<DateTime>> GetAvailableDates()
        {
            using (UnitOfWorkFactory.Create())
            {
                return await _sheduleRecordRepository.GetAvailableDates();
            }
        }

        public async Task<IReadOnlyCollection<TimeSpan>> GetSeansons(GetSeansonsInput seansonsInput)
        {
            using (UnitOfWorkFactory.Create())
            {
                return (await _sheduleRecordRepository.GetRecords(seansonsInput.CinemaId, seansonsInput.MovieId, seansonsInput.Date)).Select(r => new TimeSpan(r.Date.Hour, r.Date.Minute, r.Date.Second))
                .ToList()
                .AsReadOnly();
            }
        }

        public async Task<IValidationDictionary> DeleteRecords(DeleteRecordsInput deleteRecordsInput)
        {
            using (var unitOfWork = UnitOfWorkFactory.Create())
            {
                var recordsToDelete = await _sheduleRecordRepository.GetRecords(deleteRecordsInput.CinemaId, deleteRecordsInput.MovieId, deleteRecordsInput.Date);
                foreach (var sheduleRecord in recordsToDelete)
                {
                    _sheduleRecordRepository.Delete(sheduleRecord);
                }
                unitOfWork.SaveChanges();

                return new ValidationDictionary();
            }
        }

        public async Task<bool> RecordExist(RecordExistInput recordExistInput)
        {
            using (UnitOfWorkFactory.Create())
            {
                return await _sheduleRecordRepository.RecordExist(recordExistInput.CinemaId, recordExistInput.MovieId, recordExistInput.Date);
            }
        }

        public async Task<ValidationDictionary> EditRecord(EditRecordInput editRecordInput)
        {
            using (var unitOfWork = UnitOfWorkFactory.Create())
            {
                var existingRecords = await _sheduleRecordRepository.GetRecords(editRecordInput.CinemaId, editRecordInput.MovieId, editRecordInput.Date);
                IEnumerable<DateTime> existingDates = existingRecords.Select(r => r.Date);

                IEnumerable<DateTime> editDates = editRecordInput.TimeList.Select(t => editRecordInput.Date.Add(t)).ToList();

                var newDates = editDates.Where(t => !existingDates.Contains(t));
                var deleteDates = existingDates.Where(t => !editDates.Contains(t));

                foreach (var deleteDate in deleteDates)
                {
                    var recordToDelete = existingRecords.Single(r => r.Date == deleteDate);
                    _sheduleRecordRepository.Delete(recordToDelete);
                }

                var newRecrods = newDates.Select(newDate => new Domain.SheduleRecord.SheduleRecord
                {
                    CinemaId = editRecordInput.CinemaId,
                    MovieId = editRecordInput.MovieId,
                    Date = newDate
                }).ToList();

                _sheduleRecordRepository.AddRecords(newRecrods);
                unitOfWork.SaveChanges();

                return new ValidationDictionary();
            }
        }
    }
}
