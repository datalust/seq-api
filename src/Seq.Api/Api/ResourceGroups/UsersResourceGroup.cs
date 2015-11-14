using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Seq.Api.Model.Users;

namespace Seq.Api.ResourceGroups
{
    public class UsersResourceGroup : ApiResourceGroup
    {
        internal UsersResourceGroup(ISeqConnection connection)
            : base("Users", connection)
        {
        }

        public async Task<UserEntity> FindAsync(string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            return await GroupGetAsync<UserEntity>("Item", new Dictionary<string, object> { { "id", id } });
        }

        public async Task<UserEntity> FindCurrentAsync()
        {
            return await GroupGetAsync<UserEntity>("Current");
        }

        public async Task<List<UserEntity>> ListAsync()
        {
            return await GroupListAsync<UserEntity>("Items");
        }

        public async Task<UserEntity> TemplateAsync()
        {
            return await GroupGetAsync<UserEntity>("Template");
        }

        public async Task<UserEntity> AddAsync(UserEntity entity)
        {
            return await Client.PostAsync<UserEntity, UserEntity>(entity, "Create", entity);
        }

        public async Task RemoveAsync(UserEntity entity)
        {
            await Client.DeleteAsync(entity, "Self", entity);
        }

        public async Task UpdateAsync(UserEntity entity)
        {
            await Client.PutAsync(entity, "Self", entity);
        }

        public async Task<UserEntity> LoginAsync(string username, string password)
        {
            if (username == null) throw new ArgumentNullException("username");
            if (password == null) throw new ArgumentNullException("password");
            var credentials = new CredentialsPart {Username = username, Password = password};
            return await GroupPostAsync<CredentialsPart, UserEntity>("Login", credentials);
        }

        public async Task LogoutAsync()
        {
            await GroupPostAsync("Logout", new object());
        }
    }
}
