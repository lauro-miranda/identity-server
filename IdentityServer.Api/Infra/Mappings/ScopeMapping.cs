using IdentityServer.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Api.Infra.Mappings
{
    public class ScopeMapping : IEntityTypeConfiguration<Scope>
    {
        public void Configure(EntityTypeBuilder<Scope> builder)
        {
            builder.ToTable(nameof(Scope));
            builder.HasKey(x => x.Id);
        }
    }
}
