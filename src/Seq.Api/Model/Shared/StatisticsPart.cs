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

namespace Seq.Api.Model.Shared
{
    /// <summary>
    /// Information about a request to search for events.
    /// </summary>
    public class StatisticsPart
    {
        /// <summary>
        /// The server-side elapsed time taken satisfying the request.
        /// </summary>
        public TimeSpan Elapsed { get; set; }

        /// <summary>
        /// The number of events that were scanned in the search (and were not
        /// able to be excluded based on index information or pre-filtering).
        /// </summary>
        public ulong ScannedEventCount { get; set; }

        /// <summary>
        /// The id of the last event inspected in the search.
        /// </summary>
        public string LastReadEventId { get; set; }

        /// <summary>
        /// The timestamp of the last event inspected in the search.
        /// </summary>
        public string LastReadEventTimestamp { get; set; }

        /// <summary>
        /// Status of the result set.
        /// </summary>
        public ResultSetStatus Status { get; set; }

        /// <summary>
        /// Whether it was necessary to read from disk in processing this request.
        /// </summary>
        public bool UncachedSegmentsScanned { get; set; }
    }
}
