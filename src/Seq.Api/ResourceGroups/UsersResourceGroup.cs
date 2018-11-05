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

        public async Task<UserEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<UserEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<UserEntity> FindCurrentAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<UserEntity>("Current", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<UserEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<UserEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<UserEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<UserEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<UserEntity> AddAsync(UserEntity entity, CancellationToken cancellationToken = default)
        {
            return await Client.PostAsync<UserEntity, UserEntity>(entity, "Create", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task RemoveAsync(UserEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(UserEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<UserEntity> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            if (password == null) throw new ArgumentNullException(nameof(password));
            var credentials = new CredentialsPart {Username = username, Password = password};
            return await GroupPostAsync<CredentialsPart, UserEntity>("Login", credentials, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task LogoutAsync(CancellationToken cancellationToken = default)
        {
            await GroupPostAsync("Logout", new object(), cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<SearchHistoryEntity> GetSearchHistoryAsync(UserEntity entity, CancellationToken cancellationToken = default)
        {
            return await Client.GetAsync<SearchHistoryEntity>(entity, "SearchHistory", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<UserEntity> LoginWindowsIntegratedAsync(CancellationToken cancellationToken = default)
        {
            var providers = await GroupGetAsync<AuthProvidersPart>("AuthenticationProviders", cancellationToken: cancellationToken).ConfigureAwait(false);
            var provider = providers.Providers.SingleOrDefault(p => p.Name == "Integrated Windows Authentication");
            if (provider == null)
                throw new SeqApiException("The Integrated Windows Authentication provider is not available.", null);
            var response = await Client.HttpClient.GetAsync(provider.Url, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await FindCurrentAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
