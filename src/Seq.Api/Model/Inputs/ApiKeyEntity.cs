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
using Seq.Api.Model.Security;

namespace Seq.Api.Model.Inputs
{
    /// <summary>
    /// API keys can be used to authenticate and identify log event sources, and for
    /// users to delegate some or all permissions to a client of the Seq API (app or integration) without exposing
    /// user credentials.
    /// </summary>
    public class ApiKeyEntity : Entity
    {
        /// <summary>
        /// A friendly human-readable description of the API key.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The API key token. <see cref="Token"/> can be specified explicitly when creating an API key, but is not
        /// readable once the API key is created. Leaving the token blank will cause the server to generate a
        /// cryptographically random API key token. After creation, the first few (additional, redundant) characters
        /// of the token will be readable from <see cref="TokenPrefix"/>, but because only a cryptographically-secure
        /// hash of the token is stored internally, the token itself cannot be retrieved.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// A few characters from the start of the <see cref="Token"/> stored as plain text, to aid in identifying tokens.
        /// </summary>
        public string TokenPrefix { get; set; }

        /// <summary>
        /// Settings that control how events are ingested through the API key.
        /// </summary>
        public InputSettingsPart InputSettings { get; set; } = new InputSettingsPart();

        /// <summary>
        /// If <c>true</c>, the key is the built-in (tokenless) API key representing unauthenticated HTTP ingestion.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// If non-<c>null</c>, the id of the user for whom this is a personal API key.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// The <see cref="Permission"/>s assigned to the API key. Note that, if the API key is owned by an individual user, permissions
        /// not held by the user will be ignored by the server.
        /// </summary>
        public HashSet<Permission> AssignedPermissions { get; set; } = new HashSet<Permission>();

        /// <summary>
        /// Information about the ingestion activity using this API key.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InputMetricsPart InputMetrics { get; set; } = new InputMetricsPart();
    }
}
