using LM.Domain.Entities;
using LM.Responses;
using LM.Responses.Extensions;

namespace IdentityServer.Api.Domain.Models
{
    public class ScopeResource : Entity
    {
        [Obsolete(ConstructorObsoleteMessage, true)]
        ScopeResource() { }
        ScopeResource(Guid code, Scope scope, Resource resource) : base(code)
        {
            Scope = scope;
            ScopeId = scope.Id;
            Resource = resource;
            ResourceId = resource.Id;
        }

        public long ScopeId { get; private set; }

        public Scope Scope { get; private set; }

        public long ResourceId { get; private set; }

        public Resource Resource { get; private set; }

        public static Response<ScopeResource> Create(Scope scope, Resource resource)
        {
            var response = Response<ScopeResource>.Create();

            if (scope == null)
                response.WithBusinessError($"{nameof(scope)} is required.");

            if (resource == null)
                response.WithBusinessError($"{nameof(resource)} is required.");

            if (response.HasError)
                return response;

            return response.SetValue(new ScopeResource(Guid.NewGuid(), scope, resource));
        }
    }
}