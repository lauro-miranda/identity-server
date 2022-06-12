using IdentityServer.Api.Domain.Models;
using LM.Domain.Repositories;
using LM.Responses;

namespace IdentityServer.Api.Domain.Repositories.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> AnyAsync(Guid code);

        Task<Maybe<User>> FindByCredentialsAsync(string identification, string password);

        Task<Maybe<Client>> FindClientAsync(string clientId);

        Task<Maybe<Resource>> FindResourceAsync(string name);

        Task<List<Resource>> FindResourcesAsync(IEnumerable<string> scopeNames);

        Task<List<Resource>> GetAllResourcesAsync();
    }
}