using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Seq.Api.Model.Apps;
using Seq.Api.Model.Signals;

namespace Seq.Api.Model.AppInstances
{
    public class AppInstanceEntity : Entity
    {
        public AppInstanceEntity()
        {
            Settings = new Dictionary<string, string>();
            InvocationOverridableSettings = new List<string>();
            InvocationOverridableSettingDefinitions = new List<AppSettingPart>();
            EventsPerSuppressionWindow = 1;
            Metrics = new AppInstanceMetricsPart();
        }

        public string AppId { get; set; }
        public string Title { get; set; }
        public Dictionary<string, string> Settings { get; set; }

        /// <summary>
        /// If <c>true</c>, administrative users may invoke the app manually or through alerts.
        /// This field is read-only from the API and generally indicates that the app is an input.
        /// </summary>
        public bool AcceptPrivilegedInvocation { get; set; }

        /// <summary>
        /// If <c>true</c>, regular users can manually send events to the app, or use the app
        /// as the target for alert notifications.
        /// </summary>
        public bool AcceptDirectInvocation { get; set; }
        public List<string> InvocationOverridableSettings { get; set; }
        public List<AppSettingPart> InvocationOverridableSettingDefinitions { get; set; }

        /// <summary>
        /// If <c>true</c>, events will be streamed to the app.
        /// </summary>
        public bool AcceptStreamedEvents { get; set; }
        public SignalExpressionPart InputSignalExpression { get; set; }
        public TimeSpan? ArrivalWindow { get; set; }
        public TimeSpan SuppressionTime { get; set; }
        public int EventsPerSuppressionWindow { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public AppInstanceMetricsPart Metrics { get; set; }

        [Obsolete("Use !AcceptStreamedEvents instead. This field will be removed in Seq 6.0.")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? IsManualInputOnly { get; set; }

        [Obsolete("Use !AcceptDirectInvocation instead. This field will be removed in Seq 6.0.")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? DisallowManualInput { get; set; }
    }
}
