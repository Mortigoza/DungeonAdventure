using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DungeonAdventure.CapaDatos
{
    public class Dungeon
    {
        public List<Monster> Monsters { get; set; } = new List<Monster>();

        public void AddMonster(Monster monster)
        {
            Monsters.Add(monster);
        }

        public void RemoveMonster(Monster monster)
        {
            Monsters.Remove(monster);
        }

        public bool HasMonsters()
        {
            return Monsters.Count > 0;
        }

        public int MonsterCount()
        {
            return Monsters.Count;
        }

        public void ClearDungeon()
        {
            Monsters.Clear();
        }

        public Monster GetRandomMonster()
        {
            if (Monsters.Count == 0) return null;
            var random = new Random();
            return Monsters[random.Next(Monsters.Count)];
        }
    }
}
