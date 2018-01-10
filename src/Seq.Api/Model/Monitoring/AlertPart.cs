using Seq.Api.Model.LogEvents;
using System;
using System.Collections.Generic;

namespace Seq.Api.Model.Monitoring
{
    public class AlertPart
    {
        Dictionary<string, string> _notificationAppSettingOverrides = new Dictionary<string, string>();

        public string Id { get; set; }
        public string Condition { get; set; }
        public TimeSpan MeasurementWindow { get; set; }
        public TimeSpan StabilizationWindow { get; set; } = TimeSpan.FromSeconds(30);
        public TimeSpan SuppressionTime { get; set; }
        public LogEventLevel Level { get; set; } = LogEventLevel.Warning;
        public string NotificationAppInstanceId { get; set; }

        public Dictionary<string, string> NotificationAppSettingOverrides
        {
            get => _notificationAppSettingOverrides;
            set => _notificationAppSettingOverrides = value ?? new Dictionary<string, string>();
        }
    }
}
