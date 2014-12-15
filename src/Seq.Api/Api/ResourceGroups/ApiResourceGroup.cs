using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Seq.Api.Client;
using Seq.Api.Model;

namespace Seq.Api.ResourceGroups
{
    [DebuggerDisplay("{_name}")]
    public class ApiResourceGroup
    {
        readonly string _name;
        readonly ISeqConnection _connection;

        internal ApiResourceGroup(string name, ISeqConnection connection)
        {
            _name = name;
            _connection = connection;
        }

        protected SeqApiClient Client { get { return _connection.Client; } }

        protected Task<ResourceGroup> LoadGroupAsync()
        {
            return _connection.LoadResourceGroupAsync(_name);
        }

        protected async Task<TEntity> GroupGetAsync<TEntity>(string link, IDictionary<string, object> parameters = null)
        {
            var group = await LoadGroupAsync();
            return await Client.GetAsync<TEntity>(group, link, parameters);
        }

        protected async Task<List<TEntity>> GroupListAsync<TEntity>(string link, IDictionary<string, object> parameters = null)
        {
            var group = await LoadGroupAsync();
            return await Client.ListAsync<TEntity>(group, link, parameters);
        }

        protected async Task GroupPostAsync<TEntity>(string link, TEntity content, IDictionary<string, object> parameters = null)
        {
            var group = await LoadGroupAsync();
            await Client.PostAsync(group, link, content, parameters);
        }

        protected async Task<TResponse> GroupPostAsync<TEntity, TResponse>(string link, TEntity content, IDictionary<string, object> parameters = null)
        {
            var group = await LoadGroupAsync();
            return await Client.PostAsync<TEntity, TResponse>(group, link, content, parameters);
        }

        protected async Task GroupPutAsync<TEntity>(string link, TEntity content, IDictionary<string, object> parameters = null)
        {
            var group = await LoadGroupAsync();
            await Client.PutAsync(group, link, content, parameters);
        }

        protected async Task GroupDeleteAsync<TEntity>(string link, TEntity content, IDictionary<string, object> parameters = null)
        {
            var group = await LoadGroupAsync();
            await Client.DeleteAsync(group, link, content, parameters);
        }
    }
}
