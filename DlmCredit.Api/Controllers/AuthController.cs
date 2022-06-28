using Microsoft.AspNetCore.Mvc;
using DlmCredit.Infrastructure.OAuth;
using Microsoft.AspNetCore.Authorization;
using DlmCredit.Infrastructure.Services;

namespace DlmCredit.Api.Controllers
{
    /// <summary>
    /// OAuth concersn such as geberating aut token, refresh revoking and token validation
    /// </summary>
    [ApiController]
    public partial class AuthController : ControllerBase
    {

        private readonly IJwtTokenHandler tokenHandler;
        private readonly IUserService _userService;
        public AuthController(IJwtTokenHandler tokenHandler, IUserService userService)
        {
            this.tokenHandler = tokenHandler;
            _userService = userService;
        }
        [HttpPost("~/oauth/token")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.GetUserDetails(request.UserName);
            if (user == null) return Unauthorized();

            var token = tokenHandler.GenerateToken(user.UserName, user);
            return Ok(token);
        }
    }
}
