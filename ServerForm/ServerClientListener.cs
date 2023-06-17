using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using RockPaperScissorNetworkLibrary;

namespace ServerForm
{
    public class ServerClientListener
    {
        private TcpListener _listener;
        private string ip;
        private int port;

        private ClientThreadManager _threadManager;

        public ServerClientListener(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            _threadManager = new ClientThreadManager();
        }

        public void StartServer()
        {
            if(_listener == null) _listener = new TcpListener(IPAddress.Parse(ip), port);
            _listener.Start();
            StartListening();
        }
        public void StopListener()
        {
            _keepListening = false;
            _listener.Stop();
        }

        private bool _keepListening = true;
        private async void StartListening()
        {
            _keepListening = true;
            while(_keepListening)
            {
                ClientWorker client = new ClientWorker(_listener.AcceptTcpClient());
                Thread thread = new Thread(client.Run);
                _threadManager.Add(client);
                thread.Start();
            }
        }
    }
    
}
