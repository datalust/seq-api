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
            ILinked resource;
            string link;

            if (entity.Links.ContainsKey("Create"))
            {
                resource = entity;
                link = "Create";
            }
            else
            {
                resource = await LoadGroupAsync().ConfigureAwait(false);
                link = "Items";
            }

            return await Client.PostAsync<TEntity, TResponse>(resource, link, entity, parameters).ConfigureAwait(false);
        }
    }
}
