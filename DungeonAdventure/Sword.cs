using DungeonAdventure.CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DungeonAdventure
{
    public class Sword
    {
        public int AttackPower { get; set; }
        public int Gold { get; set; }
        public Sword(int gold, int attackPower)
        {            
            AttackPower = attackPower;
            Gold = gold;
        }

        public void Use(Monster monster)
        {
            monster.TakeDamage(AttackPower);
        }
        public void EquipPlayer(Player player)
        {
            player.AttackPower += AttackPower;
        }
        public void EquipMonster(Monster monster)
        {
            monster.AttackPower += AttackPower;
        }

    }
}
