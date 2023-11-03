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

using System;
using System.Text.Json.Serialization;
using Seq.Api.Model.Events;
using Seq.Api.ResourceGroups;

namespace Seq.Api.Model.Permalinks
{
    /// <summary>
    /// A permanently preserved event with a stable URI.
    /// </summary>
    public class PermalinkEntity : Entity
    {
        /// <summary>
        /// When retrieving an event that <em>may</em> be permalinked (backwards compatibility),
        /// this hint is given by specifying `permalinkId=unknown` in the API call.
        /// </summary>
        public const string UnknownId = "unknown";

        /// <summary>
        /// The owner of the permalink.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// The original id of the permalinked event.
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// When the permalink was created.
        /// </summary>
        public DateTime CreatedUtc { get; set; }

        /// <summary>
        /// The event itself. Only populated when explicitly requested from the API.
        /// See <see cref="PermalinksResourceGroup.FindAsync"/>.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EventEntity Event { get; set; }
    }
}
