using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MovieSheduler.Domain.SheduleRecord;

namespace MovieSheduler.EntityFramework.Repositories.SheduleRecord
{
    public class SheduleRecordRepository : MovieShedulerRepositoryBase<Domain.SheduleRecord.SheduleRecord, int>, ISheduleRecordRepository
    {
        public SheduleRecordRepository(MovieShedulerContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<DateTime?> GetFirstAvailableDateAsync()
        {
            return (await Table.OrderBy(r => r.Date).FirstOrDefaultAsync())?.Date;
        }

        public async Task<IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>> GetSheduleByDateAsync(DateTime date)
        {
            return (await Table.Where(r => r.Date.Year == date.Year && r.Date.Month == date.Month && r.Date.Day == date.Day)
                .Include(d => d.Movie)
                .Include(d => d.Cinema)
                .ToListAsync())
                .AsReadOnly();
        }

        public void AddRecords(IEnumerable<Domain.SheduleRecord.SheduleRecord> record)
        {
            Table.AddRange(record);
        }

        public bool ExistByProperties(Domain.SheduleRecord.SheduleRecord record)
        {
            return Table.Any(r => r.CinemaId == record.CinemaId && r.MovieId == record.MovieId && r.Date == record.Date);
        }


        public async Task<IReadOnlyCollection<DateTime>> GetAvailableDates()
        {
            return await Task.Run(() => Table.Select(t => new {t.Date.Year, t.Date.Month, t.Date.Day}).Distinct()
                .ToList()
                .Select(d => new DateTime(d.Year, d.Month, d.Day))
                .ToList()
                .AsReadOnly());
        }

        public async Task<IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>> GetRecords(int cinemaId, int movieId, DateTime date)
        {
            return (await Table.Where(r => r.Date.Day == date.Day && r.Date.Month == date.Month && r.Date.Day == date.Day
                                           && r.CinemaId == cinemaId && r.MovieId == movieId).ToListAsync()).AsReadOnly();
        }

        public async Task<bool> RecordExist(int cinemaId, int movieId, DateTime date)
        {
            return await Table.AnyAsync(r => r.Date.Day == date.Day && r.Date.Month == date.Month && r.Date.Day == date.Day
                                             && r.CinemaId == cinemaId && r.MovieId == movieId);
        }
    }
}
