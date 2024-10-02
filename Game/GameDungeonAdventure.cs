using DungeonAdventure;
using DungeonAdventure.CapaDatos;

namespace Game
{
    public partial class GameDungeonAdventure : Form
    {
        private TextBox txtOutput;
        private TextBox txtInput;
        private Button btnEnter;
        private Dungeon _dungeon;
        private Player _player;
        private GameLogic _gameLogic;
        public GameDungeonAdventure()
        {
            InitializeComponent();
            txtOutput = new TextBox();
            txtOutput.Multiline = true;
            txtOutput.ReadOnly = true;
            txtOutput.Dock = DockStyle.Top;

            txtInput = new TextBox();
            txtInput.Dock = DockStyle.Bottom;

            btnEnter = new Button();
            btnEnter.Text = "Enter";
            btnEnter.Dock = DockStyle.Bottom;
            btnEnter.Click += BtnEnter_Click;

            Controls.Add(txtOutput);
            Controls.Add(txtInput);
            Controls.Add(btnEnter);

            _dungeon = new Dungeon();
            _player = new Player("Hero", 100, 10);
            var monsterRepo = new InMemoryMonsterRepository();
            _gameLogic = new GameLogic(monsterRepo);

            _dungeon.AddMonster(new Monster("Goblin", 30, 5, 2));
            _dungeon.AddMonster(new Monster("Orc", 50, 10, 5));

            txtOutput.Text = "Estás en una oscura cueva. A la luz de tu antorcha, ves una puerta al norte y un camino hacia el oeste.\n" +
                     "¿Qué haces? Escribe uno de los siguientes comandos:\n" +
                     "- norte\n" +
                     "- oeste\n" +
                     "- atacar\n" +
                     "- curar\n" +
                     "Escribe tu elección y presiona 'Enter'.";
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240); 
            txtOutput.BackColor = System.Drawing.Color.White; 
            txtOutput.ForeColor = System.Drawing.Color.Black; 
            txtInput.BackColor = System.Drawing.Color.FromArgb(230, 230, 230); 
            txtInput.ForeColor = System.Drawing.Color.Black;
            txtOutput.Dock = DockStyle.Fill; 
            txtOutput.SelectionStart = txtOutput.Text.Length;
            txtOutput.ScrollToCaret();

            // Estilo del botón
            btnEnter.BackColor = System.Drawing.Color.LightBlue; 
            btnEnter.FlatStyle = FlatStyle.Flat; 
            btnEnter.ForeColor = System.Drawing.Color.White; 
            txtOutput.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            txtInput.Font = new System.Drawing.Font("Arial", 12);
            txtInput.Text = "Tu respuesta aquí";
            btnEnter.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            btnEnter.Height = 50;
            btnEnter.MouseEnter += (s, e) => btnEnter.BackColor = System.Drawing.Color.DarkBlue;
            btnEnter.MouseLeave += (s, e) => btnEnter.BackColor = System.Drawing.Color.LightBlue;
            GroupBox groupBoxPlayerStats = new GroupBox();
            groupBoxPlayerStats.Text = "Estadísticas del Jugador";
            groupBoxPlayerStats.Dock = DockStyle.Bottom;
            Controls.Add(groupBoxPlayerStats);

            Label lblHealth = new Label { Text = $"Salud: {_player.Health}", Dock = DockStyle.Top };
            Label lblGold = new Label { Text = $"Oro: {_player.Gold}", Dock = DockStyle.Top };
            groupBoxPlayerStats.Controls.Add(lblHealth);
            groupBoxPlayerStats.Controls.Add(lblGold);



        }

        private void InitializePlaceholder()
        {
            txtInput.Text = "Tu respuesta aquí"; // Texto de sugerencia
            txtInput.ForeColor = System.Drawing.Color.Gray; // Color gris para el texto de sugerencia
            txtInput.Leave += TxtInput_Leave; // Evento cuando el TextBox pierde el foco
            txtInput.Enter += TxtInput_Enter; // Evento cuando el TextBox recibe el foco
        }

        private void TxtInput_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                txtInput.Text = "Tu respuesta aquí"; // Restablecer el texto de sugerencia
                txtInput.ForeColor = System.Drawing.Color.Gray; // Regresar el color gris
            }
        }

        private void TxtInput_Enter(object sender, EventArgs e)
        {
            if (txtInput.Text == "Tu respuesta aquí")
            {
                txtInput.Text = ""; // Limpiar el TextBox cuando recibe el foco
                txtInput.ForeColor = System.Drawing.Color.Black; // Cambiar a color negro
            }
        }
        private void BtnEnter_Click(object sender, EventArgs e)
        {
            string input = txtInput.Text.ToLower();
            txtInput.Text = "";

            switch (input)
            {
                case "norte":
                    txtOutput.AppendText("\nEntras en una habitación más grande.");
                    ExploreDungeon();
                    break;
                case "oeste":
                    txtOutput.AppendText("\nEncuentras un cofre con oro.");
                    _player.EarnGold(10);
                    txtOutput.AppendText($"\nHas ganado 10 de oro. Ahora tienes {_player.Gold} de oro.");
                    break;
                case "atacar":
                    BattleMonster();
                    break;
                case "curar":
                    HealPlayer();
                    break;
                default:
                    txtOutput.AppendText("\nNo entiendo ese comando.");
                    break;
            }
        }
        private void ExploreDungeon()
        {
            var monster = _dungeon.GetRandomMonster();
            if (monster != null)
            {
                txtOutput.AppendText($"\nTe encuentras con un {monster.Name}!");
            }
            else
            {
                txtOutput.AppendText("\nNo hay monstruos aquí.");
            }
        }

        private void BattleMonster()
        {
            var monster = _dungeon.GetRandomMonster();
            if (monster != null)
            {
                var result = _gameLogic.Battle(_player, monster);
                txtOutput.AppendText($"\n{result}");

                if (!monster.IsAlive())
                {
                    _dungeon.RemoveMonster(monster);
                    var lootResult = _gameLogic.LootMonster(_player, monster);
                    txtOutput.AppendText($"\n{lootResult}");
                }
            }
            else
            {
                txtOutput.AppendText("\nNo hay monstruos con los que pelear.");
            }

            CheckPlayerStatus();
        }

        private void HealPlayer()
        {
            var result = _gameLogic.HealPlayer(_player, 10);
            txtOutput.AppendText($"\n{result}");
        }

        private void CheckPlayerStatus()
        {
            var status = _gameLogic.CheckPlayerStatus(_player);
            txtOutput.AppendText($"\n{status}");
        }
    }
    public class InMemoryMonsterRepository : IMonsterRepository
    {
        private List<Monster> _monsters = new List<Monster>
        {
            new Monster("Goblin", 30, 5, 2),
            new Monster("Orc", 50, 10, 5)
        };

        public List<Monster> GetMonsters()
        {
            return _monsters;
        }
    }
}
