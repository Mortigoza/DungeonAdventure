using DungeonAdventure.CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonAdventure
{
    public class GameLogic
    {
        private readonly IMonsterRepository _monsterRepository;

        public GameLogic(IMonsterRepository monsterRepository)
        {
            _monsterRepository = monsterRepository;
        }

        public string ExploreDungeon()
        {
            var monsters = _monsterRepository.GetMonsters();
            if (monsters == null || monsters.Count == 0)
            {
                return "No hay monstruos en esta mazmorra.";
            }
            return "Has encontrado monstruos.";
        }

        public string Battle(Player player, Monster monster)
        {
            while (monster.IsAlive() && player.IsAlive())
            {
                monster.TakeDamage(player.AttackPower);
                if (!monster.IsAlive())
                {
                    return $"{player.Name} ha derrotado a {monster.Name}!";
                }

                monster.Attack(player);
                if (!player.IsAlive())
                {
                    return $"{player.Name} ha sido derrotado por {monster.Name}!";
                }
            }
            return string.Empty;
        }

        public string HealPlayer(Player player, int amount)
        {
            player.Heal(amount);
            return $"{player.Name} se ha curado por {amount} puntos.";
        }

        public string CheckPlayerStatus(Player player)
        {
            return player.IsAlive() ? $"{player.Name} está vivo con {player.Health} HP." : $"{player.Name} está muerto.";
        }

        public string LootMonster(Player player, Monster monster)
        {
            int loot = monster.GetLoot();
            player.EarnGold(loot);
            return $"{player.Name} ha obtenido {loot} de oro de {monster.Name}.";
        }

        // Nuevos métodos para aumentar la lógica
        public string RunAway(Player player, Monster monster)
        {
            // Simular que el jugador intenta huir y tiene un 50% de éxito
            var random = new Random();
            if (random.Next(2) == 0) // 50% de probabilidad
            {
                return $"{player.Name} ha huido de {monster.Name} exitosamente.";
            }
            else
            {
                return $"{player.Name} no logró huir y debe enfrentarse a {monster.Name} nuevamente.";
            }
        }

        public string UsePotion(Player player, int healthRestore)
        {
            player.Heal(healthRestore);
            return $"{player.Name} ha utilizado una poción y restaurado {healthRestore} HP.";
        }

        public string CheckDungeonStatus(Dungeon dungeon)
        {
            return dungeon.HasMonsters() ? "La mazmora tiene monstruos." : "La mazmora está vacía.";
        }
    }
}
