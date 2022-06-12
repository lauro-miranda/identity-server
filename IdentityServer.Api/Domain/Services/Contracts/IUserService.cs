using IdentityServer.Api.Domain.Models;
using IdentityServer.Messages.Requests;
using IdentityServer.Messages.Responses;
using LM.Responses;

namespace IdentityServer.Api.Domain.Services.Contracts
{
    public interface IUserService
    {
        Task<Response<UserResponseMessage>> CreateAsync(UserRequestMessage message);

        Task RegisterLoginAsync(User user);
    }
}