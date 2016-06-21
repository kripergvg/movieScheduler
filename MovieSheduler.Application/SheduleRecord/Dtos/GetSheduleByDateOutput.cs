using System.Collections.Generic;

namespace MovieSheduler.Application.SheduleRecord.Dtos
{
    public class GetSheduleByDateOutput
    {
        public IReadOnlyCollection<SheduleRecordDto> SheduleRecords { get; set; }
    }
}
