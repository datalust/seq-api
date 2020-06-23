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

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Client;
using Seq.Api.Model;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Base class for resource groups representing portions of the Seq API. These wrap an underlying
    /// <see cref="ResourceGroup"/> described in the API metadata at <c>/api/{resourceGroup}/resources</c>.
    /// </summary>
    [DebuggerDisplay("{" + nameof(_name) + "}")]
    public abstract class ApiResourceGroup
    {
        readonly string _name;
        readonly ISeqConnection _connection;

        internal ApiResourceGroup(string name, ISeqConnection connection)
        {
            _name = name;
            _connection = connection;
        }

        /// <summary>
        /// The API client used to access the API.
        /// </summary>
        protected SeqApiClient Client => _connection.Client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns></returns>
        protected Task<ResourceGroup> LoadGroupAsync(CancellationToken cancellationToken = default)
        {
            return _connection.LoadResourceGroupAsync(_name, cancellationToken);
        }

        /// <summary>
        /// Get an entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type being operated upon.</typeparam>
        /// <param name="link">The name of a link (or link template) included in the entity's link collection.</param>
        /// <param name="parameters">Parameters that will be substituted into the operation's link template.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The entity.</returns>
        protected async Task<TEntity> GroupGetAsync<TEntity>(string link, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var group = await LoadGroupAsync(cancellationToken).ConfigureAwait(false);
            return await Client.GetAsync<TEntity>(group, link, parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a linked resource as a string.
        /// </summary>
        /// <param name="link">The name of a link (or link template) included in the entity's link collection.</param>
        /// <param name="parameters">Parameters that will be substituted into the operation's link template.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The string.</returns>
        protected async Task<string> GroupGetStringAsync(string link, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var group = await LoadGroupAsync(cancellationToken).ConfigureAwait(false);
            return await Client.GetStringAsync(group, link, parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// List entities.
        /// </summary>
        /// <typeparam name="TEntity">The entity type being operated upon.</typeparam>
        /// <param name="link">The name of a link (or link template) included in the entity's link collection.</param>
        /// <param name="parameters">Parameters that will be substituted into the operation's link template.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The resulting entities.</returns>
        protected async Task<List<TEntity>> GroupListAsync<TEntity>(string link, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var group = await LoadGroupAsync(cancellationToken).ConfigureAwait(false);
            return await Client.ListAsync<TEntity>(group, link, parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Post/create an entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type being operated upon.</typeparam>
        /// <param name="link">The name of a link (or link template) included in the entity's link collection.</param>
        /// <param name="content">The content included in the operation.</param>
        /// <param name="parameters">Parameters that will be substituted into the operation's link template.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        protected async Task GroupPostAsync<TEntity>(string link, TEntity content, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var group = await LoadGroupAsync(cancellationToken).ConfigureAwait(false);
            await Client.PostAsync(group, link, content, parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Post/create an entity and read the response as a string.
        /// </summary>
        /// <typeparam name="TEntity">The entity type being operated upon.</typeparam>
        /// <param name="link">The name of a link (or link template) included in the entity's link collection.</param>
        /// <param name="content">The content included in the operation.</param>
        /// <param name="parameters">Parameters that will be substituted into the operation's link template.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The response string.</returns>
        protected async Task<string> GroupPostReadStringAsync<TEntity>(string link, TEntity content, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var group = await LoadGroupAsync(cancellationToken).ConfigureAwait(false);
            return await Client.PostReadStringAsync(group, link, content, parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Post/create an entity and read the response as a stream.
        /// </summary>
        /// <typeparam name="TEntity">The entity type being operated upon.</typeparam>
        /// <param name="link">The name of a link (or link template) included in the entity's link collection.</param>
        /// <param name="content">The content included in the operation.</param>
        /// <param name="parameters">Parameters that will be substituted into the operation's link template.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The response stream.</returns>
        protected async Task<Stream> GroupPostReadBytesAsync<TEntity>(string link, TEntity content, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var group = await LoadGroupAsync(cancellationToken).ConfigureAwait(false);
            return await Client.PostReadStreamAsync(group, link, content, parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Post/create an entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type being operated upon.</typeparam>
        /// <typeparam name="TResponse">The response type of the operation.</typeparam>
        /// <param name="link">The name of a link (or link template) included in the entity's link collection.</param>
        /// <param name="content">The content included in the operation.</param>
        /// <param name="parameters">Parameters that will be substituted into the operation's link template.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The response type.</returns>
        protected async Task<TResponse> GroupPostAsync<TEntity, TResponse>(string link, TEntity content, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var group = await LoadGroupAsync(cancellationToken).ConfigureAwait(false);
            return await Client.PostAsync<TEntity, TResponse>(group, link, content, parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type being operated upon.</typeparam>
        /// <param name="link">The name of a link (or link template) included in the entity's link collection.</param>
        /// <param name="content">The content included in the operation.</param>
        /// <param name="parameters">Parameters that will be substituted into the operation's link template.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        protected async Task GroupPutAsync<TEntity>(string link, TEntity content, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var group = await LoadGroupAsync(cancellationToken).ConfigureAwait(false);
            await Client.PutAsync(group, link, content, parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type being operated upon.</typeparam>
        /// <param name="link">The name of a link (or link template) included in the entity's link collection.</param>
        /// <param name="content">The content included in the operation.</param>
        /// <param name="parameters">Parameters that will be substituted into the operation's link template.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        protected async Task GroupDeleteAsync<TEntity>(string link, TEntity content, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var group = await LoadGroupAsync(cancellationToken).ConfigureAwait(false);
            await Client.DeleteAsync(group, link, content, parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type being operated upon.</typeparam>
        /// <typeparam name="TResponse">The response type of the operation.</typeparam>
        /// <param name="link">The name of a link (or link template) included in the entity's link collection.</param>
        /// <param name="content">The content included in the operation.</param>
        /// <param name="parameters">Parameters that will be substituted into the operation's link template.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The response type.</returns>
        protected async Task<TResponse> GroupDeleteAsync<TEntity, TResponse>(string link, TEntity content, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var group = await LoadGroupAsync(cancellationToken).ConfigureAwait(false);
            return await Client.DeleteAsync<TEntity, TResponse>(group, link, content, parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a URI from an entity's link collection.
        /// </summary>
        /// <typeparam name="TEntity">The entity type being operated upon.</typeparam>
        /// <param name="entity"></param>
        /// <param name="link">The name of a link (or link template) included in the entity's link collection.</param>
        /// <param name="orElse">A default URI to use when no link with the given name is present.</param>
        /// <returns>The link.</returns>
        protected string GetLink<TEntity>(TEntity entity, string link, string orElse) where TEntity : ILinked
        {
            return entity.Links.ContainsKey(link) ? link : orElse;
        }

        /// <summary>
        /// Create an entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type being operated upon.</typeparam>
        /// <typeparam name="TResponse">The response type of the operation.</typeparam>
        /// <param name="entity"></param>
        /// <param name="parameters">Parameters that will be substituted into the operation's link template.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The response.</returns>
        protected async Task<TResponse> GroupCreateAsync<TEntity, TResponse>(TEntity entity,
            IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
            where TEntity : ILinked
        {
            ILinked resource;
            string link;

            if (entity.Links.ContainsKey("Create"))
            {
                resource = entity;
                link = "Create";
            }
            else
            {
                resource = await LoadGroupAsync(cancellationToken).ConfigureAwait(false);
                link = "Items";
            }

            return await Client.PostAsync<TEntity, TResponse>(resource, link, entity, parameters, cancellationToken).ConfigureAwait(false);
        }
    }
}
