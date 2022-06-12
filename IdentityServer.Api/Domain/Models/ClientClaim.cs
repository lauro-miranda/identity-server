using LM.Domain.Entities;
using LM.Responses;
using LM.Responses.Extensions;

namespace IdentityServer.Api.Domain.Models
{
    public class ClientClaim : Entity
    {
        [Obsolete(ConstructorObsoleteMessage, true)]
        ClientClaim() { }
        ClientClaim(Guid code, Client client, Claim claim) : base(code)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            ClientId = client.Id;
            Claim = claim ?? throw new ArgumentNullException(nameof(claim));
            ClaimId = claim.Id;
        }

        public long ClientId { get; private set; }

        public Client Client { get; private set; }

        public long ClaimId { get; private set; }

        public Claim Claim { get; private set; }

        public static Response<ClientClaim> Create(Client client, Claim claim)
        {
            var response = Response<ClientClaim>.Create();

            if (client == null)
                response.WithBusinessError($"{nameof(client)} is required.");

            if (claim == null)
                response.WithBusinessError($"{nameof(claim)} is required.");

            if (response.HasError)
                return response;

            return response.SetValue(new ClientClaim(Guid.NewGuid(), client, claim));
        }
    }
}