using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RockPaperScissorNetworkLibrary;

namespace ServerForm
{
    public class ClientWorker : Worker
    {
        private NetworkConnection _conn;

        private Thread _currentThread;

        public ClientWorker(TcpClient client) : base(null, 50)
        {
            _conn = new NetworkConnection(client);
        }

        public override void Start()
        {
            _currentThread = Thread.CurrentThread;
        }

        public override void Update()
        {

        }



        public void Join()
        {
            Stop();
            _currentThread.Join();
        }

    }
}
