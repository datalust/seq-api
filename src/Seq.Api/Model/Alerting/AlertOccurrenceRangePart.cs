using Seq.Api.Model.LogEvents;
using Seq.Api.Model.Shared;

namespace Seq.Api.Model.Alerting
{
    /// <summary>
    /// An occurrence metric of an alert that triggered notifications.
    /// </summary>
    public class AlertOccurrenceRangePart
    {
        /// <summary>
        /// The time grouping that triggered the alert.
        /// </summary>
        public DateTimeRangePart DetectedOverRange { get; set; }
    }
}
