using IdentityServer.Api.Domain.Repositories.Contracts;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace IdentityServer.Api.Configuration.Stories
{
    public class ResourceStore : IResourceStore
    {
        IUserRepository UserRepository { get; }

        public ResourceStore(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var resource = await UserRepository.FindResourceAsync(name);

            if (!resource.HasValue)
                return new ApiResource();

            return new ApiResource(resource.Value.Name, resource.Value.DisplayName);
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var resources = await UserRepository.FindResourcesAsync(scopeNames);

            if (!resources.Any())
                return new List<ApiResource>();

            return resources.Select(r => new ApiResource(r.Name, r.DisplayName));
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.Run<IEnumerable<IdentityResource>>(() => new List<IdentityResource>());
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            var resources = await UserRepository.GetAllResourcesAsync();

            return new Resources(new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            },
            resources.Select(r => new ApiResource(r.Name, r.DisplayName)));
        }
    }
}