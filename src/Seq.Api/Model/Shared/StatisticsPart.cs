using System;

namespace Seq.Api.Model.Shared
{
    public class StatisticsPart
    {
        public TimeSpan Elapsed { get; set; }
        public long ScannedEventCount { get; set; }
        public string LastReadEventId { get; set; }
        public string LastReadEventTimestamp { get; set; }
        public ResultSetStatus Status { get; set; }
        public bool UncachedSegmentsScanned { get; set; }
    }
}
