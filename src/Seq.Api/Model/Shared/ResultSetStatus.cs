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

namespace Seq.Api.Model.Shared
{
    /// <summary>
    /// Information about the state of an event result set.
    /// </summary>
    public enum ResultSetStatus
    {
        /// <summary>
        /// Uninitialized value.
        /// </summary>
        Unknown,

        /// <summary>
        /// Still more to search (even if this result set is empty).
        /// </summary>
        Partial,

        /// <summary>
        /// Covers the whole range.
        /// </summary>
        Complete,

        /// <summary>
        /// Retrieved the requested event count, then stopped.
        /// </summary>
        Full
    }
}
