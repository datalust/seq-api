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

namespace Seq.Api.Model.Settings
{
    /// <summary>
    /// Settings for internal error reporting.
    /// </summary>
    public class InternalErrorReportingSettingsPart
    {
        /// <summary>
        /// If <c>true</c>, redacted internal error reports will be sent
        /// automatically to Datalust.
        /// </summary>
        public bool InternalErrorReportingEnabled { get; set; }

        /// <summary>
        /// If internal error reporting is enabled, an optional email address
        /// that will be attached to error reports so that the support team
        /// at Datalust can respond with fix/mitigation information.
        /// </summary>
        public string ReplyEmail { get; set; }
        
        /// <summary>
        /// If <c>true</c>, anonymized usage telemetry will be sent
        /// automatically to Datalust.
        /// </summary>
        public bool UsageTelemetryEnabled { get; set; }
    }
}
