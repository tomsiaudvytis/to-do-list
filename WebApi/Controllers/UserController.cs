using Common;
using Common.Interfaces.Services;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
            => _userService = userService;

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

        [HttpGet]
        [Authorize(Policy = UserRoles.Admin)]
        public IActionResult GetUsers(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Ok(_userService.GetAll());
            }

            var user = _userService.GetByEmail(email);

            if (user == null)
                return NotFound();

            return Ok(_userService.GetByEmail(email));
        }
    }
}