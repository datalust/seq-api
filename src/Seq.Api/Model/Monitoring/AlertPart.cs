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

using Seq.Api.Model.LogEvents;
using System;
using System.Collections.Generic;

namespace Seq.Api.Model.Monitoring
{
    public class AlertPart
    {
        Dictionary<string, string> _notificationAppSettingOverrides = new Dictionary<string, string>();

        public string Id { get; set; }
        public string Condition { get; set; }
        public string Title { get; set; }
        public TimeSpan MeasurementWindow { get; set; }
        public TimeSpan StabilizationWindow { get; set; } = TimeSpan.FromSeconds(30);
        public TimeSpan SuppressionTime { get; set; }
        public LogEventLevel Level { get; set; } = LogEventLevel.Warning;
        public string NotificationAppInstanceId { get; set; }

        public Dictionary<string, string> NotificationAppSettingOverrides
        {
            get => _notificationAppSettingOverrides;
            set => _notificationAppSettingOverrides = value ?? new Dictionary<string, string>();
        }
    }
}
