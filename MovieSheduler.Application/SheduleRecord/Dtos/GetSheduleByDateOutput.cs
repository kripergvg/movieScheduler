using System;
using System.Collections.Generic;

namespace MovieSheduler.Application.SheduleRecord.Dtos
{
    public class GetSheduleByDateOutput
    {
        public GetSheduleByDateOutput(IReadOnlyCollection<SheduleRecordDto> sheduleRecords)
        {
            if (sheduleRecords == null)
                throw new ArgumentNullException(nameof(sheduleRecords));

            SheduleRecords = sheduleRecords;
        }

        public IReadOnlyCollection<SheduleRecordDto> SheduleRecords { get; set; }
    }
}
