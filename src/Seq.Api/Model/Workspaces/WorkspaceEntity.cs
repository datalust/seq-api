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

using Seq.Api.Model.Security;
using Seq.Api.Model.Signals;

namespace Seq.Api.Model.Workspaces
{
    /// <summary>
    /// A workspace is a collection of related entities that help to focus
    /// the Seq UI around a particular context.
    /// </summary>
    public class WorkspaceEntity : Entity
    {
        /// <summary>
        /// A friendly, human-readable title for the workspace.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// An optional long-form description of the workspace.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The id of the user who owns the workspace. If <c>null</c>, the workspace is shared.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// If <c>true</c>, only users with the <see cref="Permission.Setup"/> permission can modify the workspace.
        /// </summary>
        public bool IsProtected { get; set; }

        /// <summary>
        /// An optional <see cref="SignalExpressionPart"/> that will be activated when opening the <em>Events</em>
        /// screen with the workspace selected.
        /// </summary>
        public SignalExpressionPart DefaultSignalExpression { get; set; }
        
        /// <summary>
        /// Content included in the workspace.
        /// </summary>
        public WorkspaceContentPart Content { get; set; } = new WorkspaceContentPart();
    }
}
