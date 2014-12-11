using System;
using System.Collections.Generic;

namespace Seq.Api.Model.Metrics
{
    public class MetricsEntity : Entity
    {
        public MetricsEntity()
        {
            RunningTasks = new List<RunningTaskPart>();
        }

        public int EventStoreDaysRecorded { get; set; }
        public int EventStoreDaysCached { get; set; }
        public int EventStoreEventsCached { get; set; }
        public DateTime? EventStoreFirstSegmentDateUtc { get; set; }
        public DateTime? EventStoreLastSegmentDateUtc { get; set; }
        public long EventStoreDiskRemainingBytes { get; set; }

        public int EndpointArrivalsPerMinute { get; set; }
        public int EndpointInfluxPerMinute { get; set; }
        public int EndpointInvalidPayloadsPerMinute { get; set; }
        public int EndpointUnauthorizedPayloadsPerMinute { get; set; }

        public TimeSpan ProcessUptime { get; set; }
        public long ProcessWorkingSetBytes { get; set; }
        public int ProcessThreads { get; set; }
        public int ProcessThreadPoolUserThreadsAvailable { get; set; }
        public int ProcessThreadPoolIocpThreadsAvailable { get; set; }

        public double SystemMemoryUtilization { get; set; }

        public List<RunningTaskPart> RunningTasks { get; set; }
    }
}
