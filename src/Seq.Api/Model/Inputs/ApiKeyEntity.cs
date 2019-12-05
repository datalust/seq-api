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
using Newtonsoft.Json;
using Seq.Api.Model.LogEvents;
using Seq.Api.Model.Security;
using Seq.Api.Model.Signals;

namespace Seq.Api.Model.Inputs
{
    public class ApiKeyEntity : Entity
    {
        public string Title { get; set; }
        public string Token { get; set; }
        public string TokenPrefix { get; set; }
        public List<InputAppliedPropertyPart> AppliedProperties { get; set; } = new List<InputAppliedPropertyPart>();
        public SignalFilterPart InputFilter { get; set; } = new SignalFilterPart();
        public LogEventLevel? MinimumLevel { get; set; }
        public bool UseServerTimestamps { get; set; }
        public bool IsDefault { get; set; }
        public string OwnerId { get; set; }
        public HashSet<Permission> AssignedPermissions { get; set; } = new HashSet<Permission>();

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ApiKeyMetricsPart Metrics { get; set; } = new ApiKeyMetricsPart();
    }
}
