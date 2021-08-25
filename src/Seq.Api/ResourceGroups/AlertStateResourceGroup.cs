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
using Seq.Api.Model.Alerting;
using Seq.Api.Model.Dashboarding;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Inspect the current states of alerts being monitored by the server.
    /// </summary>
    public class AlertStateResourceGroup : ApiResourceGroup
    {
        internal AlertStateResourceGroup(ILoadResourceGroup connection)
            : base("AlertState", connection)
        {
        }

        /// <summary>
        /// Retrieve the alert state with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the alert state.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The alert state.</returns>
        public async Task<AlertStateEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<AlertStateEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the states of all alerts being monitored by the server.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing all current alert states.</returns>
        public async Task<List<AlertStateEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<AlertStateEntity>("Items", null, cancellationToken).ConfigureAwait(false);
        }
    
        /// <summary>
        /// Remove an alert state; this will remove the corresponding alert.
        /// </summary>
        /// <param name="entity">The alert state to remove</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        public async Task RemoveAsync(AlertStateEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
