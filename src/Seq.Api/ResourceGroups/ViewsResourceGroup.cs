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
using Seq.Api.Model.Metrics;
using Seq.Api.Model.Users;

namespace Seq.Api.ResourceGroups;

/// <summary>
/// Perform operations on metrics views. A view is a collection of filters, groupings, and pinned charts used to
/// populate the Seq Metrics screen.
/// </summary>
public class ViewsResourceGroup : ApiResourceGroup
{
    internal ViewsResourceGroup(ILoadResourceGroup connection)
        : base("Views", connection)
    {
    }

    /// <summary>
    /// Retrieve the view with the given id; throws if the entity does not exist.
    /// </summary>
    /// <param name="id">The id of the view.</param>
    /// <param name="partial">If <c>true</c>, include only partial details in the result.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
    /// <returns>The view.</returns>
    public async Task<ViewEntity> FindAsync(string id, bool partial = false, CancellationToken cancellationToken = default)
    {
        if (id == null) throw new ArgumentNullException(nameof(id));
        return await GroupGetAsync<ViewEntity>("Item", new Dictionary<string, object> { { "id", id }, { "partial", partial } }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve views.
    /// </summary>
    /// <param name="ownerId">If the id of a <see cref="UserEntity"/> is provided, only views owned by them will be included in the result; if
    /// not specified, personal views for all owners will be listed.</param>
    /// <param name="shared">If <c>true</c>, shared views will be included in the result.</param>
    /// <param name="partial">If <c>true</c>, include only partial details in the result.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
    /// <returns>A list containing matching views.</returns>
    public async Task<List<ViewEntity>> ListAsync(string ownerId = null, bool shared = false, bool partial = false, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<string, object> { { "ownerId", ownerId }, { "shared", shared }, { "partial", partial } };
        return await GroupListAsync<ViewEntity>("Items", parameters, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Construct a view with server defaults pre-initialized.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
    /// <returns>The unsaved view.</returns>
    public async Task<ViewEntity> TemplateAsync(CancellationToken cancellationToken = default)
    {
        return await GroupGetAsync<ViewEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Add a new view.
    /// </summary>
    /// <param name="entity">The view to add.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
    /// <returns>The view, with server-allocated properties such as <see cref="Entity.Id"/> initialized.</returns>
    public async Task<ViewEntity> AddAsync(ViewEntity entity, CancellationToken cancellationToken = default)
    {
        return await GroupCreateAsync<ViewEntity, ViewEntity>(entity, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
        
    /// <summary>
    /// Remove an existing view.
    /// </summary>
    /// <param name="entity">The view to remove.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
    /// <returns>A task indicating completion.</returns>
    public async Task RemoveAsync(ViewEntity entity, CancellationToken cancellationToken = default)
    {
        await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Update an existing view.
    /// </summary>
    /// <param name="entity">The view to update.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
    /// <returns>A task indicating completion.</returns>
    public async Task UpdateAsync(ViewEntity entity, CancellationToken cancellationToken = default)
    {
        await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}