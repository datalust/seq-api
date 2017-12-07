using Seq.Api.Model.LogEvents;
using System;

namespace Seq.Api.Model.Monitoring
{
    public class AlertPart
    {
        public string Id { get; set; }
        public string Condition { get; set; }
        public TimeSpan MeasurementWindow { get; set; }
        public TimeSpan StabilizationWindow { get; set; } = TimeSpan.FromSeconds(30);
        public TimeSpan SuppressionTime { get; set; }
        public LogEventLevel Level { get; set; } = LogEventLevel.Warning;
        public string NotificationAppInstanceId { get; set; }
    }
}
