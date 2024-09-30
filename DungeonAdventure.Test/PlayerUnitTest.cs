using DungeonAdventure.CapaDatos;
using Moq;

namespace DungeonAdventure.Test
{
    public class PlayerUnitTest
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void JugadorEstaVivoExitoso_Test()
        {
            var jugador = new Player("Heroe", 100, 50);

            bool resultado = jugador.IsAlive();

            Assert.IsTrue(resultado);
        }

        [Test]
        public void JugadorEstaVivoFalso_Test()
        {
            var jugador = new Player("Heroe", 0, 50);

            bool resultado = jugador.IsAlive();

            Assert.IsFalse(resultado);
        }

        [Test]
        public void ReducirSalud_Test()
        {
            var jugador = new Player("Heroe", 100, 50);

            jugador.TakeDamage(20);

            Assert.AreEqual(80, jugador.Health);
        }

        [Test]
        public void SaludNoMenorACero_Test()
        {
            var jugador = new Player("Heroe", 10, 50);

            jugador.TakeDamage(20);

            Assert.AreEqual(0, jugador.Health);
        }

        [Test]
        public void IncrementarSaludPorValor_Test()
        {
            var jugador = new Player("Heroe", 50, 50);

            jugador.Heal(30);

            Assert.AreEqual(80, jugador.Health);
        }

        [Test]
        public void IncrementarGold_Test()
        {
            var jugador = new Player("Heroe", 100, 50, 50);
            jugador.EarnGold(30);

            Assert.AreEqual(80, jugador.Gold);
        }
    }
}