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
using Seq.Api.Model.Feeds;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on NuGet feeds.
    /// </summary>
    public class FeedsResourceGroup : ApiResourceGroup
    {
        internal FeedsResourceGroup(ILoadResourceGroup connection)
            : base("Feeds", connection)
        {
        }

        /// <summary>
        /// Retrieve the feed with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the feed.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The feed.</returns>
        public async Task<NuGetFeedEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<NuGetFeedEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve feeds.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing matching feeds.</returns>
        public async Task<List<NuGetFeedEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<NuGetFeedEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Construct a feed with server defaults pre-initialized.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The unsaved feed.</returns>
        public async Task<NuGetFeedEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<NuGetFeedEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a new feed.
        /// </summary>
        /// <param name="entity">The feed to add.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The feed, with server-allocated properties such as <see cref="Entity.Id"/> initialized.</returns>
        public async Task<NuGetFeedEntity> AddAsync(NuGetFeedEntity entity, CancellationToken cancellationToken = default)
        {
            return await GroupCreateAsync<NuGetFeedEntity, NuGetFeedEntity>(entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove an existing feed.
        /// </summary>
        /// <param name="entity">The feed to remove.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task RemoveAsync(NuGetFeedEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an existing feed.
        /// </summary>
        /// <param name="entity">The feed to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task UpdateAsync(NuGetFeedEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}