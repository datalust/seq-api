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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.Diagnostics;
using Seq.Api.Model.Diagnostics.Storage;
using Seq.Api.Model.Inputs;

// ReSharper disable UnusedMember.Global

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Access server diagnostics.
    /// </summary>
    public class DiagnosticsResourceGroup : ApiResourceGroup
    {
        internal DiagnosticsResourceGroup(ISeqConnection connection)
            : base("Diagnostics", connection)
        {
        }

        /// <summary>
        /// Retrieve the current server metrics, including memory usage, ingestion rates, disk consumption, etc.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>Current server metrics.</returns>
        public async Task<ServerMetricsEntity> GetServerMetricsAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<ServerMetricsEntity>("ServerMetrics", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve status messages describing the state of the server.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>Status messages.</returns>
        public async Task<ServerStatusPart> GetServerStatusAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<ServerStatusPart>("ServerStatus", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the server ingestion log, including information about failed event ingestion.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The server ingestion log.</returns>
        public async Task<string> GetIngestionLogAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetStringAsync("IngestionLog", cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Retrieve a detailed system metric.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <param name="measurement">The measurement to get.</param>
        /// <returns>A timeseries showing the measurement over time.</returns>
        public async Task<MeasurementTimeseriesPart> GetMeasurementTimeseriesAsync(string measurement, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>{ ["measurement"] = measurement };
            return await GroupGetAsync<MeasurementTimeseriesPart>("Metric", parameters, cancellationToken);
        }

        /// <summary>
        /// Report on storage space consumed by the event store across a range of timestamps. The returned range may be
        /// extended to account for the resolution of the underlying data.
        /// </summary>
        /// <param name="rangeStart">The (inclusive) start of the range to report on. If omitted, the results will report from the
        /// earliest stored data. The range start must land on a five-minute boundary.</param>
        /// <param name="rangeEnd">The (exclusive) end of the range to report on. If omitted, the results will report from the
        /// earliest stored data. The range must be a multiple of the interval size, or a whole number of days if
        /// no interval is specified.</param>
        /// <param name="intervalMinutes">The bucket size to use. Must be a multiple of 5 minutes. Defaults to 1440 (one day).</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>Storage consumption information.</returns>
        public async Task<StorageConsumptionPart> GetStorageConsumptionAsync(
            DateTime? rangeStart,
            DateTime? rangeEnd,
            int? intervalMinutes,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>
            {
                ["rangeStart"] = rangeStart,
                ["rangeEnd"] = rangeEnd,
                ["intervalMinutes"] = intervalMinutes
            };
            return await GroupGetAsync<StorageConsumptionPart>("Storage", parameters, cancellationToken);
        }
    }
}
