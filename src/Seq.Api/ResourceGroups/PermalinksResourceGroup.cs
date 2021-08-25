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
using Seq.Api.Model.Permalinks;
using Seq.Api.Model.Users;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on permalinked events.
    /// </summary>
    public class PermalinksResourceGroup : ApiResourceGroup
    {
        internal PermalinksResourceGroup(ILoadResourceGroup connection)
            : base("Permalinks", connection)
        {
        }

        /// <summary>
        /// Retrieve the permalink with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the permalink.</param>
        /// <param name="renderEvent">If <c>true</c> and <paramref name="includeEvent"/> is <c>true</c>, then the rendered message will be included in the event; otherwise, event
        /// information will include the message template only.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <param name="includeEvent">If <c>true</c>, the event payload will be returned along with permalink metadata; otherwise, only metadata will be returned.</param>
        /// <returns>The permalink.</returns>
        public async Task<PermalinkEntity> FindAsync(
            string id,
            bool includeEvent = false,
            bool renderEvent = false,
            CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            var parameters = new Dictionary<string, object>
            {
                {"id", id},
                {"includeEvent", includeEvent},
                {"renderEvent", renderEvent}
            };
            return await GroupGetAsync<PermalinkEntity>("Item", parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve permalinks.
        /// </summary>
        /// <param name="ownerId">If the id of a <see cref="UserEntity"/> is provided, only permalinks owned by them will be included in the result; if
        /// not specified, permalinks for all owners will be listed.</param>
        /// <param name="renderEvent">If <c>true</c> and <paramref name="includeEvent"/> is <c>true</c>, then the rendered message will be included in the event; otherwise, event
        /// information will include the message template only.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <param name="includeEvent">If <c>true</c>, the event payload will be returned along with permalink metadata; otherwise, only metadata will be returned.</param>
        /// <returns>A list containing matching permalinks.</returns>
        public async Task<List<PermalinkEntity>> ListAsync(
            string ownerId = null,
            bool includeEvent = false,
            bool renderEvent = false,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"includeEvent", includeEvent},
                {"renderEvent", renderEvent},
                { "ownerId", ownerId }
            };

            return await GroupListAsync<PermalinkEntity>("Items", parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Construct a permalink with server defaults pre-initialized.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The unsaved permalink.</returns>
        public async Task<PermalinkEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<PermalinkEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a new permalink.
        /// </summary>
        /// <param name="entity">The permalink to add.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The permalink, with server-allocated properties such as <see cref="Entity.Id"/> initialized.</returns>
        public async Task<PermalinkEntity> AddAsync(PermalinkEntity entity, CancellationToken cancellationToken = default)
        {
            return await GroupCreateAsync<PermalinkEntity, PermalinkEntity>(entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove an existing permalink.
        /// </summary>
        /// <param name="entity">The permalink to remove.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task RemoveAsync(PermalinkEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an existing permalink.
        /// </summary>
        /// <param name="entity">The permalink to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task UpdateAsync(PermalinkEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
