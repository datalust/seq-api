using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Seq.Api.Model.Users;
using System.Linq;
using Seq.Api.Client;

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
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<UserEntity>("Item", new Dictionary<string, object> { { "id", id } }).ConfigureAwait(false);
        }

        public async Task<UserEntity> FindCurrentAsync()
        {
            return await GroupGetAsync<UserEntity>("Current").ConfigureAwait(false);
        }

        public async Task<List<UserEntity>> ListAsync()
        {
            return await GroupListAsync<UserEntity>("Items").ConfigureAwait(false);
        }

        public async Task<UserEntity> TemplateAsync()
        {
            return await GroupGetAsync<UserEntity>("Template").ConfigureAwait(false);
        }

        public async Task<UserEntity> AddAsync(UserEntity entity)
        {
            return await Client.PostAsync<UserEntity, UserEntity>(entity, "Create", entity).ConfigureAwait(false);
        }

        public async Task RemoveAsync(UserEntity entity)
        {
            await Client.DeleteAsync(entity, "Self", entity).ConfigureAwait(false);
        }

        public async Task UpdateAsync(UserEntity entity)
        {
            await Client.PutAsync(entity, "Self", entity).ConfigureAwait(false);
        }

        public async Task<UserEntity> LoginAsync(string username, string password)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            if (password == null) throw new ArgumentNullException(nameof(password));
            var credentials = new CredentialsPart {Username = username, Password = password};
            return await GroupPostAsync<CredentialsPart, UserEntity>("Login", credentials).ConfigureAwait(false);
        }

        public async Task LogoutAsync()
        {
            await GroupPostAsync("Logout", new object()).ConfigureAwait(false);
        }

        public async Task<SearchHistoryEntity> GetSearchHistoryAsync(UserEntity entity)
        {
            return await Client.GetAsync<SearchHistoryEntity>(entity, "SearchHistory").ConfigureAwait(false);
        }

        public async Task<UserEntity> LoginWindowsIntegratedAsync()
        {
            var providers = await GroupGetAsync<AuthProvidersPart>("AuthenticationProviders").ConfigureAwait(false);
            var provider = providers.Providers.SingleOrDefault(p => p.Name == "Integrated Windows Authentication");
            if (provider == null)
                throw new SeqApiException("The Integrated Windows Authentication provider is not available.");
            var response = await Client.HttpClient.GetAsync(provider.Url).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await FindCurrentAsync().ConfigureAwait(false);
        }
    }
}
