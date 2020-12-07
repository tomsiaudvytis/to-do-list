using System.Collections.Generic;
using System.Linq;
using Common.Configurations;
using Common.Interfaces.Services;
using Common.Models;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Common;
using Common.Interfaces.Repositories;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly Authentication _authentication;
        private readonly IUserRepository _userRepository;

        public UserService(IOptions<Authentication> authentication, IUserRepository userRepository)
        {
            _authentication = authentication.Value;
            _userRepository = userRepository;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            if (model == null)
                return null;

            var user = _userRepository.Authenticate(model.Password, model.Email);

            if (user == null)
                return null;

            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll() => _userRepository.GetAll();

        public User GetByEmail(string email) => _userRepository.GetByEmail(email);

        private string GenerateJwtToken(User user)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authentication.Secret));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("role", user.Role),
                new Claim("user_id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}