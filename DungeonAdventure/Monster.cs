using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonAdventure.CapaDatos
{
    public class Monster
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }
        public int Defense { get; set; }

        public Monster(string name, int health, int attackPower, int defense)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
            Defense = defense;
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public void TakeDamage(int damage)
        {
            Health -= Math.Max(damage - Defense, 0); // Defense reduces damage taken
            if (Health < 0) Health = 0; // Prevent negative health
        }

        public void Attack(Player player)
        {
            player.TakeDamage(AttackPower);
        }

        public int GetLoot()
        {
            return new Random().Next(10, 50); // Random loot amount
        }
    }
}
