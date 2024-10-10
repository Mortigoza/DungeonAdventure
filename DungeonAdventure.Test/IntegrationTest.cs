using DungeonAdventure.CapaDatos;
using Moq;

namespace DungeonAdventure.Test
{
    internal class IntegrationTest
    {
        private Mock<IMonsterRepository> _mockMonsterRepository;
        private Mock<IUserRepository> _mockUserRepository;
        private AuthService authService;
        private Dungeon dungeon;
        private GameLogic game;
        private Sword sword;
        private Monster monster, superMonster;
        private Player player;
        private User usuario;

        [SetUp]
        public void Setup()
        {
            _mockMonsterRepository = new Mock<IMonsterRepository>();
            _mockMonsterRepository.Setup(m => m.GetMonsters()).Returns(new List<Monster> { monster });
            _mockUserRepository = new Mock<IUserRepository>();
            usuario = new User("Usuario1", "pass123");
            usuario.Experience = 1; usuario.Gold = 10;
            authService = new AuthService(_mockUserRepository.Object);

            _mockUserRepository.Setup(u => u.GetUser("Usuario1")).Returns(usuario);
            game = new GameLogic(_mockMonsterRepository.Object);
            
            player = new Player("Jugador1", 70, 50, 0);
            monster = new Monster("Monstruo1", 30,40, 5);
            superMonster= new Monster("SuperMonstruo", 30, 40, 40);
            sword = new Sword(2,20);
            dungeon = new Dungeon();
        }

        [Test]
        public void JugadorPierdeEnfrentamientoTest()
        {
            while (player.IsAlive() && monster.IsAlive())
            {
                monster.Attack(player);//el monstruo ataca primero
                if (monster.IsAlive())
                {
                    player.TakeDamage(monster.AttackPower);
                }
                else
                {
                    player.EarnGold(monster.GetLoot());
                }
            }
            Assert.IsFalse(player.IsAlive());
        }

        [Test]
        public void JugadorGanaEnfrentamientoTest()
        {
            while (player.IsAlive() && monster.IsAlive())
            {
                monster.Attack(player);
                if (monster.IsAlive())
                {
                    monster.TakeDamage(player.AttackPower);
                }
                else
                {
                    player.EarnGold(monster.GetLoot());
                }
            }
            Assert.IsTrue(player.IsAlive());
        }

        [Test]
        public void JugadorAtacaConEspadaTest()
        {
            player = new Player("Jugador1", 45, 20, 0);
            sword.EquipPlayer(player);
            while (player.IsAlive()&&monster.IsAlive())
            {
                monster.TakeDamage(player.AttackPower);
                monster.Attack(player);
            }
            Assert.IsTrue(player.IsAlive());
        }

        [Test]
        public void MonstruoAtacaConEspadaTest()
        {
            player = new Player("Jugador1", 100, 20, 0);
            sword.EquipMonster(monster);
            while (player.IsAlive() && monster.IsAlive())
            {
                monster.TakeDamage(player.AttackPower);
                monster.Attack(player);
            }
            Assert.IsFalse(player.IsAlive());
            
        }

        [Test]
        public void BatallaenCuevaTest()
        {
            var explorationResult = game.ExploreDungeon();
            Assert.AreEqual("Has encontrado monstruos.", explorationResult);

            var battleResult = game.Battle(player, monster);
            Assert.AreEqual("Jugador1 ha derrotado a Monstruo1!", battleResult);

            Assert.IsFalse(dungeon.HasMonsters());// La cueva debería estar vacía después de la victoria del jugador

        }

        [Test]
        public void EntrenamientoTest()
        {
            var Jugador = new Player("Jugador1", 11, 0, 0); //le paso la salud minima esperada para ejecutar
            int salud = 1;
            int ataque = 5;
            string resultado = game.Train(Jugador);
            Assert.AreEqual($"{Jugador.Name} ha entrenado y ahora tiene {salud} HP y {ataque} de poder de ataque.", resultado);
        }

        [Test]
        public void EntrenamientoFallidoTest()
        {
            var Jugador = new Player("Jugador1", 9, 0, 0); //le paso la salud menor a la minima esperada para ejecutar

            string resultado = game.Train(Jugador);
            Assert.AreEqual($"{Jugador.Name} no tiene suficiente salud para entrenar.", resultado);
        }

        [Test]
        public void DescansoTest()
        {
            var Jugador = new Player("Jugador1", 1, 5, 0);
            int salud = 20;
            int ataque = 3;
            string resultado = game.Rest(Jugador);
            Assert.AreEqual($"{Jugador.Name} ha descansado y ha recuperado {salud} HP, pero ha perdido algo de fuerza y ahora tiene {ataque} de poder de ataque.", resultado);

        }
        [Test]
        public void LogeoyAsignacionPlayerTest()
        {
            bool loginSuccess = authService.Login(usuario.Username,usuario.Password);
            Assert.IsTrue(loginSuccess);//chequeo de logueo exitoso            
            var player = new Player(usuario.Username, 100, 10, usuario.Gold); 
            //asigno al usuario con un jugador y sus respectivas estadisticas                        
            Assert.IsNotNull(player);
            Assert.AreEqual("Usuario1", player.Name); //compruebo que el usuario y el nombre del jugador sea el mismo

        }
        [Test]
        public void UsuarioGanaYGuardaOroTest() 
        {
            var battleResult = game.Battle(player, monster);
            if (battleResult== "Jugador1 ha derrotado a Monstruo1!")
            {
                player.EarnGold(monster.GetLoot());
                usuario.Gold+= player.Gold;
            }
            Assert.IsTrue(usuario.Gold > 10);
        }

    }
}
