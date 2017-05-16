using System;
using System.Collections.Generic;

namespace Seq.Api.Model.Apps
{
    public class AppEntity : Entity
    {
        public AppEntity()
        {
            Name = "New App";
#pragma warning disable CS0618 // Type or member is obsolete
            AssemblyNames = new List<string>();
            AvailableSettings = new List<AppSettingPart>();
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public string Name { get; set; }

        public string Description { get; set; }

        [Obsolete("Packages must be installed via a feed.")]
        public string MainReactorTypeName { get; set; }

        [Obsolete("Packages must be installed via a feed.")]
        public List<string> AssemblyNames { get; set; }

        public List<AppSettingPart> AvailableSettings { get; set; }

        public bool AllowReprocessing { get; set; }

        public bool IsReadOnly { get; set; }

        public AppPackagePart Package { get; set; }
    }
}
