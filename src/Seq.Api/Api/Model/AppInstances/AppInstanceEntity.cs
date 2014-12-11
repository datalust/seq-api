using System;
using System.Collections.Generic;

namespace Seq.Api.Model.AppInstances
{
    public class AppInstanceEntity : Entity
    {
        public AppInstanceEntity()
        {
            Settings = new Dictionary<string, string>();
            ArrivalWindow = TimeSpan.FromSeconds(30);
        }

        public string Title { get; set; }
        public bool IsManualInputOnly { get; set; }
        public string AppId { get; set; }
        public string ViewId { get; set; }
        public string QueryId { get; set; }
        public Dictionary<string, string> Settings { get; set; }
        public TimeSpan ArrivalWindow { get; set; }
    }
}
