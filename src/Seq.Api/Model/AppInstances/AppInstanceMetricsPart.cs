namespace Seq.Api.Model.AppInstances
{
    public class AppInstanceMetricsPart
    {
        public int ReceivedEventsPerMinute { get; set; }
        public int EmittedEventsPerMinute { get; set; }
        public long ProcessWorkingSetBytes { get; set; }
        public bool IsRunning { get; set; }
    }
}
