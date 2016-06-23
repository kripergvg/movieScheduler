using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieSheduler.Domain.Infrastructure;

namespace MovieSheduler.Domain.SheduleRecord
{
    public interface ISheduleRecordRepository : IRepository<SheduleRecord, int>
    {
        /// <summary>
        /// Get shedule for date
        /// </summary>
        /// <param name="date">The date on which you want to return the schedule </param>
        /// <returns>Shedule</returns>
        Task<IReadOnlyCollection<SheduleRecord>> GetSheduleByDateAsync(DateTime date);

        /// <summary>
        /// Get first date with shedule records
        /// </summary>
        /// <returns>Shedule record date</returns>
        Task<DateTime?> GetFirstAvailableDateAsync();

        /// <summary>
        /// Adds record
        /// </summary>
        /// <param name="record">Records</param>
        /// <returns>Result of add record</returns>
        void AddRecords(IEnumerable<SheduleRecord> record);

        /// <summary>
        /// Check whether there is a record by properties
        /// </summary>
        /// <param name="record">Property of record</param>
        /// <returns></returns>
        bool ExistByProperties(SheduleRecord record);

        /// <summary>
        /// Receives the dates on which there is a schedule
        /// </summary>
        /// <returns>List of date</returns>
        Task<IReadOnlyCollection<DateTime>> GetAvailableDates();

        /// <summary>
        /// Get records
        /// </summary>
        /// <param name="cinemaId">Cinema id for which you want to return records</param>
        /// <param name="movieId">Movie id for which you want to return records</param>
        /// <param name="date">Date Cinema id for which you want to return records</param>
        Task<IReadOnlyCollection<SheduleRecord>> GetRecords(int cinemaId, int movieId, DateTime date);

        /// <summary>
        ///  Checks whether there is a record
        /// </summary>
        /// <param name="cinemaId">Cinema id for which you want to checks whether there is a record</param>
        /// <param name="movieId">Movie id for which you want to checks whether there is a record</param>
        /// <param name="date">Date for which you want to checks whether there is a record</param>
        Task<bool> RecordExist(int cinemaId, int movieId, DateTime date);
    }
}
