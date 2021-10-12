using System;
using System.Collections.Generic;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Seq.Api.Model.Alerting
{
    /// <summary>
    /// A summary of recent activity on an alert.
    /// </summary>
    
    public class AlertActivityPart
    {
        /// <summary>
        /// When the last check for the alert was performed.
        /// </summary>
        public DateTime? LastCheck { get; set; }
        
        /// <summary>
        /// Whether or not the last check triggered any notifications.
        /// </summary>
        public bool LastCheckTriggered { get; set; }
        
        /// <summary>
        /// Any failures that prevented the last check from completing successfully.
        /// These failures indicate a problem with the alert itself, not with the
        /// data being monitored.
        ///
        /// A value of <c>null</c> indicates the last check succeeded.
        /// </summary>
        public List<string> LastCheckFailures { get; set; }
        
        /// <summary>
        /// When the alert may be checked again after being triggered.
        /// </summary>
        public DateTime? SuppressedUntil { get; set; }
        
        /// <summary>
        /// The most recent occurrences of the alert that triggered notifications.
        /// </summary>
        public List<AlertOccurrencePart> RecentOccurrences { get; set; } = new List<AlertOccurrencePart>();

        /// <summary>
        /// The number of times this alert has been triggered since its creation.
        /// </summary>
        public int TotalOccurrences { get; set; }
    }
}
