﻿// Copyright © Datalust and contributors. 
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
using Seq.Api.Model.Inputs;
using Seq.Api.Model.Signals;
using Seq.Api.ResourceGroups;
// ReSharper disable UnusedAutoPropertyAccessor.Global

#nullable enable

namespace Seq.Api.Model.AppInstances;

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
        ProcessMetrics = new AppInstanceProcessMetricsPart();
        InputSettings = new InputSettingsPart();
        InputMetrics = new InputMetricsPart();
        DiagnosticInputMetrics = new InputMetricsPart();
        OutputMetrics = new AppInstanceOutputMetricsPart();
    }

    /// <summary>
    /// The id of the <see cref="AppEntity"/> that this is an instance of.
    /// </summary>
    public string? AppId { get; set; }

    /// <summary>
    /// The user-friendly title of the app instance.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Values for the settings exposed by the app.
    /// </summary>
    public Dictionary<string, string>? Settings { get; set; }

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
    public List<string>? InvocationOverridableSettings { get; set; }

    /// <summary>
    /// Metadata describing the overridable settings. This field is provided by the server
    /// and cannot be modified.
    /// </summary>
    public List<AppSettingPart>? InvocationOverridableSettingDefinitions { get; set; }

    /// <summary>
    /// If <c>true</c>, events will be streamed to the app. Otherwise, events will be
    /// sent only manually or in response to alerts being triggered.
    /// </summary>
    public bool AcceptStreamedEvents { get; set; }

    /// <summary>
    /// The signal expression describing which events will be sent to the app; if <c>null</c>,
    /// all events will reach the app.
    /// </summary>
    public SignalExpressionPart? StreamedSignalExpression { get; set; }

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
    /// Settings that control how events are ingested through the app.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InputSettingsPart? InputSettings { get; set; }

    /// <summary>
    /// Metrics describing the state and activity of the app process.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public AppInstanceProcessMetricsPart? ProcessMetrics { get; set; }
        
    /// <summary>
    /// Information about ingestion activity through this app.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InputMetricsPart? InputMetrics { get; set; }

    /// <summary>
    /// Information about the app's diagnostic input.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InputMetricsPart? DiagnosticInputMetrics { get; set; }

    /// <summary>
    /// Information about events output through the app.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public AppInstanceOutputMetricsPart? OutputMetrics { get; set; }

    /// <summary>
    /// The name of the app.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? AppName { get; set; }

    /// <summary>
    /// If <c>true</c>, then the app is able to write events to the log.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? IsInput { get; set; }
}
