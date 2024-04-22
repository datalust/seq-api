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

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Seq.Api.Model.Security;
using Seq.Api.Model.Shared;

namespace Seq.Api.Model.Signals
{
    /// <summary>
    /// A signal is a collection of filters that identifies a subset of the event stream.
    /// </summary>
    public class SignalEntity : Entity
    {
        /// <summary>
        /// Construct a <see cref="SignalEntity"/>.
        /// </summary>
        public SignalEntity()
        {
            Title = "New Signal";
            Filters = new List<DescriptiveFilterPart>();
            Columns = new List<SignalColumnPart>();
        }

        /// <summary>
        /// The friendly, human readable title of the signal.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A long-form description of the signal's purpose and contents.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Filters that are combined (using the <c>and</c> operator) to identify events matching the filter.
        /// </summary>
        public List<DescriptiveFilterPart> Filters { get; set; }

        /// <summary>
        /// Expressions that show as columns when the signal is selected in the events screen.
        /// </summary>
        public List<SignalColumnPart> Columns { get; set; }

        /// <summary>
        /// Obsolete.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        [Obsolete("This member has been renamed `IsProtected` to better reflect its purpose.")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? IsRestricted { get; set; }

        /// <summary>
        /// If <c>true</c>, the signal can only be modified by users with the <see cref="Permission.Project"/> permission.
        /// </summary>
        public bool IsProtected { get; set; }
        
        /// <summary>
        /// If <c>true</c>, the signal has no backing index.
        /// </summary>
        public bool IsIndexSuppressed { get; set; }

        /// <summary>
        /// How the signal is grouped in the Seq UI.
        /// </summary>
        public SignalGrouping Grouping { get; set; }

        /// <summary>
        /// If <see cref="Grouping"/> is <see cref="SignalGrouping.Explicit"/>, the name of the group in which the signal appears.
        /// </summary>
        public string ExplicitGroupName { get; set; }

        /// <summary>
        /// The user id of the user who owns the signal; if <c>null</c>, the signal is shared.
        /// </summary>
        public string OwnerId { get; set; }
    }
}
