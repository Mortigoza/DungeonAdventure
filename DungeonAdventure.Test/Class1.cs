using DungeonAdventure.CapaDatos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DungeonAdventure.Test
{
    internal class Class1
    {
        private Mock<IUserRepository> _mockUserRepository;

        private Mock<IMonsterRepository> repositoriomonstruo;

        private AuthService _authService;
        private GameLogic _GameLogic;
        private User _testUser;
        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            repositoriomonstruo = new Mock<IMonsterRepository>();

            _authService = new AuthService(_mockUserRepository.Object);
            _GameLogic = new GameLogic(repositoriomonstruo.Object);
            _testUser = new User("usuario1", "pass123");
            _mockUserRepository.Setup(repo => repo.GetUser("usuario1")).Returns(_testUser);
        }

        [Test]
        public void AgregarOroTest()
        {
            int initialgold = 0;
            int amountToAdd = 50;
            _authService.AddGold("usuario1", amountToAdd);

            Assert.AreEqual(initialgold + amountToAdd, _testUser.Gold);
        }

        [Test]
        public void EncontrarTesoroTest() 
        {
            var Jugador = new Player("Jugador1", 100, 50,0);
            var random = new Random();
            int tesoroencontrado =random.Next(10,100);
            string resultado = _GameLogic.FindTreasure(Jugador);
            Assert.AreEqual($"{Jugador} ha encontrado un tesoro y ha ganado {tesoroencontrado} de oro!", resultado);
        }

        [Test]
        public void EntrenamientoTest() 
        {
            var Jugador = new Player("Jugador1",11,0,0); //le paso la salud minima esperada para ejecutar
            int salud = 1;
            int ataque = 5;
            string resultado = _GameLogic.Train(Jugador);
            Assert.AreEqual($"{Jugador.Name} ha entrenado y ahora tiene {salud} HP y {ataque} de poder de ataque.", resultado);
        }
        [Test]
        public void EntrenamientoFallidoTest()
        {
            var Jugador = new Player("Jugador1", 9, 0, 0); //le paso la salud menor a la minima esperada para ejecutar
            
            string resultado = _GameLogic.Train(Jugador);
            Assert.AreEqual($"{Jugador.Name} no tiene suficiente salud para entrenar.", resultado);
        }

        [Test]
        public void DescansoTest()
        {
            var Jugador = new Player("Jugador1", 1, 5, 0); 
            int salud = 20; 
            int ataque = 3;
            string resultado = _GameLogic.Rest(Jugador);
            Assert.AreEqual($"{Jugador.Name} ha descansado y ha recuperado {salud} HP, pero ha perdido algo de fuerza y ahora tiene {ataque} de poder de ataque.",resultado);

        }

    }
}
