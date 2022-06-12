namespace IdentityServer.Messages
{
    public class LoginRequestMessage
    {
        public string Identification { get; set; } = "";

        public string Password { get; set; } = "";
    }
}