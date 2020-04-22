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

namespace Seq.Api.Model.Monitoring
{
    /// <summary>
    /// How a measurement will be displayed within a chart.
    /// </summary>
    public class MeasurementDisplayStylePart
    {
        /// <summary>
        /// The type of display used for the measurement.
        /// </summary>
        public MeasurementDisplayType Type { get; set; } = MeasurementDisplayType.Line;

        /// <summary>
        /// For line chart measurement display types, whether the area under the line will be filled.
        /// </summary>
        public bool LineFillToZeroY { get; set; }

        /// <summary>
        /// For line chart measurement display types, whether the points along the line will be visibly marked.
        /// </summary>
        public bool LineShowMarkers { get; set; } = true;

        /// <summary>
        /// For bar chart measurement display types, whether the sum of all bars will be shown as an overlay.
        /// </summary>
        public bool BarOverlaySum { get; set; }

        /// <summary>
        /// For measurement display types that include a legend, whether the legend will be shown.
        /// </summary>
        public bool SuppressLegend { get; set; }

        /// <summary>
        /// The color palette used to display the chart.
        /// </summary>
        public MeasurementDisplayPalette Palette { get; set; } = MeasurementDisplayPalette.Default;
    }
}
