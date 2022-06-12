using IdentityServer.Api.Domain.Models;
using IdentityServer.Api.Domain.Repositories.Contracts;
using IdentityServer.Api.Domain.Services.Contracts;
using IdentityServer.Messages.Requests;
using IdentityServer.Messages.Responses;
using LM.Domain.UnitOfWork;
using LM.Responses;
using LM.Responses.Extensions;

namespace IdentityServer.Api.Domain.Services
{
    public class UserService : IUserService
    {
        IUserRepository UserRepository { get; }

        IUnitOfWork Uow { get; }

        public UserService(IUserRepository userRepository
            , IUnitOfWork uow)
        {
            UserRepository = userRepository;
            Uow = uow;
        }

        public async Task<Response<UserResponseMessage>> CreateAsync(UserRequestMessage message)
        {
            var response = Response<UserResponseMessage>.Create();

            if (await UserRepository.AnyAsync(message.Code))
                return response.WithBusinessError(nameof(message.Code), "Já existe um usuário com esse código.");

            var user = User.Create(message);

            if (user.HasError)
                return response.WithMessages(user.Messages);

            await UserRepository.AddAsync(user);

            if (!await Uow.CommitAsync())
                return response.WithCriticalError("Falha ao tentar salvar o usuário.");

            return response;
        }

        public async Task RegisterLoginAsync(User user)
        {
            if (user == null)
                return;

            user.AddLogin();

            await UserRepository.UpdateAsync(user);

            await Uow.CommitAsync();
        }
    }
}