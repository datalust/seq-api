using System.Collections.Generic;
using System.Threading.Tasks;
using Seq.Api.Model;

namespace Seq.Api.ResourceGroups
{
    public class EntityResourceGroup : ApiResourceGroup
    {
        internal EntityResourceGroup(string name, ISeqConnection connection) : base(name, connection)
        {
        }

        protected async Task<TResponse> GroupCreateAsync<TEntity, TResponse>(TEntity entity,
            IDictionary<string, object> parameters = null) where TEntity : ILinked
        {
            var link = entity.Links.ContainsKey("Create") ? "Create" : "Items";
            var group = await LoadGroupAsync().ConfigureAwait(false);
            return await Client.PostAsync<TEntity, TResponse>(group, link, entity, parameters).ConfigureAwait(false);
        }
    }
}