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

namespace Seq.Api.Model.Monitoring
{
    /// <summary>
    /// The method used to visually represent a measurement.
    /// </summary>
    public enum MeasurementDisplayType
    {
        /// <summary>
        /// A line chart. Requires the measurement and query to include a time axis.
        /// </summary>
        Line,

        /// <summary>
        /// A bar chart.
        /// </summary>
        Bar,

        /// <summary>
        /// A point (scatter) chart.
        /// </summary>
        Point,

        /// <summary>
        /// A single textual value. Requires that the measurement and query produce a single value.
        /// </summary>
        Value,

        /// <summary>
        /// A (donut-styled) pie chart.
        /// </summary>
        Pie,

        /// <summary>
        /// A table of raw data values.
        /// </summary>
        Table
    }
}
