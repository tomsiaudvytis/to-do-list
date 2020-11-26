using System.Collections.Generic;
using System.Linq;
using Common.Configurations;
using Common.Enums;
using Common.Interfaces.Services;
using Common.Models;
using Microsoft.Extensions.Options;
using WebApi.Extensions;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User>
        {
            new User
            {
                Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "1234567891012",
                Email = "some@email.com", Role = UserRole.Regular
            },
            new User
            {
                Id = 2, FirstName = "Test2", LastName = "User2", Username = "test2", Password = "1234567891012",
                Email = "some2@email.com", Role = UserRole.Admin
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

            var token = user.GenerateJwtToken(_authentication.Secret);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
            => _users;

        public User GetByEmail(string email)
            => _users.FirstOrDefault(x => x.Email == email);
    }
}