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

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                Password = "1234567891012",
                Email = "some@email.com",
                Role = Policies.User
            },
            new User
            {
                Id = 2,
                FirstName = "Test2",
                LastName = "User2",
                Password = "1234567891012",
                Email = "some2@email.com",
                Role = Policies.Admin
            }
        };

        private readonly Authentication _authentication;

        public UserService(IOptions<Authentication> authentication)
        {
            _authentication = authentication.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            if (model == null)
                return null;

            var user = _users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            if (user == null)
                return null;

            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
            => _users;

        public User GetByEmail(string email)
            => _users.FirstOrDefault(x => x.Email == email);

        private string GenerateJwtToken(User user)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authentication.Secret));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("role", user.Role),
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