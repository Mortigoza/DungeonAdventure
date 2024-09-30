using DungeonAdventure.CapaDatos;
using Moq;

namespace DungeonAdventure.Test
{
    public class RegressionTest
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void JugadorConSaludNoMenorACero()
        {
            var jugador = new Player("Heroe", 50, 10);
            jugador.TakeDamage(60); 
            Assert.AreEqual(0, jugador.Health); 
        }

        [Test]
        public void JugadorSanaCorrectamente()
        {
            var jugador = new Player("Heroe", 30, 10);
            jugador.Heal(20); 
            Assert.AreEqual(50, jugador.Health);
        }

        [Test]
        public void JugadorIncrementaGoldCorrectamente()
        {
            var jugador = new Player("Heroe", 100, 10);
            jugador.EarnGold(50);
            Assert.AreEqual(50, jugador.Gold);
        }

        [Test]
        public void JugadorGastaGoldDecrementaCorrectamente()
        {
            var jugador = new Player("Heroe", 100, 10);
            jugador.EarnGold(100);
            jugador.SpendGold(30);
            Assert.AreEqual(70, jugador.Gold);
        }

        [Test]
        public void MonstruoAtacaConsideraDefensa()
        {
            var monstruo = new Monster("Goblin", 50, 10, 5);
            monstruo.TakeDamage(20);
            Assert.AreEqual(35, monstruo.Health);
        }

        [Test]
        public void MonstruoAtacaJugadorBajaSalud()
        {
            var jugador = new Player("Heroe", 100, 10);
            var monstruo = new Monster("Goblin", 50, 10, 2);
            monstruo.Attack(jugador);
            Assert.AreEqual(90, jugador.Health);
        }

        [Test]
        public void MonstruoVivoCuandoSaludPositiva()
        {
            var monstruo = new Monster("Goblin", 50, 10, 2);
            Assert.IsTrue(monstruo.IsAlive()); 
        }

        [Test]
        public void JugadorSaludNoMayorACien()
        {
            var jugador = new Player("Heroe", 100, 20);
            jugador.TakeDamage(30); 

            jugador.Heal(50); 

            Assert.AreEqual(120, jugador.Health, "La salud del jugador no debería exceder 100.");
        }

        [Test]
        public void JugadorNoPuedeGastarMasGoldDelQueTiene()
        {
            var jugador = new Player("Heroe", 100, 20, 50);

            jugador.SpendGold(70);

            Assert.AreEqual(50, jugador.Gold, "El oro del jugador no debería cambiar si intenta gastar más del que tiene.");
        }

        [Test]
        public void JugadorGanaGoldIncrementaValor()
        {
            var jugador = new Player("Heroe", 100, 20, 30); // Inicia con 30 de oro

            jugador.EarnGold(20); // Gana 20 de oro

            Assert.AreEqual(50, jugador.Gold, "El oro del jugador debería ser 50 después de ganar 20.");
        }
    }
}