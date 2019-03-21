using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Seq.Api.Model.Apps
{
    public class AppEntity : Entity
    {
        public AppEntity()
        {
            Name = "New App";
            AvailableSettings = new List<AppSettingPart>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<AppSettingPart> AvailableSettings { get; set; }

        public bool AllowReprocessing { get; set; }

        public AppPackagePart Package { get; set; }

        /// <summary>
        /// If <c>true</c>, the app produces an input stream and does not accept events itself.
        /// </summary>
        public bool IsInput { get; set; }
    }
}
