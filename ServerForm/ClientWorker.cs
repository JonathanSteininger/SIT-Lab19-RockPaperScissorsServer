﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RockPaperScissorNetworkLibrary;

namespace ServerForm
{
    public class ClientWorker : Worker, IDisposable
    {
        private NetworkConnection _conn;

        private Thread _currentThread;

        private Player _player;

        private ClientThreadManager _parentManager;
        public Player Player { get { return _player; } }
        public GameManager GameManager { get; set; }


        /// <summary>
        /// ClinetWorker Constructor.
        /// </summary>
        /// <param name="client">The active connection to the client</param>
        public ClientWorker(TcpClient client, ClientThreadManager parentManager) : base(null, 50)
        {
            _conn = new NetworkConnection(client);
            _player = new Player("Guest");
            _parentManager = parentManager;
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
            if (recivedObject.ResponseType == ResponseType.ErrorMessage)
            {
                return;
            }
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
                    GameManager.AddMessage(_player, recivedObject.Data as string);
                    break;
                case DataSentType.GameMove:
                    _player.GameMove = recivedObject.Data as RoundChoice?;
                    GameManager.CheckResults();
                    break;
                case DataSentType.CreateGame:
                    GameManager = new GameManager(this, ((SentGameData)recivedObject.Data).IsSinglePlayer);
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
                    _conn.Send(new ServerResponse(ResponseType.GameInfo, GameManager.Game));
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

        public void CheckResult()
        {
            GameResult result;
            Player p2 = GameManager.FindOpponent(_player);
            if (_player.GameMove == p2.GameMove) result = GameResult.Draw;
            else result = WinOrLose((RoundChoice)p2.GameMove);
            //send object
            _conn.Send(new ServerResponse(ResponseType.RoundResult, result));
        }

        private GameResult WinOrLose(RoundChoice choice)
        {
            switch(choice)
            {
                case RoundChoice.Rock:
                    return _player.GameMove == RoundChoice.Paper ? GameResult.Win : GameResult.Lose;
                case RoundChoice.Paper:
                    return _player.GameMove == RoundChoice.Scissors ? GameResult.Win : GameResult.Lose;
                case RoundChoice.Scissors:
                    return _player.GameMove == RoundChoice.Rock ? GameResult.Win : GameResult.Lose;
                default: throw new Exception("What? this should not run at all ever. I dont know how you managed to throw this exception. uhh... RoundChoice was not Rock, Paper, or Scissors");
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

        public void Dispose()
        {
            Stop();
            _conn.Dispose();
            _player = null;
            GroupStopper = null;
            _currentThread = null;
        }
    }
}
