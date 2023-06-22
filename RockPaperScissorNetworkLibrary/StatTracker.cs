using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorNetworkLibrary
{
    public class StatTracker
    {
        private int _totalGames;

        private Dictionary<GameResult, int> _totals;

        private Dictionary<int, Player> _players;

        public int Wins => _totals[GameResult.Win];
        public int Losses => _totals[GameResult.Lose];
        public int Draws => _totals[GameResult.Draw];
        public int TotalGames { get { return _totalGames; } }
        public int PlayersPlayedCount { get { return _players.Count; } }
        public Player[] PlayersPlayed { get { return _players.Values.ToArray(); } }

        private List<HashPlayerResult> _playerHistory;

        public float WinRate => Wins / ((TotalGames <= 0) ? 1 : TotalGames);
        public float GamesPerPlayerAvg => TotalGames / ((PlayersPlayedCount <= 0) ? 1 : PlayersPlayedCount);


        public int BotsPlayed => _playerHistory.CountPlayer(Player.BOT_HASH);

        public int GamesCountWithPlayer(Player player) => _playerHistory.CountPlayer(player);

        public StatTracker()
        {
            _totalGames = 0;
            _totals = new Dictionary<GameResult, int>()
            {
                {GameResult.Win, 0 },
                {GameResult.Draw, 0 },
                {GameResult.Lose, 0 }
            };

            _playerHistory = new List<HashPlayerResult>();
            _players = new Dictionary<int, Player>();
        }

        /// <summary>
        /// Adds a game to the history, updates the players you have ever played, and adds the restult to the coresponding counter.
        /// </summary>
        /// <param name="result">the games result</param>
        /// <param name="opponent">the oponent player from the game</param>
        public void AddGame(GameResult result, Player opponent)
        {
            UpdatePlayer(opponent);
            _playerHistory.Add(new HashPlayerResult(opponent.GetHashCode(), result));
            _totals[result]++;
            _totalGames++;
        }

        public void UpdatePlayer(Player opponent)
        {
            int localHash = opponent.GetHashCode();
            if (!_players.ContainsKey(localHash)) _players.Add(localHash, opponent);
            else _players[localHash] = opponent;
        }

        public override string ToString() => $"Wins: {Wins}, Losses: {Losses}, Draws: {Draws}.";


    }
    internal struct HashPlayerResult
    {
        public int PlayerHash { get; set; }
        public GameResult Result { get; set; }
        public HashPlayerResult(int hash, GameResult result)
        {
            PlayerHash = hash;
            Result = result;
        }
    }

    internal static class PlayerHistoryListExtension
    {
        public static int CountPlayer(this List<HashPlayerResult> item, Player player)
        {
            int count = 0;
            int localHash = player.GetHashCode();
            for(int i = 0; i < item.Count; i++)
            {
                if (localHash == item[i].PlayerHash) count++;
            }
            return count;
        }
        public static int CountPlayer(this List<HashPlayerResult> item, int Hash)
        {
            int count = 0;
            for (int i = 0; i < item.Count; i++)
            {
                if (Hash == item[i].PlayerHash) count++;
            }
            return count;
        }
    }
    
}
