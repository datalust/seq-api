namespace Seq.Api.Data
{
    public class QueryExecutionStatisticsPart
    {
        public long ScannedEventCount { get; set; }
        public long MatchingEventCount { get; set; }
        public bool UncachedSegmentsScanned { get; set; }
        public double ElapsedMilliseconds { get; set; }
    }
}