using IdentityServer.Messages.Requests;
using LM.Domain.Entities;
using LM.Responses;
using LM.Responses.Extensions;

namespace IdentityServer.Api.Domain.Models
{
    public class Claim : Entity
    {
        [Obsolete(ConstructorObsoleteMessage, true)]
        Claim() { }
        Claim(Guid code, string type, string value) : base(code)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; internal set; }

        public string Value { get; internal set; }

        public ICollection<ClientClaim> Clients { get; private set; } = new HashSet<ClientClaim>();

        public ICollection<UserClaim> Users { get; set; } = new HashSet<UserClaim>();

        public static Response<Claim> Create(UserRequestMessage.ClaimRequestMessage message)
        {
            var response = Response<Claim>.Create();

            if (string.IsNullOrEmpty(message.Type))
                response.WithBusinessError($"{nameof(message.Type)} is required.");

            if (string.IsNullOrEmpty(message.Value))
                response.WithBusinessError($"{nameof(message.Value)} is required.");

            if (response.HasError)
                return response;

            return response.SetValue(new Claim(Guid.NewGuid(), message.Type, message.Value));
        }

        public static implicit operator Claim(Maybe<Claim> entity) => entity.Value;

        public static implicit operator Claim(Response<Claim> entity) => entity.Data;
    }
}