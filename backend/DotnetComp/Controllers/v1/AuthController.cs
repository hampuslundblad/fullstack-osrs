using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace DotnetComp.Controllers.v1
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        [HttpGet("")]
        public ActionResult Root()
        {
            return Ok(HttpContext.User.Claims.Select(x => new { x.Type, x.Value }).ToList());
        }

        [HttpGet("github")]
        public ActionResult Login([FromQuery] string returnUrl)
        {
            return Challenge(
                new AuthenticationProperties() { RedirectUri = returnUrl },
                authenticationSchemes: ["github"]
            );
        }
    }
}
