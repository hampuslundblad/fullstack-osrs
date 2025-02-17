using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace DotnetComp.Controllers.v1
{
    [ApiController]
    [Route("auth")]
    public class AuthController(ILogger<AuthController> logger) : ControllerBase
    {
        private readonly ILogger<AuthController> logger = logger;

        [HttpGet("")]
        public ActionResult Root()
        {
            return Ok(HttpContext.User.Claims.Select(x => new { x.Type, x.Value }).ToList());
        }

        [HttpGet("github")]
        public ActionResult Login([FromQuery] string returnUrl)
        {
            logger.LogInformation("Challenge with RedirectUri to {returnUrl}", returnUrl);
            return Challenge(
                new AuthenticationProperties() { RedirectUri = returnUrl },
                authenticationSchemes: ["github"]
            );
        }
    }
}
