// Copyright © Datalust and contributors. 
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;
using Seq.Api.ResourceGroups;

namespace Seq.Api.Model.Apps
{
    /// <summary>
    /// Seq apps are executable plug-ins that read from and write to the Seq event stream.
    /// </summary>
    public class AppEntity : Entity
    {
        /// <summary>
        /// Construct an <see cref="AppEntity"/> with default values.
        /// </summary>
        /// <remarks>Instead of constructing an instance directly, consider using
        /// <see cref="AppsResourceGroup.TemplateAsync"/> to obtain a partly-initialized instance.</remarks>
        public AppEntity()
        {
            Name = "New App";
            AvailableSettings = new List<AppSettingPart>();
        }

        /// <summary>
        /// The friendly name of the app.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A long-form description of the app.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Metadata describing the settings exposed by instances of the app.
        /// </summary>
        public List<AppSettingPart> AvailableSettings { get; set; }

        /// <summary>
        /// Whether instances of the app can safely process their own diagnostic events. The
        /// default is <c>false</c>. This option should not normally be set.
        /// </summary>
        public bool AllowReprocessing { get; set; }

        /// <summary>
        /// Metadata describing the NuGet package containing the executable app components.
        /// </summary>
        public AppPackagePart Package { get; set; }

        /// <summary>
        /// If <c>true</c>, the app produces an input stream and does not accept events itself.
        /// </summary>
        public bool IsInput { get; set; }
    }
}
