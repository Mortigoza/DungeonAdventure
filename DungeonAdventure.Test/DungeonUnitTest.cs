using DungeonAdventure.CapaDatos;
using Moq;

namespace DungeonAdventure.Test
{
    public class DungeonUnitTest
    {
        private Dungeon _dungeon;

        [SetUp]
        public void Setup()
        {
            _dungeon = new Dungeon();
        }

        [Test]
        public void TieneMonstruosDevuelveTrue_Test()
        {
            _dungeon.AddMonster(new Monster("Goblin", 50, 10, 2));
            bool resultado = _dungeon.HasMonsters();
            Assert.IsTrue(resultado);
        }

        [Test]
        public void ContadorDeMonstruosExisten_Test()
        {
            _dungeon.AddMonster(new Monster("Goblin", 50, 10, 2));
            _dungeon.AddMonster(new Monster("Orc", 100, 20, 5));
            int cuentaTotal = _dungeon.MonsterCount();
            Assert.AreEqual(2, cuentaTotal);
        }

        [Test]
        public void RemoverTodosLosMonstruos_Test()
        {
            _dungeon.AddMonster(new Monster("Goblin", 50, 10, 2));
            _dungeon.ClearDungeon();
            int cuentaTotal = _dungeon.MonsterCount();
            Assert.AreEqual(0, cuentaTotal);
        }

        [Test]
        public void ObtenerMonstruoAleatorio_Test()
        {
            var monstruo1 = new Monster("Goblin", 50, 10, 2);
            var monstruo2 = new Monster("Orc", 100, 20, 5);
            _dungeon.AddMonster(monstruo1);
            _dungeon.AddMonster(monstruo2);

            Monster resultado = _dungeon.GetRandomMonster();

            Assert.IsTrue(resultado == monstruo1 || resultado == monstruo2);
        }
    }
}