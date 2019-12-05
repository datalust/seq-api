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
using Seq.Api.Model.SqlQueries;
using Seq.Api.Model.Users;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on saved SQL queries.
    /// </summary>
    public class SqlQueriesResourceGroup : ApiResourceGroup
    {
        internal SqlQueriesResourceGroup(ISeqConnection connection)
            : base("SqlQueries", connection)
        {
        }

        /// <summary>
        /// Retrieve the query with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the query.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The query.</returns>
        public async Task<SqlQueryEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<SqlQueryEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve queries.
        /// </summary>
        /// <param name="ownerId">If the id of a <see cref="UserEntity"/> is provided, only queries owned by them will be included in the result; if
        /// not specified, personal queries for all owners will be listed.</param>
        /// <param name="shared">If <c>true</c>, shared queries will be included in the result.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing matching queries.</returns>
        public async Task<List<SqlQueryEntity>> ListAsync(string ownerId = null, bool shared = false, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object> { { "ownerId", ownerId }, { "shared", shared } };
            return await GroupListAsync<SqlQueryEntity>("Items", parameters, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Construct a query with server defaults pre-initialized.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The unsaved query.</returns>
        public async Task<SqlQueryEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<SqlQueryEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a new query.
        /// </summary>
        /// <param name="entity">The query to add.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The query, with server-allocated properties such as <see cref="Entity.Id"/> initialized.</returns>
        public async Task<SqlQueryEntity> AddAsync(SqlQueryEntity entity, CancellationToken cancellationToken = default)
        {
            return await GroupCreateAsync<SqlQueryEntity, SqlQueryEntity>(entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove an existing query.
        /// </summary>
        /// <param name="entity">The query to remove.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task RemoveAsync(SqlQueryEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an existing query.
        /// </summary>
        /// <param name="entity">The query to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task UpdateAsync(SqlQueryEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
