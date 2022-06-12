using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Api.Controllers.Default
{
    [ApiController, Route("")]
    public class MeController : ControllerBase
    {
        [HttpGet, Route("")]
        public IActionResult Get() => Ok(new { name = "identity-server", version = "1.0.0" });
    }
}