using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Api.DTOs;
using User.Api.Services;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _authService.LoginAsync(model);
            if (result == null) return Unauthorized();

            return Ok(result);

        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshRequest request)
        {
            var result = await _authService.RefreshTokenAsync(request);
            if (result == null) return Unauthorized();
            return Ok(result);


        }

    }

}
