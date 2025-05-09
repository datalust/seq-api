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
using Seq.Api.Model.LogEvents;
using Seq.Api.Model.Shared;

namespace Seq.Api.Model.Inputs
{
    /// <summary>
    /// Settings carried by API keys, (input) app instances, and other inputs.
    /// </summary>
    public class InputSettingsPart
    {
        /// <summary>
        /// Properties that will be automatically added to all events ingested using the key. These will override any properties with
        /// the same names already present on the event.
        /// </summary>
        public List<EventPropertyPart> AppliedProperties { get; set; } = new List<EventPropertyPart>();

        /// <summary>
        /// A filter that selects events to ingest. If <c>null</c>, all events received using the key will be ingested.
        /// </summary>
        public DescriptiveFilterPart Filter { get; set; } = new DescriptiveFilterPart();

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
    }
}
