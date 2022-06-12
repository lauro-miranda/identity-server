using IdentityServer.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Api.Infra.Mappings
{
    public class LoginHistoryMapping : IEntityTypeConfiguration<LoginHistory>
    {
        public void Configure(EntityTypeBuilder<LoginHistory> builder)
        {
            builder.ToTable(nameof(LoginHistory));
            builder.HasOne(x => x.User).WithMany(x => x.History).HasForeignKey(x => x.UserId);
        }
    }
}