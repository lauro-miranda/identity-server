using IdentityServer.Api.Domain.Repositories.Contracts;
using IdentityServer.Api.Domain.Services.Contracts;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Secutiry = System.Security.Claims;

namespace IdentityServer.Api.Configuration.Services
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        IUserRepository UserRepository { get; }

        IUserService UserService { get; }

        public ResourceOwnerPasswordValidator(IUserRepository userRepository, IUserService userService)
        {
            UserService = userService;
            UserRepository = userRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                await AuthenticateAsync(context);
            }
            catch
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.UnsupportedGrantType, "Falha ao tentar autenticar o usuário.");
            }
        }

        async Task AuthenticateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await UserRepository.FindByCredentialsAsync(context.UserName, context.Password.Sha256());

            if (user.HasValue && user.Value.Active)
            {
                context.Result = new GrantValidationResult(user.Value.Id.ToString()
                    , "custom"
                    , user.Value.Claims.Select(c => c.Claim).Select(c => new Secutiry.Claim(c.Type, c.Value)));

                await UserService.RegisterLoginAsync(user);
            }
            else if (user.HasValue && !user.Value.Active)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient, "Usuário inativo.");
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient);
            }
        }
    }
}