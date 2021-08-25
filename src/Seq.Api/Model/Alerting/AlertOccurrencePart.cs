using System;
using System.Collections.Generic;
using Seq.Api.Model.AppInstances;
using Seq.Api.Model.LogEvents;
using Seq.Api.Model.Shared;

namespace Seq.Api.Model.Alerting
{
    /// <summary>
    /// An occurrence of an alert that triggered notifications.
    /// </summary>
    public class AlertOccurrencePart
    {
        /// <summary>
        /// The time when the alert was checked and triggered.
        /// </summary>
        public DateTime DetectedAt { get; set; }
        
        /// <summary>
        /// The time grouping that triggered the alert.
        /// </summary>
        public DateTimeRange DetectedOverRange { get; set; }

        /// <summary>
        /// The level of notifications sent for this instance.
        /// </summary>
        public LogEventLevel NotificationLevel { get; set; }

        /// <summary>
        /// The <see cref="NotificationChannelPart">NotificationChannelParts</see> that were alerted.
        /// </summary>
        public List<AlertNotificationPart> Notifications { get; set; } = new List<AlertNotificationPart>();
    }
}