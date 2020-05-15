namespace Seq.Api.Model.AppInstances
{
    /// <summary>
    /// Describes the events reaching being output from Seq through the app.
    /// </summary>
    public class AppInstanceOutputMetricsPart
    {
        /// <summary>
        /// The number of events per minute sent from Seq to the app. Includes streamed events (if enabled),
        /// manual invocations, and alert notifications. There may be some delay between dispatching an event, and
        /// it being processed by the app.
        /// </summary>
        public int DispatchedEventsPerMinute { get; set; }
    }
}
