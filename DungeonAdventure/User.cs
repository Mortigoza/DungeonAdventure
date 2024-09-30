using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonAdventure
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
            Level = 1;
            Experience = 0;
            Gold = 0;
        }

        public void GainExperience(int amount)
        {
            Experience += amount;
            if (Experience >= 100)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            Experience = 0; // Reset experience after leveling up
        }

        public void EarnGold(int amount)
        {
            Gold += amount;
        }

        public void SpendGold(int amount)
        {
            if (Gold >= amount)
                Gold -= amount;
        }
    }
}
