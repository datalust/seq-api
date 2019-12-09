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

namespace Seq.Api.Model.Data
{
    /// <summary>
    /// Results of a query for a given time slice.
    /// </summary>
    public class TimeSlicePart
    {
        /// <summary>
        /// The beginning of the interval contributing to the results,
        /// encoded as an ISO-8601 date/time string.
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Result rows within the interval.
        /// </summary>
        public object[][] Rows { get; set; }
    }
}