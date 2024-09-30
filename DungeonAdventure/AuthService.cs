using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonAdventure
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(string username, string password)
        {
            if (_userRepository.IsUserRegistered(username))
                throw new Exception("Username already exists.");

            _userRepository.Register(new User(username, password));
        }

        public bool Login(string username, string password)
        {
            var user = _userRepository.GetUser(username);
            return user != null && user.Password == password;
        }

        public void ChangePassword(string username, string newPassword)
        {
            var user = _userRepository.GetUser(username);
            if (user == null) throw new Exception("User not found.");
            user.Password = newPassword;
        }

        public int GetUserLevel(string username)
        {
            var user = _userRepository.GetUser(username);
            return user?.Level ?? 0; // Return level or 0 if user doesn't exist
        }

        public void AddGold(string username, int amount)
        {
            var user = _userRepository.GetUser(username);
            if (user != null)
                user.EarnGold(amount);
        }

        public void SpendGold(string username, int amount)
        {
            var user = _userRepository.GetUser(username);
            if (user != null)
                user.SpendGold(amount);
        }
    }
}
