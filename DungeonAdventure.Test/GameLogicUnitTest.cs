using DungeonAdventure.CapaDatos;
using Moq;

namespace DungeonAdventure.Test
{
    public class GameLogicUnitTest
    {
        private Mock<IMonsterRepository> _mockMonsterRepository;
        private GameLogic _gameLogic;

        [SetUp]
        public void Setup()
        {
            _mockMonsterRepository = new Mock<IMonsterRepository>();
            _gameLogic = new GameLogic(_mockMonsterRepository.Object);
        }

        [Test]
        public void NoHayMonstruosMensaje_Test()
        {
            _mockMonsterRepository.Setup(repo => repo.GetMonsters()).Returns(new List<Monster>());

            string resultado = _gameLogic.ExploreDungeon();

            Assert.AreEqual("No hay monstruos en esta mazmorra.", resultado);
        }

        [Test]
        public void SiHayMonstruosMensaje_Test()
        {
            var monstruos = new List<Monster> { new Monster("Goblin", 50, 10, 2) };
            _mockMonsterRepository.Setup(repo => repo.GetMonsters()).Returns(monstruos);

            string resultado = _gameLogic.ExploreDungeon();

            Assert.AreEqual("Has encontrado monstruos.", resultado);
        }

        [Test]
        public void HasVencidoAlMonstruo_Test()
        {
            var jugador = new Player("Heroe", 100, 20);
            var monstruo = new Monster("Goblin", 50, 10, 2);

            string resultado = _gameLogic.Battle(jugador, monstruo);

            Assert.AreEqual("Heroe ha derrotado a Goblin!", resultado);
        }

        [Test]
        public void HasSidoDerrotadoPorMonstruo_Test()
        {
            var jugador = new Player("Heroe", 30, 5); 
            var monstruo = new Monster("Goblin", 50, 25, 2);

            string resultado = _gameLogic.Battle(jugador, monstruo);

            Assert.AreEqual("Heroe ha sido derrotado por Goblin!", resultado);
        }

        [Test]
        public void IncrementarSalud_Test()
        {
            var jugador = new Player("Heroe", 50, 20);

            string resultado = _gameLogic.HealPlayer(jugador, 20);

            Assert.AreEqual(70, jugador.Health);
            Assert.AreEqual("Heroe se ha curado por 20 puntos.", resultado);
        }

        [Test]
        public void ChequearEstadoJugadorVivo_Test()
        {
            var jugador = new Player("Heroe", 100, 20);

            string resultado = _gameLogic.CheckPlayerStatus(jugador);

            Assert.AreEqual("Heroe está vivo con 100 HP.", resultado);
        }

        [Test]
        public void ChequearEstadoJugadorMuerto_Test()
        {
            var jugador = new Player("Heroe", 0, 20);

            string resultado = _gameLogic.CheckPlayerStatus(jugador);

            Assert.AreEqual("Heroe está muerto.", resultado);
        }

        [Test]
        public void IncrementaJugadorGold_Test()
        {
            var jugador = new Player("Heroe", 100, 20);
            var monstruo = new Monster("Goblin", 50, 10, 2);

            string resultado = _gameLogic.LootMonster(jugador, monstruo);

            Assert.IsTrue(jugador.Gold > 0);
        }
    }
}