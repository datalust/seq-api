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

using System.Collections.Generic;
using Seq.Api.Model.Signals;

namespace Seq.Api.Model.Dashboarding
{
    /// <summary>
    /// A chart that appears on a dashboard.
    /// </summary>
    public class ChartPart
    {
        /// <summary>
        /// The unique id assigned to the chart.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A human-friendly title for the chart.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// An optional <see cref="SignalExpressionPart"/> that limits what data the chart will display.
        /// </summary>
        public SignalExpressionPart SignalExpression { get; set; }

        /// <summary>
        /// The individual queries making up the chart. In most instances, only one query is currently supported
        /// here.
        /// </summary>
        public List<ChartQueryPart> Queries { get; set; } = new List<ChartQueryPart>();

        /// <summary>
        /// How the chart will appear on the dashboard.
        /// </summary>
        public ChartDisplayStylePart DisplayStyle { get; set; } = new ChartDisplayStylePart();
    }
}
