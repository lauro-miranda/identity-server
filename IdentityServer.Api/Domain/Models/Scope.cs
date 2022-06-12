using LM.Domain.Entities;
using LM.Responses;
using LM.Responses.Extensions;

namespace IdentityServer.Api.Domain.Models
{
    public class Scope : Entity
    {
        [Obsolete(ConstructorObsoleteMessage, true)]
        Scope() { }
        Scope(Guid code, string name) : base(code)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public ICollection<ClientScope> Clients { get; private set; } = new HashSet<ClientScope>();

        public ICollection<ScopeResource> Resources { get; set; } = new HashSet<ScopeResource>();

        public static Response<Scope> Create(Guid code, string name)
        {
            var response = Response<Scope>.Create();

            if (string.IsNullOrEmpty(name))
                response.WithBusinessError($"{nameof(name)} is required.");

            if (response.HasError)
                return response;

            return response.SetValue(new Scope(code, name));
        }
    }
}