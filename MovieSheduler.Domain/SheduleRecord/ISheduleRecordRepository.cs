using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieSheduler.Domain.SheduleRecord
{
    public interface ISheduleRecordRepository
    {
        Task<IReadOnlyCollection<SheduleRecord>> GetSheduleByDateAsync(DateTime date);
        Task<DateTime?> GetFirstAvailableDateAsync();
        void AddRecords(IEnumerable<SheduleRecord> record);
        bool ExistByProperties(SheduleRecord record);
    }
}
