using JamOrder.Models;
using JamOrder.Services.Interfaces;
using JamOrder.Validators;
using Microsoft.AspNetCore.Mvc;

namespace JamOrder.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public UserController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            UserValidator userValidator = new();
            var result = userValidator.Validate(user);
            if(!result.IsValid) 
            {
                return BadRequest(result.ToString());
            }
            _userService.RegisterUser(user);
            return NoContent();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredentials userCredentials)
        {
            UserCredentialsValidator userValidator = new();
            var result = userValidator.Validate(userCredentials);
            if (!result.IsValid)
            {
                return BadRequest(result.ToString());
            }
            var existingUser = _userService.GetUser(userCredentials);

            if (existingUser == null)
            {
                return Unauthorized("Seems like you are not registered");
            }

            var token = _tokenService.GenerateToken(existingUser.Username);
            existingUser.Token = token;
            return Ok(token);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _tokenService.RemoveToken(token);
            return NoContent();
        }

        [HttpGet("check-token")]
        public IActionResult CheckToken()
        {
            return Ok();
        }
    }
}
