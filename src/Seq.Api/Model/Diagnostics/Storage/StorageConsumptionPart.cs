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

using Seq.Api.Model.Shared;

namespace Seq.Api.Model.Diagnostics.Storage
{
    /// <summary>
    /// Describes storage space consumed by the event store, for a range
    /// of event timestamps.
    /// </summary>
    public class StorageConsumptionPart
    {
        /// <summary>
        /// The range of timestamps covered by the result.
        /// </summary>
        public DateTimeRange Range { get; set; }

        /// <summary>
        /// The duration of the timestamp interval covered by each result.
        /// </summary>
        public uint IntervalMinutes { get; set; }
        
        /// <summary>
        /// A potentially-sparse rowset describing the storage space consumed
        /// for a range of timestamp intervals.
        /// </summary>
        public RowsetPart Results { get; set; }
    }
}
