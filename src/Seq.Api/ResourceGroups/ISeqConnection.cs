using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Client;
using Seq.Api.Model;

namespace Seq.Api.ResourceGroups
{
    interface ISeqConnection
    {
        Task<ResourceGroup> LoadResourceGroupAsync(string name, CancellationToken cancellationToken = default);
        SeqApiClient Client { get; }
    }
}
