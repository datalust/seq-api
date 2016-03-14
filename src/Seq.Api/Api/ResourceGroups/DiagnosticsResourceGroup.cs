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

        public async Task<ServerMetricsEntity> GetServerMetricsAsync()
        {
            return await GroupGetAsync<ServerMetricsEntity>("ServerMetrics").ConfigureAwait(false);
        }

        public async Task<ServerStatusPart> GetServerStatusAsync()
        {
            return await GroupGetAsync<ServerStatusPart>("ServerStatus").ConfigureAwait(false);
        }

        public async Task<string> GetIngestionLogAsync()
        {
            return await GroupGetStringAsync("IngestionLog").ConfigureAwait(false);
        }
    }
}
