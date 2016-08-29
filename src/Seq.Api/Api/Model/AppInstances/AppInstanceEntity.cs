using System;
using System.Collections.Generic;

namespace Seq.Api.Model.AppInstances
{
    public class AppInstanceEntity : Entity
    {
        public AppInstanceEntity()
        {
            Settings = new Dictionary<string, string>();
            SignalIds = new List<string>();
            EventsPerSuppressionWindow = 1;
        }

        public string Title { get; set; }
        public bool IsManualInputOnly { get; set; }
        public string AppId { get; set; }
        public Dictionary<string, string> Settings { get; set; }
        public TimeSpan? ArrivalWindow { get; set; }
        public List<string> SignalIds { get; set; }
        public bool DisallowManualInput { get; set; }
        public int ChannelCapacity { get; set; }
        public TimeSpan SuppressionTime { get; set; }
        public int EventsPerSuppressionWindow { get; set; }
    }
}
