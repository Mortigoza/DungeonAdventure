using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonAdventure.CapaDatos
{
    public class Player
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }
        public int Gold { get; set; }

        public Player(string name, int health, int attackPower, int gold = 0)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
            Gold = gold; // Permite inicializar con un valor de oro específico
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0; // Prevent negative health
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public void Heal(int amount)
        {
            Health += amount;
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
