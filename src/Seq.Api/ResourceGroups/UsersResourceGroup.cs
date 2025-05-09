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
using System.Threading.Tasks;
using Seq.Api.Model.Users;
using System.Linq;
using System.Threading;
using Seq.Api.Client;
using Seq.Api.Model;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on users.
    /// </summary>
    public class UsersResourceGroup : ApiResourceGroup
    {
        internal UsersResourceGroup(ILoadResourceGroup connection)
            : base("Users", connection)
        {
        }

        /// <summary>
        /// Retrieve the user with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The user.</returns>
        public async Task<UserEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<UserEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the current logged-in user.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The user, or <c>null</c> if no user is logged in.</returns>
        public async Task<UserEntity> FindCurrentAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<UserEntity>("Current", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve users.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing matching users.</returns>
        public async Task<List<UserEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<UserEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Construct a user with server defaults pre-initialized.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The unsaved user.</returns>
        public async Task<UserEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<UserEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a new user.
        /// </summary>
        /// <param name="entity">The user to add.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The user, with server-allocated properties such as <see cref="Entity.Id"/> initialized.</returns>
        public async Task<UserEntity> AddAsync(UserEntity entity, CancellationToken cancellationToken = default)
        {
            return await GroupCreateAsync<UserEntity, UserEntity>(entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove an existing user.
        /// </summary>
        /// <param name="entity">The user to remove.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task RemoveAsync(UserEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="entity">The user to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task UpdateAsync(UserEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Log in, using the supplied <paramref name="username"/> and <paramref name="password"/>. Only valid when
        /// Active Directory or username/password authentication is enabled.
        /// </summary>
        /// <param name="username">The username to log in with.</param>
        /// <param name="password">The password to log in with.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The logged-in user.</returns>
        /// <remarks>Following successful login, other calls through the API client will authenticate as the logged-in user.</remarks>
        public async Task<UserEntity> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            if (password == null) throw new ArgumentNullException(nameof(password));
            var credentials = new CredentialsPart {Username = username, Password = password};
            return await GroupPostAsync<CredentialsPart, UserEntity>("Login", credentials, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Log out the current user.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task LogoutAsync(CancellationToken cancellationToken = default)
        {
            await GroupPostAsync("Logout", new object(), cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the search history for the given user. Only allows the current user's search history to be retrieved.
        /// </summary>
        /// <param name="entity">The user to retrieve search history for; must be logged in or the owner of the authenticating API key.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The the user's search history.</returns>
        public async Task<SearchHistoryEntity> GetSearchHistoryAsync(UserEntity entity, CancellationToken cancellationToken = default)
        {
            return await Client.GetAsync<SearchHistoryEntity>(entity, "SearchHistory", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a user's search history by adding, removing, or modifying <paramref name="item"/>.
        /// </summary>
        /// <param name="entity">The search history for the user.</param>
        /// <param name="item">The item to modify.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task signalling completion.</returns>
        public async Task UpdateSearchHistoryAsync(SearchHistoryEntity entity, SearchHistoryItemPart item, CancellationToken cancellationToken = default)
        { 
            await Client.PostAsync(entity, "Update", item, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Log in, using integrated Windows authentication. Only available when Active Directory authentication is enabled.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The logged-in user.</returns>
        /// <remarks>Following successful login, other calls through the API client will authenticate as the logged-in user.</remarks>
        public async Task<UserEntity> LoginWindowsIntegratedAsync(CancellationToken cancellationToken = default)
        {
            var providers = await GroupGetAsync<AuthProvidersPart>("AuthenticationProviders", cancellationToken: cancellationToken).ConfigureAwait(false);
            var provider = providers.Providers.SingleOrDefault(p => p.Name == "Integrated Windows Authentication");
            if (provider == null)
                throw new SeqApiException("The Integrated Windows Authentication provider is not available.");
            var response = await Client.HttpClient.GetAsync(provider.Url, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await FindCurrentAsync(cancellationToken).ConfigureAwait(false);
        }
        
        /// <summary>Reset the authentication properties stored for <paramref name="entity"/>. After this operation
        /// completes, the user's <see cref="UserEntity.Username"/> will exclusively determine how they are linked
        /// to the authentication provider on their next login.</summary>
        /// <param name="entity">The user to modify.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task signalling completion.</returns>
        public async Task UnlinkAuthenticationProviderAsync(UserEntity entity, CancellationToken cancellationToken = default)
        { 
            await Client.PostAsync(entity, "UnlinkAuthenticationProvider", new {}, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
