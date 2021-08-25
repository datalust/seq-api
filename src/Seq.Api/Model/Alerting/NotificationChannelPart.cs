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

using System.Collections.Generic;
using Seq.Api.Model.AppInstances;

namespace Seq.Api.Model.Alerting
{
    /// <summary>
    /// A notification channel belonging to an alert.
    /// </summary>
    public class NotificationChannelPart
    {
        /// <summary>
        /// A system-assigned identifier for the channel. This is used when updating channels to carry over unchanged
        /// values of sensitive settings, which are not round-tripped to the client for editing. When creating a
        /// new channel, this should be <c>null</c>.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// The message used for the title or subject of the notification. If not specified, a default message based
        /// on the alert title will be used.
        /// </summary>
        public string NotificationMessage { get; set; }

        /// <summary>
        /// If <c>true</c>, notifications will include a sample of the events that contributed to the triggering of
        /// the alert.
        /// </summary>
        public bool IncludeContributingEvents { get; set; }
        
        /// <summary>
        /// When <see cref="IncludeContributingEvents"/> is <c>true</c>, the maximum number of contributing events to
        /// include in the notification. Note that this value is an upper limit, and server resource constraints may
        /// prevent all contributing events from being included even below this limit.
        /// </summary>
        public uint? IncludedContributingEventLimit { get; set; }

        /// <summary>
        /// The id of an <see cref="AppInstanceEntity"/> that will receive notifications from the alert.
        /// </summary>
        public string NotificationAppInstanceId { get; set; }

        /// <summary>
        /// Additional properties that will be used to configure the notification app when triggered
        /// by the alert.
        /// </summary>
        public Dictionary<string, string> NotificationAppSettingOverrides { get; set; } =
            new Dictionary<string, string>();
    }
}
