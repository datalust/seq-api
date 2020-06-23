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
using Seq.Api.Model.Apps;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on Seq apps. Seq apps are executable plug-ins that read from and write to the Seq event stream.
    /// </summary>
    public class AppsResourceGroup : ApiResourceGroup
    {
        internal AppsResourceGroup(ISeqConnection connection)
            : base("Apps", connection)
        {
        }

        /// <summary>
        /// Retrieve the app with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the app.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The app.</returns>
        public async Task<AppEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<AppEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve apps.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing matching apps.</returns>
        public async Task<List<AppEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<AppEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Construct an app with server defaults pre-initialized.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The unsaved app.</returns>
        public async Task<AppEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<AppEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a new app.
        /// </summary>
        /// <param name="entity">The app to add.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The app, with server-allocated properties such as <see cref="Entity.Id"/> initialized.</returns>
        public async Task<AppEntity> AddAsync(AppEntity entity, CancellationToken cancellationToken = default)
        {
            return await GroupCreateAsync<AppEntity, AppEntity>(entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove an existing app.
        /// </summary>
        /// <param name="entity">The app to remove.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task RemoveAsync(AppEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an existing app.
        /// </summary>
        /// <param name="entity">The app to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task UpdateAsync(AppEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a new app by installing a NuGet package.
        /// </summary>
        /// <param name="feedId">The feed from which to retrieve the package.</param>
        /// <param name="packageId">The package id, for example <c>Seq.App.JsonArchive</c>.</param>
        /// <param name="version">The version of the package to install. If <c>null</c>, the latest version of the package will be installed.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The resulting app.</returns>
        public async Task<AppEntity> InstallPackageAsync(string feedId, string packageId, string version = null, CancellationToken cancellationToken = default)
        {
            if (feedId == null) throw new ArgumentNullException(nameof(feedId));
            if (packageId == null) throw new ArgumentNullException(nameof(packageId));
            var parameters = new Dictionary<string, object> { { "feedId", feedId }, { "packageId", packageId } };
            if (version != null) parameters.Add("version", version);
            return await GroupPostAsync<object, AppEntity>("InstallPackage", new object(), parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update the underlying package for app <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">The app to update the package for.</param>
        /// <param name="version">The version to update to; if <c>null</c>, the latest available version in the feed will be used.</param>
        /// <param name="force">If <c>true</c>, update the app package even if the same version is already installed.</param>
        /// <returns>The app with updated package information.</returns>
        public async Task<AppEntity> UpdatePackageAsync(AppEntity entity, string version = null, bool force = false)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var parameters = new Dictionary<string, object>();
            if (force) parameters.Add("force", true);
            if (version != null) parameters.Add("version", version);
            return await Client.PostAsync<object, AppEntity>(entity, "UpdatePackage", new object(), parameters).ConfigureAwait(false);
        }
    }
}