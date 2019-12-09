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
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model;
using Seq.Api.Model.Monitoring;
using Seq.Api.Model.Users;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on dashboards. Dashboards are collections of charts and alerts that are either shared or user-specific.
    /// </summary>
    public class DashboardsResourceGroup : ApiResourceGroup
    {
        internal DashboardsResourceGroup(ISeqConnection connection)
            : base("Dashboards", connection)
        {
        }

        /// <summary>
        /// Retrieve the dashboard with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the dashboard.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The dashboard.</returns>
        public async Task<DashboardEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<DashboardEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve dashboards.
        /// </summary>
        /// <param name="ownerId">If the id of a <see cref="UserEntity"/> is provided, only dashboards owned by them will be included in the result; if
        /// not specified, personal dashboards for all owners will be listed.</param>
        /// <param name="shared">If <c>true</c>, shared dashboards will be included in the result.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing matching dashboards.</returns>
        public async Task<List<DashboardEntity>> ListAsync(string ownerId = null, bool shared = false, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object> { { "ownerId", ownerId }, { "shared", shared } };
            return await GroupListAsync<DashboardEntity>("Items", parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Construct a dashboard with server defaults pre-initialized.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The unsaved dashboard.</returns>
        public async Task<DashboardEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<DashboardEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a new dashboard.
        /// </summary>
        /// <param name="entity">The dashboard to add.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The dashboard, with server-allocated properties such as <see cref="Entity.Id"/> initialized.</returns>
        public async Task<DashboardEntity> AddAsync(DashboardEntity entity, CancellationToken cancellationToken = default)
        {
            return await GroupCreateAsync<DashboardEntity, DashboardEntity>(entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove an existing dashboard.
        /// </summary>
        /// <param name="entity">The dashboard to remove.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task RemoveAsync(DashboardEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an existing dashboard.
        /// </summary>
        /// <param name="entity">The dashboard to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task UpdateAsync(DashboardEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}