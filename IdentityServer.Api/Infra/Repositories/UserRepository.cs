using IdentityServer.Api.Domain.Models;
using IdentityServer.Api.Domain.Repositories.Contracts;
using IdentityServer.Api.Infra.Context;
using LM.Infra.Repositories;
using LM.Responses;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Api.Infra.Repositories
{
    public class UserRepository : Repository<User, IdentityServerContext>, IUserRepository
    {
        public UserRepository(IdentityServerContext context)
            : base(context)
        { }

        public async Task<bool> AnyAsync(Guid code)
            => await DbSet.AnyAsync(x => x.Code == code);

        public async Task<Maybe<User>> FindByCredentialsAsync(string identification, string password)
            => await DbSet.Include(u => u.Claims)
                    .ThenInclude(c => c.Claim)
                .FirstOrDefaultAsync(x => !string.IsNullOrEmpty(x.Identification)
                && x.Identification.Equals(identification)
                && !string.IsNullOrEmpty(x.Password) && x.Password.Equals(password));
        public async Task<Maybe<Client>> FindClientAsync(string clientId)
            => await Context.Clients
                .Include(x => x.Claims)
                    .ThenInclude(x => x.Claim)
                .Include(x => x.Scopes)
                    .ThenInclude(x => x.Scope)
            .FirstOrDefaultAsync(c => c.ClientId.Equals(clientId));

        public async Task<Maybe<Resource>> FindResourceAsync(string name)
            => await Context.Resources.FirstOrDefaultAsync(r => r.Name.Equals(name));

        public async Task<List<Resource>> FindResourcesAsync(IEnumerable<string> scopeNames)
            => await Context.Resources.Where(r => r.Scopes.Any(s => scopeNames.Contains(s.Scope.Name))).ToListAsync();

        public async Task<List<Resource>> GetAllResourcesAsync()
            => await Context.Resources.ToListAsync();
    }
}