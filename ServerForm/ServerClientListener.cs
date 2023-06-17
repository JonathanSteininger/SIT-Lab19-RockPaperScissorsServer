using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ServerForm
{
    public class ServerClientListener
    {
        private TcpListener _listener;
        private string ip;
        private int port;

        
        public ServerClientListener(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public void StartFindingClients()
        {
            _listener = new TcpListener(IPAddress.Parse(ip),port);
            _listener.Start();

            while(true)
            {

            }
        }
    }
    public class ClientThreadManage
    {
        public List<Worker>
    }
}
