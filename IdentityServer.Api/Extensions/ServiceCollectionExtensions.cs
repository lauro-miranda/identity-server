using IdentityServer.Api.Configuration.Services;
using IdentityServer.Api.Configuration.Stories;
using IdentityServer.Api.Settings;

namespace IdentityServer.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureHttpClientPool(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            var identityServerSettings = configuration.GetSection(nameof(IdentityServerSettings)).Get<IdentityServerSettings>();

            services.AddHttpClient(identityServerSettings.PollyConfigurationName, client =>
            {
                client.BaseAddress = new Uri(identityServerSettings.Authority);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
        }

        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var identityServerSettings = configuration.GetSection(nameof(IdentityServerSettings)).Get<IdentityServerSettings>();

            services.AddAuthentication("Bearer").AddIdentityServerAuthentication(options =>
            {
                options.Authority = identityServerSettings.Authority;
                options.RequireHttpsMetadata = false;
                options.IntrospectionDiscoveryPolicy.RequireHttps = false;
                options.ApiName = identityServerSettings.PasswordToken.Scope;
            });
        }

        public static void ConfigureIdentityServer(this IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddResourceStore<ResourceStore>()
                .AddClientStore<ClientStore>()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddProfileService<ProfileService>();
        }
    }
}