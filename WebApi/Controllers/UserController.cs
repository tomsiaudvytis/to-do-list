using Common.Configurations;
using Common.Interfaces.Services;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Authentication _authentication;
        private readonly IUserService _userService;

        public UserController(IOptions<Authentication> authentication, IUserService userService)
        {
            _userService = userService;
            _authentication = authentication.Value;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] AuthenticateRequest authenticateRequest)
        {
            var response = _userService.Authenticate(authenticateRequest);

            if (response == null)
            {
                return BadRequest(new {message = "Email and/or password is incorrect"});
            }

            return Ok(response);
        }
    }
}