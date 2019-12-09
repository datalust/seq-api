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

using System;
using System.Collections.Generic;

namespace Seq.Api.Model.Diagnostics
{
    /// <summary>
    /// Metrics describing the state and performance of the Seq server.
    /// </summary>
    public class ServerMetricsEntity : Entity
    {
        /// <summary>
        /// Construct a <see cref="ServerMetricsEntity"/>.
        /// </summary>
        public ServerMetricsEntity()
        {
            RunningTasks = new List<RunningTaskPart>();
        }

        /// <summary>
        /// The number of days of events able to fit in the memory cache.
        /// </summary>
        public double EventStoreDaysCached { get; set; }

        /// <summary>
        /// The number of events able to fit in the memory cache.
        /// </summary>
        public int EventStoreEventsCached { get; set; }

        /// <summary>
        /// The oldest on-disk extent currently stored.
        /// </summary>
        public DateTime? EventStoreFirstExtentRangeStartUtc { get; set; }

        /// <summary>
        /// The most recent on-disk extent currently stored.
        /// </summary>
        public DateTime? EventStoreLastExtentRangeEndUtc { get; set; }

        /// <summary>
        /// Bytes of free space remaining on the disk used for event storage.
        /// </summary>
        public long EventStoreDiskRemainingBytes { get; set; }

        /// <summary>
        /// The number of events that arrived at the ingestion endpoint in the past minute.
        /// </summary>
        public int EndpointArrivalsPerMinute { get; set; }

        /// <summary>
        /// The number of events ingested in the past minute.
        /// </summary>
        public int EndpointInfluxPerMinute { get; set; }

        /// <summary>
        /// The number of bytes of raw JSON event data ingested in the past minute (approximate).
        /// </summary>
        public long EndpointIngestedBytesPerMinute { get; set; }

        /// <summary>
        /// The number of invalid event payloads seen in the past minute.
        /// </summary>
        public int EndpointInvalidPayloadsPerMinute { get; set; }

        /// <summary>
        /// The number of unauthorized event payloads seen in the past minute.
        /// </summary>
        public int EndpointUnauthorizedPayloadsPerMinute { get; set; }

        /// <summary>
        /// The length of time for which the Seq server process has been running.
        /// </summary>
        public TimeSpan ProcessUptime { get; set; }

        /// <summary>
        /// The number of bytes working set held by the Seq server process.
        /// </summary>
        public long ProcessWorkingSetBytes { get; set; }

        /// <summary>
        /// The number of threads running in the Seq server process.
        /// </summary>
        public int ProcessThreads { get; set; }

        /// <summary>
        /// The number of thread pool user threads available.
        /// </summary>
        public int ProcessThreadPoolUserThreadsAvailable { get; set; }

        /// <summary>
        /// The number of async I/O thread pool threads available.
        /// </summary>
        public int ProcessThreadPoolIocpThreadsAvailable { get; set; }

        /// <summary>
        /// The proportion of system physical memory that is currently allocated.
        /// </summary>
        public double SystemMemoryUtilization { get; set; }

        /// <summary>
        /// Tasks running in the Seq server.
        /// </summary>
        public List<RunningTaskPart> RunningTasks { get; set; }

        /// <summary>
        /// The number of SQL-style queries executed in the past minute.
        /// </summary>
        public int QueriesPerMinute { get; set; }

        /// <summary>
        /// The number of time slices from SQL-style queries that could be read from cache in the past minute.
        /// </summary>
        public int QueryCacheTimeSliceHitsPerMinute { get; set; }

        /// <summary>
        /// The number of cached SQL query time slices invalidated in the past minute.
        /// </summary>
        public int QueryCacheInvalidationsPerMinute { get; set; }

        /// <summary>
        /// The number of active sessions reading from or writing to Seq's internal metadata store.
        /// </summary>
        public int DocumentStoreActiveSessions { get; set; }
    }
}
