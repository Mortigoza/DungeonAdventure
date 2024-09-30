using DungeonAdventure.CapaDatos;
using System.Collections.Generic;

namespace DungeonAdventure
{
    public interface IMonsterRepository
    {
        List<Monster> GetMonsters();
    }
}