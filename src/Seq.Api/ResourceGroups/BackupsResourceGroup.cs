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
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.Backups;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on backups. Seq backups include metadata like users, signals, API keys and other configuration, but do not include
    /// the event stream. Backups are fully encrypted with AES-256 and cannot be restored without the secret key from the originating Seq instance.
    /// </summary>
    public class BackupsResourceGroup : ApiResourceGroup
    {
        internal BackupsResourceGroup(ILoadResourceGroup connection)
            : base("Backups", connection)
        {
        }

        /// <summary>
        /// Retrieve the backup with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the backup.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The backup.</returns>
        public async Task<BackupEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<BackupEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve backups.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing matching backups.</returns>
        public async Task<List<BackupEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<BackupEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Download a backup with the current state of the server. Note that the backup will not be stored server-side.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The <c>.seqbac</c> backup file.</returns>
        public async Task<Stream> DownloadImmediateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupPostReadBytesAsync("Immediate", new object(), cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
