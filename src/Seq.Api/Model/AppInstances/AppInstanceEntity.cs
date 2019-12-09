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

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Seq.Api.Model.Apps;
using Seq.Api.Model.Signals;
using Seq.Api.ResourceGroups;

namespace Seq.Api.Model.AppInstances
{
    /// <summary>
    /// App instances are individual processes based on a running <see cref="AppEntity"/> that can
    /// read from and write to the Seq event stream.
    /// </summary>
    public class AppInstanceEntity : Entity
    {
        /// <summary>
        /// Construct an <see cref="AppInstanceEntity"/> with default values.
        /// </summary>
        /// <remarks>Instead of constructing an instance directly, consider using
        /// <see cref="AppInstancesResourceGroup.TemplateAsync"/> to obtain a partly-initialized instance.</remarks>
        public AppInstanceEntity()
        {
            Settings = new Dictionary<string, string>();
            InvocationOverridableSettings = new List<string>();
            InvocationOverridableSettingDefinitions = new List<AppSettingPart>();
            EventsPerSuppressionWindow = 1;
            Metrics = new AppInstanceMetricsPart();
        }

        /// <summary>
        /// The id of the <see cref="AppEntity"/> that this is an instance of.
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// The user-friendly title of the app instance.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Values for the settings exposed by the app.
        /// </summary>
        public Dictionary<string, string> Settings { get; set; }

        /// <summary>
        /// If <c>true</c>, administrative users may invoke the app manually or through alerts.
        /// This field is read-only from the API and generally indicates that the app is an input.
        /// </summary>
        public bool AcceptPrivilegedInvocation { get; set; }

        /// <summary>
        /// If <c>true</c>, regular users can manually send events to the app, or use the app
        /// as the target for alert notifications.
        /// </summary>
        public bool AcceptDirectInvocation { get; set; }

        /// <summary>
        /// The settings that can be overridden at invocation time (when an event is sent to
        /// the instance).
        /// </summary>
        public List<string> InvocationOverridableSettings { get; set; }

        /// <summary>
        /// Metadata describing the overridable settings. This field is provided by the server
        /// and cannot be modified.
        /// </summary>
        public List<AppSettingPart> InvocationOverridableSettingDefinitions { get; set; }

        /// <summary>
        /// If <c>true</c>, events will be streamed to the app. Otherwise, events will be
        /// sent only manually or in response to alerts being triggered.
        /// </summary>
        public bool AcceptStreamedEvents { get; set; }

        /// <summary>
        /// The signal expression describing which events will be sent to the app; if <c>null</c>,
        /// all events will reach the app.
        /// </summary>
        public SignalExpressionPart InputSignalExpression { get; set; }

        /// <summary>
        /// If a value is specified, events will be buffered to disk and sorted by timestamp-order
        /// within the specified window. This is not recommended for performance reasons, and should
        /// be avoided when possible.
        /// </summary>
        public TimeSpan? ArrivalWindow { get; set; }

        /// <summary>
        /// The time after an event reaches the app during which no further events will be processed.
        /// The default <see cref="TimeSpan.Zero"/> indicates no suppression time, and all events
        /// will be processed in that case.
        /// </summary>
        public TimeSpan SuppressionTime { get; set; }

        /// <summary>
        /// If <see cref="SuppressionTime"/> is set, the number of events that will be allowed during the
        /// suppression window. The default is <c>1</c>, to allow only the initial event that triggered suppression.
        /// </summary>
        public int EventsPerSuppressionWindow { get; set; }

        /// <summary>
        /// Metrics describing the state and activity of the app.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public AppInstanceMetricsPart Metrics { get; set; }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete("Use !AcceptStreamedEvents instead. This field will be removed in Seq 6.0.")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? IsManualInputOnly { get; set; }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete("Use !AcceptDirectInvocation instead. This field will be removed in Seq 6.0.")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? DisallowManualInput { get; set; }
    }
}
