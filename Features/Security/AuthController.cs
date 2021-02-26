using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using ShopsRUs.Service.Features.Security.Models;
using ShopsRUs.Service.Features.Security.Services;
using ShopsRUs.Service.Features.Customer;
using ShopsRUs.Service.Features.Customer.Services;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ShopsRUs.Service.Features.Security
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        public AuthController(IUserService userService, ITokenService tokenService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost(Name = nameof(Login))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [OpenApiTag("ShopsRUs API")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userData = await _userService.FindUserByEmailAsync(loginRequest.Email);

            if (userData == null) return NotFound();

            if (await _authService.ValidateCredentials(loginRequest.Email, loginRequest.Password))
            {
                var account = userData.ToDbUser(!userData.Blocked);
                if (account.Enabled)
                {
                    var token = _tokenService.CreateToken(account);
                    return Ok(new LoginResponse { Token = token, Account = account });
                }
                return Unauthorized("Account is Locked!");
            }
            return Unauthorized("Invalid Login Credentials");
        }
    }
}
