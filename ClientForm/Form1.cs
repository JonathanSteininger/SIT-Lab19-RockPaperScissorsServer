using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RockPaperScissorNetworkLibrary;

namespace ClientForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_conn.Connected) _conn.Send(new GameCommand(RockPaperScissorNetworkLibrary.CommandType.QuitServer));
        }

        private NetworkConnection _conn;
        private Player _player;

        private void Form1_Load(object sender, EventArgs e)
        {
            _conn = new NetworkConnection("127.0.0.1", 2046);
            _player = new Player("Jonathan", "Jono123");
            _conn.Connect();
            _conn.Send(new GameSendData(DataSentType.PlayerInfo, _player));
        }
    }
}
