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

namespace Seq.Api.Model.Inputs
{
    /// <summary>
    /// Information about ingestion activity using an API key.
    /// </summary>
    public class ApiKeyMetricsPart
    {
        /// <summary>
        /// The number of events that arrived at the server tagged with this
        /// key in the past minute.
        /// </summary>
        public int ArrivalsPerMinute { get; set; }

        /// <summary>
        /// The number of events that ingested by the server tagged with this
        /// key in the past minute.
        /// </summary>
        public int InfluxPerMinute { get; set; }
        
        /// <summary>
        /// The raw JSON bytes (approximate) tagged with this API key that were ingested
        /// by the server in the past minute.
        /// </summary>
        public long IngestedBytesPerMinute { get; set; }
    }
}