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

namespace Seq.Api.Model.Dashboarding
{
    /// <summary>
    /// How a chart will be displayed on a dashboard.
    /// </summary>
    public class ChartDisplayStylePart
    {
        /// <summary>
        /// The width of the chart, in 1/12th columns.
        /// </summary>
        public int WidthColumns { get; set; } = 6;

        /// <summary>
        /// The height of the chart, in rows.
        /// </summary>
        public int HeightRows { get; set; } = 1;
    }
}
