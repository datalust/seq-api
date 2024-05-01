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
using Seq.Api.Model.Signals;
using Seq.Api.Model.Users;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on signals. A signal is a collection of filters that identifies a subset of the event stream.
    /// </summary>
    public class SignalsResourceGroup : ApiResourceGroup
    {
        internal SignalsResourceGroup(ILoadResourceGroup connection)
            : base("Signals", connection)
        {
        }

        /// <summary>
        /// Retrieve the signal with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the signal.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The signal.</returns>
        public async Task<SignalEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<SignalEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve signals.
        /// </summary>
        /// <param name="ownerId">If the id of a <see cref="UserEntity"/> is provided, only signals owned by them will be included in the result; if
        /// not specified, personal signals for all owners will be listed.</param>
        /// <param name="shared">If <c>true</c>, shared signals will be included in the result.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing matching signals.</returns>
        public async Task<List<SignalEntity>> ListAsync(string ownerId = null, bool shared = false, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object> { { "ownerId", ownerId }, { "shared", shared } };
            return await GroupListAsync<SignalEntity>("Items", parameters, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Construct a signal with server defaults pre-initialized.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The unsaved signal.</returns>
        public async Task<SignalEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<SignalEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a new signal.
        /// </summary>
        /// <param name="entity">The signal to add.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The signal, with server-allocated properties such as <see cref="Entity.Id"/> initialized.</returns>
        public async Task<SignalEntity> AddAsync(SignalEntity entity, CancellationToken cancellationToken = default)
        {
            return await GroupCreateAsync<SignalEntity, SignalEntity>(entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Remove an existing signal.
        /// </summary>
        /// <param name="entity">The signal to remove.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task RemoveAsync(SignalEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an existing signal.
        /// </summary>
        /// <param name="entity">The signal to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task UpdateAsync(SignalEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}