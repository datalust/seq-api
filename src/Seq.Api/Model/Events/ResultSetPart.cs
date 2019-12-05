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

using System.Collections.Generic;
using Seq.Api.Model.Shared;

namespace Seq.Api.Model.Events
{
    /// <summary>
    /// The result of a request for events matching a filter or signal.
    /// </summary>
    public class ResultSetPart
    {
        /// <summary>
        /// Matching events.
        /// </summary>
        public List<EventEntity> Events { get; set; }

        /// <summary>
        /// Statistics describing the server resources used to construct the
        /// result set.
        /// </summary>
        public StatisticsPart Statistics { get; set; }
    }
}
