using System.Collections.Generic;
using System.Threading.Tasks;
using Seq.Api.Model.Updates;

namespace Seq.Api.ResourceGroups
{
    public class UpdatesResourceGroup : ApiResourceGroup
    {
        internal UpdatesResourceGroup(ISeqConnection connection)
            : base("Updates", connection)
        {
        }

        public async Task<List<AvailableUpdateEntity>> ListAsync()
        {
            return await GroupListAsync<AvailableUpdateEntity>("Items");
        }
    }
}
