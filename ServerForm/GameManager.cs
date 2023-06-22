using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockPaperScissorNetworkLibrary;
namespace ServerForm
{
    public class GameManager
    {
        private GameData _gameData;
        public GameData Game { get { return _gameData; } }

        private ClientWorker _player1Worker;
        private ClientWorker _player2Worker;

        public GameManager(ClientWorker Creator, bool isSinglePlayer)
        {
            _gameData = new GameData(Creator.Player, isSinglePlayer, new List<string>());
        }

        public void UpdatePlayers()
        {
            UpdatePlayer1();
            UpdatePlayer2();

            _gameData.Player = _player1Worker.Player;
            _gameData.Opponent = _player2Worker.Player;
        }

        private void UpdatePlayer2()
        {
            if (_player2Worker == null) return;
            if (_player2Worker.Player == null) return;
            _gameData.Opponent = _player2Worker.Player;
        }

        private void UpdatePlayer1()
        {
            if (_player1Worker == null) return;
            if (_player1Worker.Player == null) return;
            _gameData.Player = _player1Worker.Player;
        }

        public void AddSecondClient(ClientWorker worker)
        {
            _player2Worker = worker;
            _gameData.Opponent = _player2Worker.Player;
            _gameData.WaitingForPlayer = false;
            _usingBot = false;
        }
        public void CheckResults()
        {
            if (_gameData.IsSinglePlayer) SinglePlayerCheckResult();
            else MultiplayerCheckResult();
        }

        private void MultiplayerCheckResult()
        {
            if (_player1Worker == null || _player2Worker == null) throw new Exception("One of the workers were null");
            _player1Worker.CheckResult();
            _player2Worker?.CheckResult();
            _player1Worker.Player.GameMove = null;
            _player2Worker.Player.GameMove = null;
        }

        private void SinglePlayerCheckResult()
        {
            BotMove();
            _player1Worker.CheckResult();
            _player1Worker.Player.GameMove = null;
            _gameData.Opponent.GameMove = null;
        }

        public void AddBot()
        {
            _gameData.Opponent = new Player("Bot");
            _gameData.WaitingForPlayer = false;
            _usingBot = true;
        }
        private bool _usingBot = false;
        public void BotMove()
        {
            _gameData.Opponent.GameMove = BotActions.GetRandomChoice();
        }
        public void AddMessage(Player FromPlayer, string Message)
        {
            _gameData.OutPutHistory.Add($"{FromPlayer.DisplayName}: {Message}");
        }
        /// <summary>
        /// Used to quickly format a serverResponse containing the game information.
        /// used to send gameInfo to connected clients.
        /// </summary>
        /// <returns></returns>
        public ServerResponse PackageGameData()
        {
            return new ServerResponse(ResponseType.GameInfo, _gameData);
        }

        internal Player FindOpponent(Player player)
        {
            if (_gameData.Player == null) return null;
            if (_gameData.Player == player) return _gameData.Opponent;
            else return _gameData.Player;
        }
    }
}
