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

namespace Seq.Api.Model.Settings
{
    /// <summary>
    /// A set of related setting values, to be applied together, or (for authentication settings) tested without being
    /// applied.
    /// </summary>
    public class SettingsValuesPart
    {
        /// <summary>
        /// Setting values keyed by setting name (see <see cref="SettingName"/>). Settings that are omitted keep their
        /// current values. Settings that are read-only (see <see cref="SettingEntity.IsReadOnly"/>) must be omitted.
        /// </summary>
        /// <remarks>When testing authentication settings, the <c>AuthenticationProvider</c> setting determines which
        /// provider is tested, and settings that don't apply to that provider are ignored.</remarks>
        public Dictionary<string, object> Settings { get; set; } = new();

        /// <summary>
        /// When testing settings for providers that check credentials locally (Active Directory), the username to
        /// sign in with. Must be <c>null</c> when applying settings.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        /// <summary>
        /// When testing settings for providers that check credentials locally (Active Directory), the password to
        /// sign in with. Must be <c>null</c> when applying settings.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }
    }
}
