using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieSheduler.Application.Infrastructure;
using MovieSheduler.Application.SheduleRecord.Dtos;

namespace MovieSheduler.Application.SheduleRecord
{
    public interface ISheduleRecordService
    {
        Task<GetSheduleByDateOutput> GetShedule(DateTime date);
        Task<DateTime?> GetFirstAvailableDate();
        IValidationDictionary AddRecord(AddRecordInput record);
        Task<IReadOnlyCollection<DateTime>> GetAvailableDates();
        Task<IReadOnlyCollection<TimeSpan>> GetSeansons(GetSeansonsInput seansonsInput);
        Task<IValidationDictionary> DeleteRecords(DeleteRecordsInput deleteRecordsInput);
        Task<bool> RecordExist(RecordExistInput recordExistInput);
        Task<ValidationDictionary> EditRecord(EditRecordInput editRecordInput);
    }
}
