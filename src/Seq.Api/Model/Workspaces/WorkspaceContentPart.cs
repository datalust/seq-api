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

using System.Collections.Generic;
using Seq.Api.Model.Monitoring;
using Seq.Api.Model.Signals;
using Seq.Api.Model.SqlQueries;

namespace Seq.Api.Model.Workspaces
{
    /// <summary>
    /// The items included in a <see cref="WorkspaceEntity"/>.
    /// </summary>
    public class WorkspaceContentPart
    {
        /// <summary>
        /// A list of <see cref="SignalEntity"/> ids to include in the workspace.
        /// </summary>
        public List<string> SignalIds { get; set; } = new List<string>();

        /// <summary>
        /// A list of <see cref="SqlQueryEntity"/> ids to include in the workspace.
        /// </summary>
        public List<string> QueryIds { get; set; } = new List<string>();
        
        /// <summary>
        /// A list of <see cref="DashboardEntity"/> ids to include in the workspace.
        /// </summary>
        public List<string> DashboardIds { get; set; } = new List<string>();
    }
}