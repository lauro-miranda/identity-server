using IdentityServer.Api.Domain.Services.Contracts;
using IdentityServer.Messages.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Api.Controllers
{
    [Authorize]
    [ApiController, Route("api/[controller]")]
    public class UserController : Default.Controller
    {
        IUserService UserService { get; }

        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> CreateAsync([FromBody] UserRequestMessage message)
            => await WithResponseAsync(() => UserService.CreateAsync(message));
    }
}