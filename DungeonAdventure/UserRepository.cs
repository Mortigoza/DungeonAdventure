using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonAdventure
{
    public class UserRepository : IUserRepository
    {
        private List<User> users = new List<User>();

        public void Register(User user)
        {
            if (!IsUserRegistered(user.Username))
            {
                users.Add(user);
            }
        }

        public User GetUser(string username)
        {
            return users.FirstOrDefault(u => u.Username == username);
        }

        public bool IsUserRegistered(string username)
        {
            return users.Any(u => u.Username == username);
        }
    }
}
