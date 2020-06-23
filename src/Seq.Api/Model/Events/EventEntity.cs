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

namespace Seq.Api.Model.Events
{
    /// <summary>
    /// An event.
    /// </summary>
    public class EventEntity : Entity
    {
        /// <summary>
        /// The ISO-8601 timestamp at which the event occurred.
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// Properties associated with the event.
        /// </summary>
        public List<EventPropertyPart> Properties { get; set; }

        /// <summary>
        /// Pre-parsed tokens showing how the event's message is constructed from its properties.
        /// </summary>
        public List<MessageTemplateTokenPart> MessageTemplateTokens { get; set; }

        /// <summary>
        /// A string describing the event's type, in dollar-prefixed hexadecimal; e.g. <c>$c0ffee00</c>.
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// A level associated with an event; <c>null</c> may indicate that the event is informational.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Level { get; set; }

        /// <summary>
        /// An exception, stack trace/backtrace associated with the event.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Exception { get; set; }

        /// <summary>
        /// If requested, a pre-rendered version of the templated event message.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RenderedMessage { get; set; }
    }
}
