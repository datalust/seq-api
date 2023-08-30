﻿// Copyright © Datalust and contributors. 
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
using Seq.Api.Model.Diagnostics;
using Seq.Api.Model.Inputs;
using Seq.Api.Model.Users;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform actions on API keys. API keys can be used to authenticate and identify log event sources, and for
    /// users to delegate some or all permissions to a client of the Seq API (app or integration) without exposing
    /// user credentials.
    /// </summary>
    public class ApiKeysResourceGroup : ApiResourceGroup
    {
        internal ApiKeysResourceGroup(ILoadResourceGroup connection)
            : base("ApiKeys", connection)
        {
        }

        /// <summary>
        /// Retrieve the API key with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the API key.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The API key.</returns>
        public async Task<ApiKeyEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<ApiKeyEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve API keys.
        /// </summary>
        /// <param name="ownerId">If the id of a <see cref="UserEntity"/> is provided, only API keys owned by them will be included in the result; if
        /// not specified, personal API keys for all owners will be listed.</param>
        /// <param name="shared">If <c>true</c>, shared API keys will be included in the result.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing matching API keys.</returns>
        public async Task<List<ApiKeyEntity>> ListAsync(string ownerId = null, bool shared = false, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object> { { "ownerId", ownerId }, {"shared", shared} };
            return await GroupListAsync<ApiKeyEntity>("Items", parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Construct an API key with server defaults pre-initialized.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The unsaved API key.</returns>
        public async Task<ApiKeyEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<ApiKeyEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a new API key.
        /// </summary>
        /// <param name="entity">The API key to add.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The API key, with server-allocated properties, such as <see cref="ApiKeyEntity.Token"/> (if server-allocated),
        /// and <see cref="Entity.Id"/>, initialized.</returns>
        /// <remarks>Leaving the token blank will cause the server to generate a cryptographically random API key token. After creation, the first
        /// few characters of the token will be readable from <see cref="ApiKeyEntity.TokenPrefix"/>, but because only a cryptographically-secure
        /// hash of the token is stored internally, the token itself cannot be retrieved.</remarks>
        public async Task<ApiKeyEntity> AddAsync(ApiKeyEntity entity, CancellationToken cancellationToken = default)
        {
            return await GroupCreateAsync<ApiKeyEntity, ApiKeyEntity>(entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove an existing API key.
        /// </summary>
        /// <param name="entity">The API key to remove.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task RemoveAsync(ApiKeyEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an existing API key.
        /// </summary>
        /// <param name="entity">The API key to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        /// <remarks>The API key token itself cannot be updated using this method.</remarks>
        public async Task UpdateAsync(ApiKeyEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a detailed metric for the API key.
        /// </summary>
        /// <param name="entity">The API key to retrieve metrics for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <param name="measurement">The measurement to get.</param>
        /// <returns></returns>
        public async Task<MeasurementTimeseriesPart> GetMeasurementTimeseriesAsync(ApiKeyEntity entity, string measurement, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>{ ["id"] = entity.Id, ["measurement"] = measurement };
            return await GroupGetAsync<MeasurementTimeseriesPart>("Metric", parameters, cancellationToken);
        }

        /// <summary>
        /// Retrieve a detailed metric for all API keys.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <param name="measurement">The measurement to get.</param>
        /// <returns></returns>
        public async Task<Dictionary<string, MeasurementTimeseriesPart>> GetAllMeasurementTimeseriesAsync(string measurement, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>{ ["measurement"] = measurement };
            return await GroupGetAsync<Dictionary<string, MeasurementTimeseriesPart>>("Metrics", parameters, cancellationToken);
        }
    }
}
