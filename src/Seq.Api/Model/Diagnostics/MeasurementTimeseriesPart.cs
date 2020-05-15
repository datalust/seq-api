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

using System;

namespace Seq.Api.Model.Diagnostics
{
    /// <summary>
    /// A histogram presenting a measurement taken at equal intervals.
    /// </summary>
    public class MeasurementTimeseriesPart
    {
        /// <summary>
        /// The point in time from which measurement begins.
        /// </summary>
        public DateTime MeasuredFrom { get; set; }
        
        /// <summary>
        /// The interval at which the measurement is taken.
        /// </summary>
        public ulong MeasurementIntervalMilliseconds { get; set; }
        
        /// <summary>
        /// The measurements at each interval, beginning with <see cref="MeasuredFrom"/>.
        /// </summary>
        public long[] Measurements { get; set; }
    }
}
