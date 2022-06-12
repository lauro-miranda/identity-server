namespace IdentityServer.Api.Settings
{
    public class IdentityServerSettings
    {
        public string PollyConfigurationName { get; set; }

        public string Authority { get; set; }

        public PasswordTokenSettings PasswordToken { get; set; }

        public class PasswordTokenSettings
        {
            public string ClientId { get; set; }

            public string ClientSecret { get; set; }

            public string Scope { get; set; }
        }
    }
}