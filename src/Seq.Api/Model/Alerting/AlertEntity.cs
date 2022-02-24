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
using System.Collections.Generic;
using Seq.Api.Model.LogEvents;
using Seq.Api.Model.Security;
using Seq.Api.Model.Shared;
using Seq.Api.Model.Signals;

namespace Seq.Api.Model.Alerting
{
    /// <summary>
    /// An alert.
    /// </summary>
    public class AlertEntity : Entity
    {
        /// <summary>
        /// A friendly, human-readable title for the alert.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// An optional long-form description of the alert.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The user id of the user who owns the alert; if <c>null</c>, the alert is shared.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// If <c>true</c>, the alert can only be modified by users with the <see cref="Permission.Project"/> permission.
        /// </summary>
        public bool IsProtected { get; set; }
        
        /// <summary>
        /// If <c>true</c>, the alert will not be processed, and notifications will not be sent.
        /// </summary>
        public bool IsDisabled { get; set; }
        
        /// <summary>
        /// An optional <see cref="SignalExpressionPart"/> limiting the data source that triggers the alert.
        /// </summary>
        public SignalExpressionPart SignalExpression { get; set; }

        /// <summary>
        /// An optional <c>where</c> clause limiting the data source that triggers the alert.
        /// </summary>
        public string Where { get; set; }
        
        /// <summary>
        /// Additional groupings applied to the data source. The <c>time()</c> grouping is controlled by the alerting
        /// infrastructure according to the <see cref="TimeGrouping"/> property and should not be specified here.
        /// </summary>
        public List<GroupingColumnPart> GroupBy { get; set; } = new List<GroupingColumnPart>();

        /// <summary>
        /// The interval over which the alert condition will be measured.
        /// </summary>
        public TimeSpan TimeGrouping { get; set; }
        
        /// <summary>
        /// The individual measurements that will be tested by the alert condition.
        /// </summary>
        public List<ColumnPart> Select { get; set; } = new List<ColumnPart>();
        
        /// <summary>
        /// The alert condition. This is a <c>having</c> clause over the grouped results
        /// computed by the alert query.
        /// </summary>
        public string Having { get; set; }
        
        /// <summary>
        /// A level indicating the severity or priority of the alert.
        /// </summary>
        public LogEventLevel NotificationLevel { get; set; }
        
        /// <summary>
        /// Additional properties that will be attached to the generated notification.
        /// </summary>
        public List<EventPropertyPart> NotificationProperties { get; set; } = new List<EventPropertyPart>();

        /// <summary>
        /// The channels that will receive notifications when the alert is triggered.
        /// </summary>
        public List<NotificationChannelPart> NotificationChannels { get; set; } = new List<NotificationChannelPart>();

        /// <summary>
        /// The time after the alert is triggered within which no further notifications will be sent.
        /// </summary>
        public TimeSpan SuppressionTime { get; set; }

        /// <summary>
        /// Any recent activity for the alert.
        /// </summary>
        public AlertActivityPart Activity { get; set; }
    }
}
