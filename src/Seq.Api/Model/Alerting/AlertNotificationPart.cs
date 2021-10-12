using Seq.Api.Model.AppInstances;

namespace Seq.Api.Model.Alerting
{
    /// <summary>
    /// A record of the <see cref="NotificationChannelPart"/> that was notified of an alert occurrence.
    /// </summary>
    public class AlertNotificationPart
    {
        /// <summary>
        /// The <see cref="AppInstanceEntity" /> that was notified.
        /// This id is a historical record, so it may be for app instance that no longer exists.
        /// </summary>
        public string HistoricalAppInstanceId { get; set; }
    }
}
