using LM.Domain.Entities;
using LM.Responses;
using LM.Responses.Extensions;

namespace IdentityServer.Api.Domain.Models
{
    public class ClientScope : Entity
    {
        [Obsolete(ConstructorObsoleteMessage, true)]
        ClientScope() { }
        ClientScope(Guid code, Client client, Scope scope) : base(code)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            ClientId = client.Id;
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            ScopeId = scope.Id;
        }

        public long ClientId { get; private set; }

        public Client Client { get; private set; }

        public long ScopeId { get; private set; }

        public Scope Scope { get; private set; }

        public static Response<ClientScope> Create(Client client, Scope scope)
        {
            var response = Response<ClientScope>.Create();

            if (client == null)
                response.WithBusinessError($"{nameof(client)} is required.");

            if (scope == null)
                response.WithBusinessError($"{nameof(scope)} is required.");

            if (response.HasError)
                return response;

            return response.SetValue(new ClientScope(Guid.NewGuid(), client, scope));
        }
    }
}