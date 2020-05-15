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
using Newtonsoft.Json;
using Seq.Api.Model.Shared;
using Seq.Api.Model.Signals;

namespace Seq.Api.Model.Users
{
    /// <summary>
    /// A user on the Seq server.
    /// </summary>
    public class UserEntity : Entity
    {
        /// <summary>
        /// The username that uniquely identifies the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// An optional display name to aid in identifying the user.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The user's email address. This will be used to show a
        /// Gravatar for the user in some situations.
        /// </summary>
        public string EmailAddress { get; set; }
        
        /// <summary>
        /// The user's preferences.
        /// </summary>
        public Dictionary<string, object> Preferences { get; set; }

        /// <summary>
        /// If changing password, the new password for the user.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string NewPassword { get; set; }

        /// <summary>
        /// A filter that is applied to searches and queries instigated by
        /// the user.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DescriptiveFilterPart ViewFilter { get; set; }

        /// <summary>
        /// If <c>true</c>, the user will be unable to log in without first
        /// changing their password. Recommended when administratively assigning
        /// a password for the user.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool MustChangePassword { get; set; }

        /// <summary>
        /// The ids of one or more roles that grant permissions to the user. Note that
        /// the Seq UI currently only supports a single role when editing users.
        /// </summary>
        public HashSet<string> RoleIds { get; set; } = new HashSet<string>();

        /// <summary>
        /// The authentication provider associated with the user account. This will normally be
        /// the system-configured authentication provider, but if the provider is changed, the
        /// user may need to be unlinked from an existing provider so that login can proceed through
        /// the new provider.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AuthenticationProvider { get; set; }

        /// <summary>
        /// The unique identifier that links the identity provided by the authentication provider
        /// with the Seq user.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AuthenticationProviderUniqueIdentifier { get; set; }
    }
}
