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

using Newtonsoft.Json;

namespace Seq.Api.Model.Data
{
    /// <summary>
    /// The result of executing a SQL-style query. Results are hierarchical, rather
    /// than tabular, to reduce the amount of data transfer required to send events to
    /// the client.
    /// </summary>
    /// <remarks>
    /// Only one of <see cref="Rows"/>, <see cref="Slices"/>, or <see cref="Series"/>
    /// will be present for a given result set.
    /// Generally, the CSV-based query endpoints are more convenient to use for
    /// simple ETL tasks.</remarks>
    public class QueryResultPart
    {
        /// <summary>
        /// The columns within the result set (at various levels of the hierarchy).
        /// </summary>
        public string[] Columns { get; set; }

        /// <summary>
        /// Result rows.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object[][] Rows { get; set; }

        /// <summary>
        /// Result time slices.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public TimeSlicePart[] Slices { get; set; }

        /// <summary>
        /// Result timeseries.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public TimeseriesPart[] Series { get; set; }

        /// <summary>
        /// On error only, a description of the error.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Error { get; set; }

        /// <summary>
        /// Reasons for the <see cref="Error"/>.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[] Reasons { get; set; }

        /// <summary>
        /// A corrected version of the query, if one could be suggested.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Suggestion { get; set; }

        /// <summary>
        /// Execution statistics.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public QueryExecutionStatisticsPart Statistics { get; set; }
    }
}