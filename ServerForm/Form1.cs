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
        }

        static ServerClientListener MainServer;
        private void Form1_Load(object sender, EventArgs e)
        {
            MainServer = new ServerClientListener("172.0.0.1", 2046);
            ListViewClients.Columns.AddRange(new ColumnHeader[]
            {
                new ColumnHeader(Name = "Name"),
                new ColumnHeader(Name = "UserName"),
                new ColumnHeader(Name = "JoinDate"),
                new ColumnHeader(Name = "Game Status"),
                new ColumnHeader(Name = "Opponent Name"),
                new ColumnHeader(Name = "Opponent UserName"),
            });
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
            while(ListViewClients != null)
            {
                UpdateListViewData();
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
