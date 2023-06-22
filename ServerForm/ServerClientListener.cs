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

        private bool _keepListening = true;

        public ClientThreadManager _clientWorkerManager;

        public ServerClientListener(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            _clientWorkerManager = new ClientThreadManager();
            _keepListening = false;
        }

        public bool IsRunning => _keepListening;

        public List<string[]> GetClientsListViewData() => _clientWorkerManager.GetClientListData();

        /// <summary>
        /// Starts the server and begins to search for clients.
        /// </summary>
        public void StartServer()
        {
            if(_listener == null) _listener = new TcpListener(IPAddress.Parse(ip), port);
            _listener.Start();
            StartListening();
        }
        /// <summary>
        /// Stops the looping listener, but will continue to complete its loop
        /// </summary>
        public void StopListener()
        {
            _keepListening = false;
            _listener.Stop();
        }
        /// <summary>
        /// Runs internally asyncronasly in the background. will check for clients.
        /// when one is found, will start a new thread, add the connecdtion to the client worker. and add the worker to the threadmanager
        /// </summary>
        private async void StartListening()
        {
            _keepListening = true;
            int _failCounter = 0;
            while(_keepListening)
            {
                try
                {
                    ClientWorker client = new ClientWorker(await _listener.AcceptTcpClientAsync(), _clientWorkerManager);
                    Thread thread = new Thread(client.Run);
                    _clientWorkerManager.Add(client);
                    thread.Start();
                    _failCounter = 0;
                }catch (Exception ex) {
                    _failCounter++;
                    await Task.Delay(400);
                    if (_failCounter >= 3) _keepListening = false;
                }
            }
        }
    }
    
}
