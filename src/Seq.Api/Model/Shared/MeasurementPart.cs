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

namespace Seq.Api.Model.Shared
{
    /// <summary>
    /// A column with value and label.
    /// </summary>
    public class ColumnPart
    {
        /// <summary>
        /// The expression (<c>select</c>ed column) that computes the value of the measurement.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// An optional label for the measurement (effectively the right-hand size of an <c>as</c> clause).
        /// </summary>
        public string Label { get; set; }
    }
}
