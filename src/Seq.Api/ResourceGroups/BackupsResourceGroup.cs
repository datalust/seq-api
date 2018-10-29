using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.Backups;

namespace Seq.Api.ResourceGroups
{
    public class BackupsResourceGroup : ApiResourceGroup
    {
        internal BackupsResourceGroup(ISeqConnection connection)
            : base("Backups", connection)
        {
        }

        public async Task<BackupEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<BackupEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<BackupEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<BackupEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<Stream> DownloadImmediateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupPostReadBytesAsync("Immediate", new object(), cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
