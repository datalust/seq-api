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
using Seq.Api.Model.Signals;

namespace Seq.Api.Model.Monitoring
{
    /// <summary>
    /// A query within a chart.
    /// </summary>
    public class ChartQueryPart
    {
        /// <summary>
        /// The unique id assigned to the query.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Individual measurements included in the query. These are effectively projected columns.
        /// </summary>
        public List<MeasurementPart> Measurements { get; set; } = new List<MeasurementPart>();

        /// <summary>
        /// An optional filtering <c>where</c> clause limiting the data that contributes to the chart.
        /// </summary>
        public string Where { get; set; }

        /// <summary>
        /// An optional <see cref="SignalExpressionPart"/> limiting the data that contributes to the chart.
        /// </summary>
        public SignalExpressionPart SignalExpression { get; set; }

        /// <summary>
        /// A series of expressions used to group data returned by the query.
        /// </summary>
        public List<string> GroupBy { get; set; } = new List<string>();

        /// <summary>
        /// How measurements included in the chart will be displayed.
        /// </summary>
        public MeasurementDisplayStylePart DisplayStyle { get; set; } = new MeasurementDisplayStylePart();

        /// <summary>
        /// Alerts attached to the chart.
        /// </summary>
        public List<AlertPart> Alerts { get; set; } = new List<AlertPart>();

        /// <summary>
        /// A filter that limits which groups will be displayed on the chart. Not supported by all chart types.
        /// </summary>
        public string Having { get; set; }

        /// <summary>
        /// An ordering applied to the results of the query; not supported by all chart types.
        /// </summary>
        public List<string> OrderBy { get; set; } = new List<string>();

        /// <summary>
        /// The row limit used for the query. By default, a server-determined limit will be applied.
        /// </summary>
        public int? Limit { get; set; }
    }
}
