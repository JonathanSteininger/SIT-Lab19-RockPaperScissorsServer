using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace RockPaperScissorNetworkLibrary
{
    public class NetworkConnection : IDisposable
    {
        private string ip;
        private int port;
        private TcpClient tcpClient;
        private NetworkStream stream;
        private BinaryReader reader;
        private BinaryWriter writer;

        public string IP { get { return ip; } set { ip = value; } }
        public int Port { get { return port; } set { port = value; } }

        private bool _serverSide = false;

        private bool _connected;
        public bool Connected { get { return _connected; } }

        public NetworkConnection(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            _connected = false;
        }
        public NetworkConnection(TcpClient tcpClient)
        {
            if (!tcpClient.Connected) throw new Exception("Must be an active client connection");
            this.tcpClient = tcpClient;
            _serverSide = true;
            _connected = true;
        }
        /// <summary>
        /// waits for something to read in the network stream, return anything thats found.
        /// </summary>
        /// <typeparam name="T">object type meant to be read</typeparam>
        /// <returns>object read from stream</returns>
        public T Read<T>()
        {
            CheckConnected();
            return JsonFactory.Deserialize<T>(reader.ReadString());
        }
        public object ReadAuto()
        {
            CheckConnected();
            return JsonFactory.DeserializeAuto(reader.ReadString());
        }
        /// <summary>
        /// Sends an object to the network stream.
        /// </summary>
        /// <param name="obj">object being sent to the stream</param>
        public void Send(object obj)
        {
            CheckConnected();
            writer.Write(JsonFactory.Serialize(obj));
        }
        /// <summary>
        /// Sends an object to stream, and returns the response from the same stream.
        /// </summary>
        /// <typeparam name="T">Response Type</typeparam>
        /// <param name="ObjectSent">The Object being sent</param>
        /// <returns>The response object</returns>
        public T RequestData<T>(object ObjectSent)
        {
            Send(ObjectSent);
            return Read<T>();
        }
        public object RequestDataAuto(object ObjectSent)
        {
            Send(ObjectSent);
            return ReadAuto();
        }
        /// <summary>
        /// Checks if the Connection is connected. 
        /// </summary>
        /// <exception cref="Exception">Throws if the connection if not connected</exception>
        private void CheckConnected()
        {
            if (!Connected || tcpClient == null || !tcpClient.Connected) throw new Exception("Not Connected to server");
        }


        /// <summary>
        /// Connects to the Destination
        /// </summary>
        public void Connect()
        {
            if (_serverSide) throw new Exception("Server Connections cant start connections. can only recive connection requests.");
            Disconnect();
            tcpClient = new TcpClient(ip, port);
            stream = tcpClient.GetStream();
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);
            _connected = true;
        }
        /// <summary>
        /// Disconnects from destination
        /// </summary>
        public void Disconnect()
        {
            _connected = false;
            tcpClient?.Close();
            stream?.Close();
            reader?.Close();
            writer?.Close();
        }

        //this is a test line

        public void Dispose()
        {
            Disconnect();
            tcpClient?.Dispose();
            stream?.Dispose();
            reader?.Dispose();
            writer?.Dispose();
        }
    }
}
