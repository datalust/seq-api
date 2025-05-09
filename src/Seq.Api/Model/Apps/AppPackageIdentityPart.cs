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

namespace Seq.Api.Model.Apps
{
    /// <summary>
    /// The data required to identify a NuGet package version. 
    /// </summary>
    public class AppPackageIdentityPart
    {
        /// <summary>
        /// The id of the <see cref="Seq.Api.Model.Feeds.NuGetFeedEntity"/> from which the package was installed.
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
    }
}