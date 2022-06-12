using IdentityServer.Messages.Requests;
using IdentityServer4.Models;
using LM.Domain.Entities;
using LM.Responses;
using LM.Responses.Extensions;

namespace IdentityServer.Api.Domain.Models
{
    public class User : Entity
    {
        [Obsolete(ConstructorObsoleteMessage, true)]
        User() { }
        User(Guid code
            , string identification
            , string password)
            : base(code)
        {
            Identification = identification;
            Password = password;
            Active = true;
        }

        public string Identification { get; private set; }

        public string Password { get; private set; }

        public bool Active { get; private set; }

        public ICollection<UserClaim> Claims { get; set; } = new HashSet<UserClaim>();

        public ICollection<LoginHistory> History { get; set; } = new HashSet<LoginHistory>();

        internal void AddLogin()
        {
            var history = LoginHistory.Create(this);

            if (history.HasError) return;

            History.Add(history);
        }

        Response AddClaimWithDefault(UserRequestMessage message)
        {
            var response = Response.Create();

            var claims = message.Claims;

            claims.Add(new UserRequestMessage.ClaimRequestMessage { Type = nameof(Code), Value = $"{Code}" });
            claims.Add(new UserRequestMessage.ClaimRequestMessage { Type = nameof(Identification), Value = $"{Identification}" });

            claims.ForEach(claimMessage =>
            {
                var claim = Claim.Create(claimMessage);

                if (claim.HasError)
                    response.WithMessages(claim.Messages);
                else
                {
                    var userClaim = UserClaim.Create(this, claim);

                    if (userClaim.HasError)
                        response.WithMessages(claim.Messages);
                    else
                    {
                        Claims.Add(userClaim);
                    }
                }
            });

            return response;
        }

        public static Response<User> Create(UserRequestMessage message)
        {
            var response = Response<User>.Create();

            if (Guid.Empty.Equals(message.Code))
                response.WithBusinessError($"{nameof(message.Code)} is required.");

            if (string.IsNullOrEmpty(message.Identification))
                response.WithBusinessError($"{nameof(message.Identification)} is required.");

            if (string.IsNullOrEmpty(message.Password))
                response.WithBusinessError($"{nameof(message.Password)} is required.");

            if (response.HasError)
                return response;

            var user = new User(message.Code
                , message.Identification
                , message.Password.Sha256());

            if (response.WithMessages(user.AddClaimWithDefault(message).Messages).HasError)
                return response;

            return response.SetValue(user);
        }

        public static implicit operator User(Maybe<User> entity) => entity.Value;

        public static implicit operator User(Response<User> entity) => entity.Data;
    }
}