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
using Seq.Api.Model.Security;
using Seq.Api.Model.Signals;

namespace Seq.Api.Model.Dashboarding
{
    /// <summary>
    /// A dashboard.
    /// </summary>
    public class DashboardEntity : Entity
    {
        /// <summary>
        /// The user who owns the dashboard. If <c>null</c>, the dashboard is shared.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// The friendly, human-readable title for the dashboard.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// If <c>true</c>, only users with <see cref="Permission.Project"/> can modify the dashboard.
        /// </summary>
        public bool IsProtected { get; set; }

        /// <summary>
        /// An optional <see cref="SignalExpressionPart"/> that limits the data contributing to the dashboard.
        /// </summary>
        public SignalExpressionPart SignalExpression { get; set; }

        /// <summary>
        /// The list of charts included in the dashboard.
        /// </summary>
        public List<ChartPart> Charts { get; set; } = new List<ChartPart>();
    }
}
