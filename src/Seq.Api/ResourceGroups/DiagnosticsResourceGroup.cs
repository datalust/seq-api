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

        public async Task<ServerMetricsEntity> GetServerMetricsAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<ServerMetricsEntity>("ServerMetrics", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<ServerStatusPart> GetServerStatusAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<ServerStatusPart>("ServerStatus", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<string> GetIngestionLogAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetStringAsync("IngestionLog", cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
