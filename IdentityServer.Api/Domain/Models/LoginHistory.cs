using LM.Domain.Entities;
using LM.Responses;
using LM.Responses.Extensions;

namespace IdentityServer.Api.Domain.Models
{
    public class LoginHistory : Entity
    {
        [Obsolete(ConstructorObsoleteMessage, true)]
        LoginHistory() { }
        public LoginHistory(Guid code, User user) : base(code)
        {
            UserId = user.Id;
            User = user;
        }

        public long UserId { get; private set; }

        public User User { get; private set; }

        public static Response<LoginHistory> Create(User user)
        {
            var response = Response<LoginHistory>.Create();

            if (user == null)
                return response.WithBusinessError(nameof(user), "Usuário não informado.");

            var loginHistory = new LoginHistory(Guid.NewGuid(), user);

            return response.SetValue(loginHistory);
        }

        public static implicit operator LoginHistory(Maybe<LoginHistory> entity) => entity.Value;

        public static implicit operator LoginHistory(Response<LoginHistory> entity) => entity.Data;
    }
}