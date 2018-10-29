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

        public string Title { get; set; }
        public bool IsManualInputOnly { get; set; }
        public string AppId { get; set; }
        public Dictionary<string, string> Settings { get; set; }
        public List<string> InvocationOverridableSettings { get; set; }
        public TimeSpan? ArrivalWindow { get; set; }
        public SignalExpressionPart InputSignalExpression { get; set; }
        public bool DisallowManualInput { get; set; }
        public int ChannelCapacity { get; set; }
        public TimeSpan SuppressionTime { get; set; }
        public int EventsPerSuppressionWindow { get; set; }

        public List<AppSettingPart> InvocationOverridableSettingDefinitions { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public AppInstanceMetricsPart Metrics { get; set; }
    }
}
