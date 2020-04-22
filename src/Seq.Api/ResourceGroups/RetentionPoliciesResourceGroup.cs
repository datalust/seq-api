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
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model;
using Seq.Api.Model.Retention;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on retention policies.
    /// </summary>
    public class RetentionPoliciesResourceGroup : ApiResourceGroup
    {
        internal RetentionPoliciesResourceGroup(ISeqConnection connection)
            : base("RetentionPolicies", connection)
        {
        }

        /// <summary>
        /// Retrieve the retention policy with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the retention policy.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The retention policy.</returns>
        public async Task<RetentionPolicyEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<RetentionPolicyEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve retention policies.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing matching retention policies.</returns>
        public async Task<List<RetentionPolicyEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<RetentionPolicyEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Construct a retention policy with server defaults pre-initialized.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The unsaved retention policy.</returns>
        public async Task<RetentionPolicyEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<RetentionPolicyEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a new retention policy.
        /// </summary>
        /// <param name="entity">The retention policy to add.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The retention policy, with server-allocated properties such as <see cref="Entity.Id"/> initialized.</returns>
        public async Task<RetentionPolicyEntity> AddAsync(RetentionPolicyEntity entity, CancellationToken cancellationToken = default)
        {
            return await GroupCreateAsync<RetentionPolicyEntity, RetentionPolicyEntity>(entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove an existing retention policy.
        /// </summary>
        /// <param name="entity">The retention policy to remove.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task RemoveAsync(RetentionPolicyEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an existing retention policy.
        /// </summary>
        /// <param name="entity">The retention policy to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task UpdateAsync(RetentionPolicyEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}