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
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model;
using Seq.Api.Model.Alerting;
using Seq.Api.Model.Signals;
using Seq.Api.Model.Users;

namespace Seq.Api.ResourceGroups
{    
    /// <summary>
    /// Create and manage alerts being monitored by the server.
    /// </summary>
    public class AlertsResourceGroup : ApiResourceGroup
    {        
        internal AlertsResourceGroup(ILoadResourceGroup connection)
            : base("Alerts", connection)
        {
        }

        /// <summary>
        /// Retrieve the dashboard with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the dashboard.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The dashboard.</returns>
        public async Task<AlertEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<AlertEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve alerts.
        /// </summary>
        /// <param name="ownerId">If the id of a <see cref="UserEntity"/> is provided, only alerts owned by them will be included in the result; if
        /// not specified, personal alerts for all owners will be listed.</param>
        /// <param name="shared">If <c>true</c>, shared alerts will be included in the result.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing matching alerts.</returns>
        public async Task<List<AlertEntity>> ListAsync(string ownerId = null, bool shared = false, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object> { { "ownerId", ownerId }, { "shared", shared } };
            return await GroupListAsync<AlertEntity>("Items", parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Construct a alert with server defaults pre-initialized.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <param name="signal">The source of events that will contribute to the alert.</param>
        /// <param name="query">An SQL query that will supply default clauses to the new alert.</param>
        /// <returns>The unsaved alert.</returns>
        public async Task<AlertEntity> TemplateAsync(SignalExpressionPart signal = null, string query = null, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>
            {
                ["signal"] = signal?.ToString(),
                ["q"] = query
            };
            
            return await GroupGetAsync<AlertEntity>("Template", parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a new alert.
        /// </summary>
        /// <param name="entity">The alert to add.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The alert, with server-allocated properties such as <see cref="Entity.Id"/> initialized.</returns>
        public async Task<AlertEntity> AddAsync(AlertEntity entity, CancellationToken cancellationToken = default)
        {
            return await GroupCreateAsync<AlertEntity, AlertEntity>(entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove an existing alert.
        /// </summary>
        /// <param name="entity">The alert to remove.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task RemoveAsync(AlertEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an existing alert.
        /// </summary>
        /// <param name="entity">The alert to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task UpdateAsync(AlertEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
