using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieSheduler.Application.Infrastructure;
using MovieSheduler.Application.SheduleRecord.Dtos;

namespace MovieSheduler.Application.SheduleRecord
{
    public interface ISheduleRecordService
    {
        /// <summary>
        /// Get shedule for date
        /// </summary>
        /// <param name="date">The date on which you want to return the schedule </param>
        /// <returns>Shedule</returns>
        Task<GetSheduleByDateOutput> GetShedule(DateTime date);

        /// <summary>
        /// Get first date with shedule records
        /// </summary>
        /// <returns>Shedule record date</returns>
        Task<DateTime?> GetFirstAvailableDate();

        /// <summary>
        /// Adds record
        /// </summary>
        /// <param name="record">Record</param>
        /// <returns>Result of add record</returns>
        IValidationDictionary AddRecord(AddRecordInput record);

        /// <summary>
        /// Receives the dates on which there is a schedule
        /// </summary>
        /// <returns>List of date</returns>
        Task<IReadOnlyCollection<DateTime>> GetAvailableDates();

        /// <summary>
        /// Receives list of seans
        /// </summary>
        /// <param name="seansonsInput">Seans parametrs</param>
        /// <returns>List of seans</returns>
        Task<IReadOnlyCollection<TimeSpan>> GetSeansList(GetSeansonsInput seansonsInput);

        /// <summary>
        /// Delets record
        /// </summary>
        /// <param name="deleteRecordsInput">Record to delete</param>
        /// <returns>Result of delete record</returns>
        Task<IValidationDictionary> DeleteRecords(DeleteRecordsInput deleteRecordsInput);

        /// <summary>
        /// Checks whether there is a schedule
        /// </summary>
        /// <param name="recordExistInput">Shedule</param>
        Task<bool> RecordExist(RecordExistInput recordExistInput);

        /// <summary>
        /// Updates record
        /// </summary>
        /// <param name="editRecordInput">Record to update</param>
        /// <returns>Result of update record</returns>
        Task<IValidationDictionary> EditRecord(EditRecordInput editRecordInput);
    }
}
