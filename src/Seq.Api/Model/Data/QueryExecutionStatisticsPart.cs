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

namespace Seq.Api.Model.Data
{
    /// <summary>
    /// Information describing the execution of a SQL-style query.
    /// </summary>
    public class QueryExecutionStatisticsPart
    {
        /// <summary>
        /// The server-side elapsed time taken to compute the query result.
        /// </summary>
        public double ElapsedMilliseconds { get; set; }
    }
}
