using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieSheduler.Domain.Infrastructure;

namespace MovieSheduler.Domain.SheduleRecord
{
    public interface ISheduleRecordRepository : IRepository<SheduleRecord, int>
    {
        Task<IReadOnlyCollection<SheduleRecord>> GetSheduleByDateAsync(DateTime date);
        Task<DateTime?> GetFirstAvailableDateAsync();
        void AddRecords(IEnumerable<SheduleRecord> record);
        bool ExistByProperties(SheduleRecord record);
        Task<IReadOnlyCollection<DateTime>> GetAvailableDates();
        Task<IReadOnlyCollection<SheduleRecord>> GetRecords(int cinemaId, int movieId, DateTime date);
        Task<bool> RecordExist(int cinemaId, int movieId, DateTime date);
    }
}
