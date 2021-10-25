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

using Seq.Api.Model.Feeds;

namespace Seq.Api.Model.Apps
{
    /// <summary>
    /// Describes a NuGet package containing executable app components.
    /// </summary>
    public class AppPackagePart
    {
        /// <summary>
        /// The id of the <see cref="NuGetFeedEntity"/> from which the package was installed.
        /// </summary>
        public string NuGetFeedId { get; set; }

        /// <summary>
        /// The package id, for example <c>Seq.Input.HealthCheck</c>.
        /// </summary>
        public string PackageId { get; set; }

        /// <summary>
        /// The version of the package.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Package authorship information.
        /// </summary>
        public string Authors { get; set; }
        
        /// <summary>
        /// URL of the package license.
        /// </summary>
        public string LicenseUrl { get; set; }

        /// <summary>
        /// Whether an update is known to be available for the app.
        /// </summary>
        public bool UpdateAvailable { get; set; }
    }
}
