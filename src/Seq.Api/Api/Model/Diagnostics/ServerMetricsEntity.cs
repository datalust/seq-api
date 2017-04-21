using System;
using System.Collections.Generic;

namespace Seq.Api.Model.Diagnostics
{
    public class ServerMetricsEntity : Entity
    {
        public ServerMetricsEntity()
        {
            RunningTasks = new List<RunningTaskPart>();
        }

        public double EventStoreDaysRecorded { get; set; }
        public double EventStoreDaysCached { get; set; }
        public int EventStoreEventsCached { get; set; }
        public DateTime? EventStoreFirstSegmentDateUtc { get; set; }
        public DateTime? EventStoreLastSegmentDateUtc { get; set; }
        public long EventStoreDiskRemainingBytes { get; set; }

        public int EndpointArrivalsPerMinute { get; set; }
        public int EndpointInfluxPerMinute { get; set; }
        public long EndpointIngestedBytesPerMinute { get; set; }
        public int EndpointInvalidPayloadsPerMinute { get; set; }
        public int EndpointUnauthorizedPayloadsPerMinute { get; set; }

        public TimeSpan ProcessUptime { get; set; }
        public long ProcessWorkingSetBytes { get; set; }
        public int ProcessThreads { get; set; }
        public int ProcessThreadPoolUserThreadsAvailable { get; set; }
        public int ProcessThreadPoolIocpThreadsAvailable { get; set; }

        public double SystemMemoryUtilization { get; set; }

        public List<RunningTaskPart> RunningTasks { get; set; }

        public int QueriesPerMinute { get; set; }
        public int QueryCacheTimeSliceHitsPerMinute { get; set; }
        public int QueryCacheInvalidationsPerMinute { get; set; }

        public int DocumentStoreActiveSessions { get; set; }
        public int DocumentStoreActiveTransactions { get; set; }
    }
}
