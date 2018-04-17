using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<BackupEntity> FindAsync(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<BackupEntity>("Item", new Dictionary<string, object> { { "id", id } }).ConfigureAwait(false);
        }

        public async Task<List<BackupEntity>> ListAsync()
        {
            return await GroupListAsync<BackupEntity>("Items").ConfigureAwait(false);
        }

        public async Task<Stream> DownloadImmediateAsync()
        {
            return await GroupPostReadBytesAsync("Immediate", new object()).ConfigureAwait(false);
        }
    }
}
