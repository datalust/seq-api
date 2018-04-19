using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Seq.Api.Model.Users;
using System.Linq;
using System.Threading;
using Seq.Api.Client;

namespace Seq.Api.ResourceGroups
{
    public class UsersResourceGroup : ApiResourceGroup
    {
        internal UsersResourceGroup(ISeqConnection connection)
            : base("Users", connection)
        {
        }

        public async Task<UserEntity> FindAsync(string id, CancellationToken token = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<UserEntity>("Item", new Dictionary<string, object> { { "id", id } }, token).ConfigureAwait(false);
        }

        public async Task<UserEntity> FindCurrentAsync(CancellationToken token = default)
        {
            return await GroupGetAsync<UserEntity>("Current", token: token).ConfigureAwait(false);
        }

        public async Task<List<UserEntity>> ListAsync(CancellationToken token = default)
        {
            return await GroupListAsync<UserEntity>("Items", token: token).ConfigureAwait(false);
        }

        public async Task<UserEntity> TemplateAsync(CancellationToken token = default)
        {
            return await GroupGetAsync<UserEntity>("Template", token: token).ConfigureAwait(false);
        }

        public async Task<UserEntity> AddAsync(UserEntity entity, CancellationToken token = default)
        {
            return await Client.PostAsync<UserEntity, UserEntity>(entity, "Create", entity, token: token).ConfigureAwait(false);
        }

        public async Task RemoveAsync(UserEntity entity, CancellationToken token = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, token: token).ConfigureAwait(false);
        }

        public async Task UpdateAsync(UserEntity entity, CancellationToken token = default)
        {
            await Client.PutAsync(entity, "Self", entity, token: token).ConfigureAwait(false);
        }

        public async Task<UserEntity> LoginAsync(string username, string password, CancellationToken token = default)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            if (password == null) throw new ArgumentNullException(nameof(password));
            var credentials = new CredentialsPart {Username = username, Password = password};
            return await GroupPostAsync<CredentialsPart, UserEntity>("Login", credentials, token: token).ConfigureAwait(false);
        }

        public async Task LogoutAsync(CancellationToken token = default)
        {
            await GroupPostAsync("Logout", new object(), token: token).ConfigureAwait(false);
        }

        public async Task<SearchHistoryEntity> GetSearchHistoryAsync(UserEntity entity, CancellationToken token = default)
        {
            return await Client.GetAsync<SearchHistoryEntity>(entity, "SearchHistory", token: token).ConfigureAwait(false);
        }

        public async Task<UserEntity> LoginWindowsIntegratedAsync(CancellationToken token = default)
        {
            var providers = await GroupGetAsync<AuthProvidersPart>("AuthenticationProviders", token: token).ConfigureAwait(false);
            var provider = providers.Providers.SingleOrDefault(p => p.Name == "Integrated Windows Authentication");
            if (provider == null)
                throw new SeqApiException("The Integrated Windows Authentication provider is not available.", null);
            var response = await Client.HttpClient.GetAsync(provider.Url, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await FindCurrentAsync(token).ConfigureAwait(false);
        }
    }
}
