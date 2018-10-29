using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.Diagnostics;

namespace Seq.Api.ResourceGroups
{
    public class DiagnosticsResourceGroup : ApiResourceGroup
    {
        internal DiagnosticsResourceGroup(ISeqConnection connection)
            : base("Diagnostics", connection)
        {
        }

        public async Task<ServerMetricsEntity> GetServerMetricsAsync(CancellationToken token = default)
        {
            return await GroupGetAsync<ServerMetricsEntity>("ServerMetrics", token: token).ConfigureAwait(false);
        }

        public async Task<ServerStatusPart> GetServerStatusAsync(CancellationToken token = default)
        {
            return await GroupGetAsync<ServerStatusPart>("ServerStatus", token: token).ConfigureAwait(false);
        }

        public async Task<string> GetIngestionLogAsync(CancellationToken token = default)
        {
            return await GroupGetStringAsync("IngestionLog", token: token).ConfigureAwait(false);
        }
    }
}
