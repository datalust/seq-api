// Copyright Datalust and contributors. 
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

namespace Seq.Api.Model.Monitoring
{
    /// <summary>
    /// Describes the state of an active alert.
    /// </summary>
    public class AlertStateEntity : Entity
    {
        /// <summary>
        /// The unique id of the alert being described.
        /// </summary>
        public string AlertId { get; set; }
        
        /// <summary>
        /// The alert's title.
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// The title of the chart to which the alert is attached.
        /// </summary>
        public string ChartTitle { get; set; }
        
        /// <summary>
        /// The title of the dashboards in which the alert is set.
        /// </summary>
        public string DashboardTitle { get; set; }

        /// <summary>
        /// The user id of the user who owns the alert; if <c>null</c>, the alert is shared.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// The notification level associated with the alert.
        /// </summary>
        public string Level { get; set; }
        
        /// <summary>
        /// The id of an app instance that will receive notifications when the alert is triggered.
        /// </summary>
        public string NotificationAppInstanceId { get; set; }

        /// <summary>
        /// The time at which the alert was last checked. Not preserved across server restarts.
        /// </summary>
        public DateTime? LastCheck { get; set; }

        /// <summary>
        /// The time at which the alert last triggered a notification. Not preserved across server restarts.
        /// </summary>
        public DateTime? LastNotification { get; set; }
        
        /// <summary>
        /// The time until which no further notifications will be sent by the alert.
        /// </summary>
        public DateTime? SuppressedUntil { get; set; }
        
        /// <summary>
        /// <c>true</c> if the alert is in the failing state; otherwise, <c>false</c>.
        /// </summary>
        public bool IsFailing { get; set; }
    }
}
