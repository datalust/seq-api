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
    /// The color palette used for displaying a measurement on a chart.
    /// </summary>
    public enum MeasurementDisplayPalette
    {
        /// <summary>
        /// The default palette.
        /// </summary>
        Default,

        /// <summary>
        /// A predominantly red palette.
        /// </summary>
        Reds,

        /// <summary>
        /// A predominantly green palette.
        /// </summary>
        Greens,

        /// <summary>
        /// A predominantly blue palette.
        /// </summary>
        Blues,

        /// <summary>
        /// An orange/purple palette.
        /// </summary>
        OrangePurple
    }
}
