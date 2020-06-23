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

using Seq.Api.Model.LogEvents;
using System;
using System.Collections.Generic;
using Seq.Api.Model.AppInstances;

namespace Seq.Api.Model.Monitoring
{
    /// <summary>
    /// An alert attached to a dashboard chart.
    /// </summary>
    public class AlertPart
    {
        Dictionary<string, string> _notificationAppSettingOverrides = new Dictionary<string, string>();

        /// <summary>
        /// The unique id assigned to the alert.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The alert condition. This is effectively a <c>having</c> clause over the grouped results
        /// computed by the <see cref="ChartQueryPart"/>.
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// A friendly, human-readable description of the alert.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The interval within which the alert condition will be measured.
        /// </summary>
        public TimeSpan MeasurementWindow { get; set; }

        /// <summary>
        /// The time allowed for events to reach the server before being included in alert calculations.
        /// This is relative to the current server clock.
        /// </summary>
        public TimeSpan StabilizationWindow { get; set; } = TimeSpan.FromSeconds(30);

        /// <summary>
        /// The time after the alert files within which no further notifications will be sent for the
        /// alert.
        /// </summary>
        public TimeSpan SuppressionTime { get; set; }

        /// <summary>
        /// An informational level that suggests the importance of the alert.
        /// </summary>
        public LogEventLevel Level { get; set; } = LogEventLevel.Warning;

        /// <summary>
        /// The id of an <see cref="AppInstanceEntity"/> that will receive notifications from the alert.
        /// </summary>
        public string NotificationAppInstanceId { get; set; }

        /// <summary>
        /// Additional properties that will be used to configure the notification app when triggered
        /// by the alert.
        /// </summary>
        public Dictionary<string, string> NotificationAppSettingOverrides
        {
            get => _notificationAppSettingOverrides;
            set => _notificationAppSettingOverrides = value ?? new Dictionary<string, string>();
        }
    }
}
