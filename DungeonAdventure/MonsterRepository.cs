using DungeonAdventure.CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonAdventure
{
    public class MonsterRepository : IMonsterRepository
    {
        private List<Monster> monsters = new List<Monster>();

        public List<Monster> GetMonsters()
        {
            return monsters;
        }

        public void AddMonster(Monster monster)
        {
            monsters.Add(monster);
        }
    }
}
