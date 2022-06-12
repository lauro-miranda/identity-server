using LM.Domain.Entities;
using LM.Responses;
using LM.Responses.Extensions;

namespace IdentityServer.Api.Domain.Models
{
    public class UserClaim : Entity
    {
        [Obsolete(ConstructorObsoleteMessage, true)]
        UserClaim() { }
        UserClaim(Guid code, User user, Claim claim) : base(code)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            UserId = user.Id;
            Claim = claim ?? throw new ArgumentNullException(nameof(claim));
            ClaimId = claim.Id;
        }

        public long UserId { get; private set; }

        public User User { get; private set; }

        public long ClaimId { get; private set; }

        public Claim Claim { get; private set; }

        public static Response<UserClaim> Create(User user, Claim claim)
        {
            var response = Response<UserClaim>.Create();

            if (user == null)
                response.WithBusinessError($"{nameof(user)} is required.");

            if (claim == null)
                response.WithBusinessError($"{nameof(claim)} is required.");

            if (response.HasError)
                return response;

            return response.SetValue(new UserClaim(Guid.NewGuid(), user, claim));
        }

        public static implicit operator UserClaim(Maybe<UserClaim> entity) => entity.Value;

        public static implicit operator UserClaim(Response<UserClaim> entity) => entity.Data;
    }
}