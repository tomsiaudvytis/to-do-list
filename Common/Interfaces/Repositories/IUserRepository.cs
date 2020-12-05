using Common.Models;
using System.Collections.Generic;

namespace Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        User Authenticate(string password, string email);
        User GetByEmail(string email);
        IEnumerable<User> GetAll();
    }
}
