using Common.Interfaces.Repositories;
using Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetByEmail(string email)
        {
            return _context.Users.Where(x => x.Email == email).FirstOrDefault();
        }

        public User Authenticate(string password, string email)
        {
            return _context.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
        }
    }
}
