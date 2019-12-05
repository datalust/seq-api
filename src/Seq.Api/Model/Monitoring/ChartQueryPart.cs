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
    public class ChartQueryPart
    {
        public string Id { get; set; }
        public List<MeasurementPart> Measurements { get; set; } = new List<MeasurementPart>();
        public string Where { get; set; }
        public SignalExpressionPart SignalExpression { get; set; }
        public List<string> GroupBy { get; set; } = new List<string>();
        public MeasurementDisplayStylePart DisplayStyle { get; set; } = new MeasurementDisplayStylePart();
        public List<AlertPart> Alerts { get; set; } = new List<AlertPart>();
        public string Having { get; set; }
        public List<string> OrderBy { get; set; } = new List<string>();
        public int? Limit { get; set; }
    }
}
