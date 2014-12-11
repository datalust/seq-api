using System.Collections.Generic;

namespace Seq.Api.Model.Apps
{
    public class AppEntity : Seq.Api.Model.Entity
    {
        public AppEntity()
        {
            Name = "New App";
            AssemblyNames = new List<string>();
            AvailableSettings = new List<AppSettingPart>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MainReactorTypeName { get; set; }

        public List<string> AssemblyNames { get; set; }

        public List<AppSettingPart> AvailableSettings { get; set; }

        public bool AllowReprocessing { get; set; }

        public bool IsReadOnly { get; set; }

        public AppPackagePart Package { get; set; }
    }
}
