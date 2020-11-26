using System.Collections.Generic;
using Common.Models;

namespace Common.Interfaces.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest authenticateRequest);
        IEnumerable<User> GetAll();
        User GetByEmail(string email);
    }
}