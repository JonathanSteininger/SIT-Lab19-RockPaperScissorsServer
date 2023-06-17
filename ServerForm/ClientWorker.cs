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

        private Player _player;
        /// <summary>
        /// ClinetWorker Constructor.
        /// </summary>
        /// <param name="client">The active connection to the client</param>
        public ClientWorker(TcpClient client) : base(null, 50)
        {
            _conn = new NetworkConnection(client);
            _player = new Player("Guest");
        }
        /// <summary>
        /// Runs when the thread starts running.
        /// </summary>
        public override void Start()
        {
            _currentThread = Thread.CurrentThread;
        }
        /// <summary>
        /// The update method that runs every loop on its thread.
        /// will run every time this instance recieves an object from the client's network stream
        /// </summary>
        public override void Update()
        {
            object RecivedObject = _conn.ReadAuto();
            try
            {
                if (RecivedObject == null) throw new Exception("recived obejct was null");

                else if (RecivedObject is GameCommand) ResponseGameCommand((GameCommand)RecivedObject);
                else if (RecivedObject is GameRequestData) ResponseGameRequestData((GameRequestData)RecivedObject);
                else if (RecivedObject is GameSendData) ResponseGameSendData((GameSendData)RecivedObject);
                else if (RecivedObject is ServerResponse) ResponseServerResponse((ServerResponse)RecivedObject);

                throw new Exception("Sent Object unknown");
            }catch (Exception ex)
            {
                _conn.Send(new ServerResponse(new ServerErrorMessage("Error with sent object.", ex)));
            }
        }

        //server Responses to object recived
        /// <summary>
        /// Response to a ServerResponse object being sent by the client
        /// </summary>
        /// <param name="recivedObject">The recived object from the client</param>
        private void ResponseServerResponse(ServerResponse recivedObject)
        {
            if (recivedObject.ResponseType == ResponseType.ErrorMessage) return;
            throw new Exception("this Server cant recive Server responses");
        }
        /// <summary>
        /// Response to a GameSendData object being sent by the client
        /// </summary>
        /// <param name="recivedObject">The recived object from the client</param>
        private void ResponseGameSendData(GameSendData recivedObject)
        {
            switch (recivedObject.DataType)
            {
                case DataSentType.PlayerInfo:
                    _player = (Player)recivedObject.Data;
                    break;
                case DataSentType.GameMessage:
                    break;
                case DataSentType.GameMove:
                    break;
                case DataSentType.CreateGame:
                    break;
            }
        }
        /// <summary>
        /// Response to a GameRequestData object being sent by the client
        /// </summary>
        /// <param name="recivedObject">The recived object from the client</param>
        private void ResponseGameRequestData(GameRequestData recivedObject)
        {
            switch (recivedObject.RequestType)
            {
                case RequestType.GetGameInfo:
                    break;
                case RequestType.GetAllGames:
                    break;
            }
        }
        /// <summary>
        /// Response to a GameCommand object being sent by the client
        /// </summary>
        /// <param name="recivedObject">The recived object from the client</param>
        private void ResponseGameCommand(GameCommand recivedObject)
        {
            switch (recivedObject.CommandType)
            {
                case CommandType.QuitGame:
                    break;
                case CommandType.QuitServer:
                    break;
            }
        }

        //other methods


        /// <summary>
        /// Only usable outside of the Update method. 
        /// meant to join this thread to an external one.
        /// 
        /// DONT USE INSIDE ITS OWN THREAD - I dont know what will happen...
        /// </summary>
        public void Join()
        {
            Stop();
            if (_currentThread == null || _currentThread.ThreadState != ThreadState.Running) return;
            _currentThread.Join();
        }

    }
}
