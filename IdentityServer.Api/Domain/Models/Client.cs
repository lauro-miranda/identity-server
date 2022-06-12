using LM.Domain.Entities;
using LM.Responses;
using LM.Responses.Extensions;

namespace IdentityServer.Api.Domain.Models
{
    public class Client : Entity
    {
        [Obsolete(ConstructorObsoleteMessage, true)]
        Client() { }
        Client(Guid code, string clientId, string clientSecret) : base(code)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public string ClientId { get; private set; }

        public string ClientSecret { get; private set; }

        public ICollection<ClientScope> Scopes { get; private set; } = new HashSet<ClientScope>();

        public ICollection<ClientClaim> Claims { get; private set; } = new HashSet<ClientClaim>();


        public static Response<Client> Create(Guid code, string clientId, string clientSecret)
        {
            var response = Response<Client>.Create();

            if (string.IsNullOrEmpty(clientId))
                response.WithBusinessError($"{nameof(clientId)} is required.");

            if (string.IsNullOrEmpty(clientSecret))
                response.WithBusinessError($"{nameof(clientSecret)} is required.");

            if (response.HasError)
                return response;

            return response.SetValue(new Client(code, clientId, clientSecret));
        }
    }
}