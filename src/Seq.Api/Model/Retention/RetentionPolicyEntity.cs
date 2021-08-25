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
using Newtonsoft.Json;
using Seq.Api.Model.Signals;

namespace Seq.Api.Model.Retention
{
    /// <summary>
    /// A retention policy. Identifies a subset of events to delete at a specified age.
    /// </summary>
    public class RetentionPolicyEntity : Entity
    {
        /// <summary>
        /// The age at which events will be deleted by the policy. This is based on the
        /// events' timestamps relative to the server's clock.
        /// </summary>
        public TimeSpan RetentionTime { get; set; }

        /// <summary>
        /// An optional <see cref="SignalExpressionPart"/> describing the set of events
        /// to delete. If <c>null</c>, the policy will efficiently truncate the event store,
        /// deleting all events.
        /// </summary>
        public SignalExpressionPart RemovedSignalExpression { get; set; }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete("Replaced by RemovedSignalExpression."),
         JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SignalId { get; set; }
    }
}
