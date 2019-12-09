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

namespace Seq.Api.Model.Users
{
    /// <summary>
    /// A user's search history.
    /// </summary>
    public class SearchHistoryEntity : Entity
    {
        /// <summary>
        /// The number of recent searches that the server will retain for the user.
        /// </summary>
        public uint RetainedRecentSearches { get; set; }

        /// <summary>
        /// Recent un-pinned searches, with the most recent included first.
        /// </summary>
        public List<string> Recent { get; set; }

        /// <summary>
        /// Pinned search history items, with the most recent included first.
        /// </summary>
        public List<string> Pinned { get; set; } 
    }
}