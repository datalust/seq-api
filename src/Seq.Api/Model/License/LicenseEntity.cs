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

namespace Seq.Api.Model.License
{
    /// <summary>
    /// A Seq license certificate.
    /// </summary>
    public class LicenseEntity : Entity
    {
        /// <summary>
        /// The cryptographically-signed certificate that describes the
        /// license, or <c>null</c> if the server is using the default license.
        /// </summary>
        public string LicenseText { get; set; }

        /// <summary>
        /// Whether or not the license is valid for the server.
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// If <c>true</c>, the server is using the default license which allows
        /// a single person to access the Seq server.
        /// </summary>
        public bool IsSingleUser { get; set; }

        /// <summary>
        /// If the license is a subscription, the subscription id.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// Information about the status of the license.
        /// </summary>
        public string StatusDescription { get; set; }

        /// <summary>
        /// If <c>true</c>, see <see cref="StatusDescription"/> for important information.
        /// </summary>
        public bool IsWarning { get; set; }

        /// <summary>
        /// If <c>true</c>, the license can be renewed online.
        /// </summary>
        public bool CanRenewOnlineNow { get; set; }

        /// <summary>
        /// The number of users licensed to access the Seq server, or <c>null</c> if
        /// the license has no user limit.
        /// </summary>
        public int? LicensedUsers { get; set; }
        
        /// <summary>
        /// If the license is for a subscription, automatically check datalust.co and
        /// update the license when the subscription is renewed or tier changed.
        /// </summary>
        public bool AutomaticallyRefresh { get; set; }
        
        /// <summary>
        /// The amount of storage (in gigabytes) that Seq is licensed to store.
        /// </summary>
        public int? StorageLimitGigabytes { get; set; }
        
        /// <summary>
        /// If <c>true</c>, the license supports running Seq in a HA configuration.
        /// </summary>
        public bool Clustered { get; set; }
    }
}
