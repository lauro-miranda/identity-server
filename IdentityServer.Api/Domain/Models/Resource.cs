using LM.Domain.Entities;
using LM.Responses;
using LM.Responses.Extensions;

namespace IdentityServer.Api.Domain.Models
{
    public class Resource : Entity
    {
        [Obsolete(ConstructorObsoleteMessage, true)]
        Resource() { }
        Resource(Guid code, string name, string displayName) : base(code)
        {
            Name = name;
            DisplayName = displayName;
        }

        public string Name { get; private set; }

        public string DisplayName { get; private set; }

        public ICollection<ScopeResource> Scopes { get; set; } = new HashSet<ScopeResource>();


        public static Response<Resource> Create(Guid code, string name, string displayName)
        {
            var response = Response<Resource>.Create();

            if (string.IsNullOrEmpty(name))
                response.WithBusinessError($"{nameof(name)} is required.");

            if (string.IsNullOrEmpty(displayName))
                response.WithBusinessError($"{nameof(displayName)} is required.");

            if (response.HasError)
                return response;

            return response.SetValue(new Resource(code, name, displayName));
        }
    }
}