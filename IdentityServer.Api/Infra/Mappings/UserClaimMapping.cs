using IdentityServer.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Api.Infra.Mappings
{
    public class UserClaimMapping : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.ToTable(nameof(UserClaim));
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany(x => x.Claims).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Claim).WithMany(x => x.Users).HasForeignKey(x => x.ClaimId);
        }
    }
}