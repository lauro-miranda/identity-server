using IdentityModel.Client;
using IdentityServer.Api.Settings;
using IdentityServer.Messages;
using LM.Responses;
using LM.Responses.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace IdentityServer.Api.Controllers
{

    [ApiController, Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        HttpClient HttpClient { get; }

        IOptions<IdentityServerSettings> IdentityServerSettings { get; }

        string DefaultErrorMessage = "Falha ao tentar autenticar o usuário.";

        public AccountController(IHttpClientFactory factory
            , IOptions<IdentityServerSettings> identityServerSettings)
        {
            HttpClient = factory.CreateClient(identityServerSettings.Value.PollyConfigurationName);
            IdentityServerSettings = identityServerSettings;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestMessage message)
        {
            var response = Response<TokenResponse>.Create();

            var clientToken = await GetClientTokenAsync(message);

            if (!clientToken.HasValue)
                return StatusCode(500, response.WithCriticalError(DefaultErrorMessage));

            if (string.IsNullOrEmpty(clientToken.Value.AccessToken))
            {
                if (clientToken.Value.Error.Equals("unauthorized_client"))
                {
                    return StatusCode(403, response.WithBusinessError(nameof(message.Identification), ""));
                }
                else if (clientToken.Value.Error.Equals("invalid_client"))
                {
                    return Unauthorized(response.WithBusinessError(nameof(message.Identification), "Usuário e/ou senha inválido(s)."));
                }
            }

            return Ok(response.SetValue(clientToken.Value));
        }

        async Task<Maybe<TokenResponse>> GetClientTokenAsync(LoginRequestMessage message)
        {
            var disco = await HttpClient.GetDiscoveryDocumentAsync();

            try
            {
                var passwordToken = new PasswordTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = IdentityServerSettings.Value.PasswordToken.ClientId,
                    ClientSecret = IdentityServerSettings.Value.PasswordToken.ClientSecret,
                    Scope = IdentityServerSettings.Value.PasswordToken.Scope,
                    UserName = message.Identification,
                    Password = message.Password
                };

                return await HttpClient.RequestPasswordTokenAsync(passwordToken);
            }
            catch (Exception ex)
            {
                Log.Error(ex, DefaultErrorMessage);
                return Maybe<TokenResponse>.Create();
            }
        }
    }
}