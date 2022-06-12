using IdentityServer.Api.Domain.Models;
using IdentityServer.Api.Infra.Context.Extensions;
using LM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdentityServer.Api.Infra.Context
{
    public class IdentityServerContext : DbContext
    {
        public IdentityServerContext(DbContextOptions<IdentityServerContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(decimal)))
            {
                property.SetColumnType("decimal(18, 2)");
            }

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(Entity).IsAssignableFrom(type.ClrType) && (type.BaseType == null || !typeof(Entity).IsAssignableFrom(type.BaseType.ClrType)))
                    modelBuilder.SetSoftDeleteFilter(type.ClrType);
            }
        }

        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateSoftDeleteStatuses();

            return base.SaveChangesAsync(cancellationToken);
        }

        void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is not Entity entity)
                    continue;

                switch (entry.State)
                {
                    case EntityState.Deleted:
                        {
                            entity.Delete();
                            entry.State = EntityState.Modified;
                        }
                        break;
                }
            }
        }
    }
}