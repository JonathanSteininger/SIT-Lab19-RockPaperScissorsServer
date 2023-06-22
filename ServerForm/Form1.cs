using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerForm
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
            MainServer?.StopListener();
        }

        static ServerClientListener MainServer;
        private void Form1_Load(object sender, EventArgs e)
        {
            MainServer = new ServerClientListener("127.0.0.1", 2046);
            MainServer.StartServer();
            ListViewClients.View = View.Details;
            ListViewClients.Columns.Add("Name");
            ListViewClients.Columns.Add("UserName");
            ListViewClients.Columns.Add("Join Date");
            ListViewClients.Columns.Add("Game Status");
            ListViewClients.Columns.Add("Opponent Name");
            ListViewClients.Columns.Add("Opponent UserName");

            int width = 120;
            foreach (ColumnHeader header in ListViewClients.Columns) header.Width = width;
            ListViewClients.Size = new Size(width * ListViewClients.Columns.Count + 20, ListViewClients.Height);
            UpdateListView();
        }
        /*
         * Player opponent = w.GameManager.FindOpponent(w.Player);
                rows.Add(new string[] {
                    w.Player.DisplayName,
                    w.Player.UserName,
                    w.Player.JoinDate.ToShortDateString(),
                    w.GameManager.Game.WaitingForPlayer ? "Waiting":"Playing",
                    opponent.DisplayName ?? "none",
                    opponent.UserName ?? "none"
                }) ;
        */

        private async void UpdateListView()
        {
            while (ListViewClients != null )
            {
                if(MainServer != null && MainServer.IsRunning) UpdateListViewData();
                await Task.Delay(100);
            }

        }

        private void UpdateListViewData()
        {
            ListViewClients.Items.Clear();
            foreach (string[] rowItems in MainServer.GetClientsListViewData())
            {
                ListViewClients.Items.Add(new ListViewItem(rowItems));
            }
        }
    }
}
