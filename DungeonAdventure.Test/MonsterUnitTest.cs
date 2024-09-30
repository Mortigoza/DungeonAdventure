using DungeonAdventure.CapaDatos;
using Moq;

namespace DungeonAdventure.Test
{
    public class MonsterUnitTest
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DanioMenorAdefenaSaludNoCambia_Test()
        {
            var monster = new Monster("Goblin", 50, 10, 10);
            monster.TakeDamage(5);
            Assert.AreEqual(50, monster.Health); 
        }
    }
}