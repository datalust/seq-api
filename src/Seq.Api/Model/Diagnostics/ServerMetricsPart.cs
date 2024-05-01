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

namespace Seq.Api.Model.Diagnostics
{
    /// <summary>
    /// Metrics describing the state and performance of the Seq server.
    /// </summary>
    /// <remarks>This information is not preserved across server restarts or fail-over.</remarks>
    public class ServerMetricsPart
    {
        /// <summary>
        /// Construct a <see cref="ServerMetricsPart"/>.
        /// </summary>
        public ServerMetricsPart()
        {
        }

        /// <summary>
        /// Bytes of free space remaining on the disk used for event storage.
        /// </summary>
        public long? EventStoreDiskRemainingBytes { get; set; }

        /// <summary>
        /// The total time spent indexing the event store in the last 24 hours.
        /// </summary>
        public TimeSpan EventStoreIndexingTimeLastDay { get; set; }

        /// <summary>
        /// The total time spent writing events to disk in the last minute.
        /// </summary>
        public TimeSpan EventStoreWriteTimeLastMinute { get; set; }

        /// <summary>
        /// The number of events that arrived at the ingestion endpoint in the past minute.
        /// </summary>
        public int InputArrivedEventsPerMinute { get; set; }

        /// <summary>
        /// The number of events ingested in the past minute.
        /// </summary>
        public int InputIngestedEventsPerMinute { get; set; }

        /// <summary>
        /// The number of bytes of raw JSON event data ingested in the past minute (approximate).
        /// </summary>
        public long InputIngestedBytesPerMinute { get; set; }

        /// <summary>
        /// The number of invalid event payloads seen in the past minute.
        /// </summary>
        public int InvalidPayloadsPerMinute { get; set; }

        /// <summary>
        /// The number of unauthorized event payloads seen in the past minute.
        /// </summary>
        public int HttpUnauthorizedPayloadsPerMinute { get; set; }

        /// <summary>
        /// The length of time for which the Seq server process has been running.
        /// </summary>
        public TimeSpan ProcessUptime { get; set; }

        /// <summary>
        /// The number of threads running in the Seq server process.
        /// </summary>
        public int ProcessThreads { get; set; }

        /// <summary>
        /// The number of queries and searches executed in the past minute.
        /// </summary>
        public int QueriesPerMinute { get; set; }
    }
}
