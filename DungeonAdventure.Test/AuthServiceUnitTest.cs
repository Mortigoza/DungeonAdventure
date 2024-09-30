using DungeonAdventure.CapaDatos;
using Moq;

namespace DungeonAdventure.Test
{
    public class AuthServiceUnitTest
    {
        private Mock<IUserRepository> _mockUserRepository;
        private AuthService _authService;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _authService = new AuthService(_mockUserRepository.Object);
        }

        [Test]
        public void RegistroExitoso_Test()
        {
            _mockUserRepository.Setup(repo => repo.IsUserRegistered("Jugador1")).Returns(false);

            _authService.Register("Jugador1", "contraseña123");

            _mockUserRepository.Verify(repo => repo.Register(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void UsuarioExistenteExcepcion_Test()
        {
            _mockUserRepository.Setup(repo => repo.IsUserRegistered("Jugador1")).Returns(true);

            Assert.Throws<Exception>(() => _authService.Register("Jugador1", "nuevaContraseña"));
        }

        [Test]
        public void LoginExitoso_Test()
        {
            var usuario = new User("Jugador1", "contraseña123");
            _mockUserRepository.Setup(repo => repo.GetUser("Jugador1")).Returns(usuario);

            bool resultado = _authService.Login("Jugador1", "contraseña123");

            Assert.IsTrue(resultado);
        }

        [Test]
        public void LoginInvalido_Test()
        {
            var usuario = new User("Jugador1", "contraseña123");
            _mockUserRepository.Setup(repo => repo.GetUser("Jugador1")).Returns(usuario);

            bool resultado = _authService.Login("Jugador1", "contraseñaMAL");

            Assert.IsFalse(resultado);
        }

        [Test]
        public void CambiarContraseniaExitosa_Test()
        {
            var usuario = new User("Jugador1", "contraseña123");
            _mockUserRepository.Setup(repo => repo.GetUser("Jugador1")).Returns(usuario);

            _authService.ChangePassword("Jugador1", "nuevaContraseña123");

            Assert.AreEqual("nuevaContraseña123", usuario.Password);
        }

        [Test]
        public void ObtenerNivelUSuario_Test()
        {
            var usuario = new User("Jugador1", "contraseña123");
            usuario.GainExperience(150);
            _mockUserRepository.Setup(repo => repo.GetUser("Jugador1")).Returns(usuario);

            int level = _authService.GetUserLevel("Jugador1");

            Assert.AreEqual(2, level);
        }


    }
}