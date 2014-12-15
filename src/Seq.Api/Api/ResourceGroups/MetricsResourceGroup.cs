using System.Threading.Tasks;
using Seq.Api.Model.Metrics;

namespace Seq.Api.ResourceGroups
{
    public class MetricsResourceGroup : ApiResourceGroup
    {
        internal MetricsResourceGroup(ISeqConnection connection)
            : base("Metrics", connection)
        {
        }

        public async Task<MetricsEntity> FindCurrentAsync()
        {
            return await GroupGetAsync<MetricsEntity>("Current");
        }
    }
}
