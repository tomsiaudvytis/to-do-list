using Common;
using Common.Interfaces.Services;
using Common.Models;
using Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserController(IUserService userService, ILogger logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] AuthenticateRequest authenticateRequest)
        {
            var response = _userService.Authenticate(authenticateRequest);

            if (response == null)
            {
                return BadRequest(new { message = "Email and/or password is incorrect" });
            }

            return Ok(response);
        }

        [HttpGet]
        [Authorize(Policy = UserRoles.Admin)]
        public IActionResult GetUsers(string email)
        {
            try
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
            catch (Exception ex)
            {
                _logger.Log(ex.Message, LogLevel.Error);
                return BadRequest(ex.Message);
            }

        }
    }
}