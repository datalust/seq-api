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
    /// An expression-based filter that carries additional descriptive information.
    /// </summary>
    public class DescriptiveFilterPart
    {
        /// <summary>
        /// A friendly, human-readable description of the filter.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// If <c>true</c>, the description identifies events excluded by the filter. The
        /// Seq UI uses this to show the description in strikethrough.
        /// </summary>
        public bool DescriptionIsExcluded { get; set; }

        /// <summary>
        /// The strictly-valid expression language filter that identifies matching events.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// The original ("fuzzy") text entered by the user into the filter bar when
        /// creating the filter. This may not be syntactically valid, e.g. it may be
        /// interpreted as free text - hence while it's displayed in the UI and forms the
        /// basis of user editing of the filter, the <see cref="Filter"/> value is executed.
        /// </summary>
        public string FilterNonStrict { get; set; }
    }
}
