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

        public GameManager(Player Creator, bool isSinglePlayer)
        {
            _gameData = new GameData(Creator, isSinglePlayer, new List<string>());
        }
        public void AddSecondPlayer(Player player2)
        {
            _gameData.Opponent = player2;
            _gameData.WaitingForPlayer = false;
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

    }
}
