// Copyright 2014-2019 Datalust and contributors. 
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
using Seq.Api.Model.LogEvents;
using Seq.Api.Model.Security;
using Seq.Api.Model.Signals;

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
        /// The API key token. API keys generated for versions of Seq prior to 5.0 will expose this value. From 5.0 onwards,
        /// <see cref="Token"/> can be specified explicitly when creating an API key, but is not readable once the API key is created.
        /// Leaving the token blank will cause the server to generate a cryptographically random API key token. After creation, the first
        /// few characters of the token will be readable from <see cref="TokenPrefix"/>, but because only a cryptographically-secure
        /// hash of the token is stored internally, the token itself cannot be retrieved.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// A few characters from the start of the <see cref="Token"/> stored as plain text, to aid in identifying tokens.
        /// </summary>
        public string TokenPrefix { get; set; }

        /// <summary>
        /// Properties that will be automatically added to all events ingested using the key. These will override any properties with
        /// the same names already present on the event.
        /// </summary>
        public List<InputAppliedPropertyPart> AppliedProperties { get; set; } = new List<InputAppliedPropertyPart>();

        /// <summary>
        /// A filter that selects events to ingest. If <c>null</c>, all events received using the key will be ingested.
        /// </summary>
        public SignalFilterPart InputFilter { get; set; } = new SignalFilterPart();

        /// <summary>
        /// A minimum level at which events received using the key will be ingested. The level hierarchy understood by Seq is fuzzy
        /// enough to handle most common leveling schemes. This value will be provided to callers so that they can dynamically
        /// filter events client-side, if supported.
        /// </summary>
        public LogEventLevel? MinimumLevel { get; set; }

        /// <summary>
        /// If <c>true</c>, timestamps already present on the events will be ignored, and server timestamps used instead. This is not
        /// recommended for most use cases.
        /// </summary>
        public bool UseServerTimestamps { get; set; }

        /// <summary>
        /// If <c>true</c>, the key is the built-in (tokenless) API key.
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
        public ApiKeyMetricsPart Metrics { get; set; } = new ApiKeyMetricsPart();
    }
}
