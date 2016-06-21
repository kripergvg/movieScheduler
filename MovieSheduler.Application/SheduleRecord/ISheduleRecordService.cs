using System;
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
    }
}
