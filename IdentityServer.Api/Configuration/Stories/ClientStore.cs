using IdentityServer.Api.Domain.Repositories.Contracts;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace IdentityServer.Api.Configuration.Stories
{
    public class ClientStore : IClientStore
    {
        IUserRepository UserRepository { get; }

        public ClientStore(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var clientResponse = await UserRepository.FindClientAsync(clientId);

            if (!clientResponse.HasValue)
                return new Client();

            var client = clientResponse.Value;

            return new Client
            {
                ClientId = client.ClientId,
                AccessTokenLifetime = 2592000,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                ClientSecrets = { new Secret(client.ClientSecret.Sha256()) },
                AllowOfflineAccess = true,
                RefreshTokenExpiration = TokenExpiration.Absolute,
                AllowedScopes = client.Scopes.Select(s => s.Scope).Select(s => s.Name).ToList(),
                Claims = client.Claims.Select(c => c.Claim).Select(c => new System.Security.Claims.Claim(c.Type, c.Value)).ToList(),
                ClientClaimsPrefix = ""
            };
        }
    }
}