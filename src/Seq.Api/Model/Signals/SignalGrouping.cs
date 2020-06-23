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

namespace Seq.Api.Model.Signals
{
    /// <summary>
    /// The method by which a signal is grouped in the Seq UI.
    /// </summary>
    public enum SignalGrouping
    {
        /// <summary>
        /// The grouping is inferred from the filters within the signal. Currently, this
        /// will result in the signal being grouped only if the signal has a single
        /// equality-based filter.
        /// </summary>
        Inferred,

        /// <summary>
        /// The signal is given an explicit group name.
        /// </summary>
        Explicit,

        /// <summary>
        /// The signal is never displayed in a group.
        /// </summary>
        None
    }
}
