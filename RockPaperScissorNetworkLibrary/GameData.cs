using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorNetworkLibrary
{
    public struct GameData : IComparable<GameData>
    {
        private Player _mainPlayer;
        private Player _opponentPlayer;

        public Player Player { get { return _mainPlayer; } set { _mainPlayer = value; } }
        public Player Opponent { get { return _opponentPlayer; } set { _opponentPlayer = value; } }
        public bool IsSinglePlayer { get; set; }

        public bool WaitingForPlayer { get; set; }

        public List<string> OutPutHistory { get; set; }

        public RoundChoice? Player1Move { get; set; }
        public RoundChoice? Player2Move { get; set; }

        public GameData(Player mainPlayer, Player opponentPlayer, bool isSinglePlayer, List<string> History)
        {
            OutPutHistory = History;
            //players
            _mainPlayer = mainPlayer;
            _opponentPlayer = opponentPlayer;
            //bools
            IsSinglePlayer = isSinglePlayer;
            WaitingForPlayer = !isSinglePlayer;
            Player1Move = null;
            Player2Move = null;
        }
        public GameData(Player mainPlayer, bool IsSinglePlayer, List<string> History) : this(mainPlayer, null, IsSinglePlayer, History) { }

        public override string ToString()
        {
            return $"Player1: {_mainPlayer?.ToString() ?? "None"}\nPlayer2: {_opponentPlayer?.ToString() ?? "None"}";
        }

        public int CompareTo(GameData other)
        {
            if(other.WaitingForPlayer == WaitingForPlayer)
            {
                return other.Player.DisplayName.CompareTo(Player.DisplayName);
            }
            else return other.WaitingForPlayer ? -1 : 1 ;
            
        }
    }
}
