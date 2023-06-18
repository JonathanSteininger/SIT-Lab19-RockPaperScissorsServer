using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorNetworkLibrary
{
    public class Player
    {
        private string _displayName;
        private string _userName;

        private int _identifier;
        private DateTime _joinDate;
        private DateTime _lastOnline;

        const string BOT_IDENTIFIER_STRING = "BOT";

        public static int BOT_HASH => BOT_IDENTIFIER_STRING.GetHashCode();

        private bool _isBot;

        public string DisplayName { get { return _displayName; } set { _displayName = value; } }
        public string UserName { get { return _userName; } set { _userName = value; } }
        public DateTime JoinDate { get { return _joinDate; } }
        public DateTime LastOnline { get { return _lastOnline; } set { _lastOnline = value; } }
        public bool IsBot { get { return _isBot; } }

        private StatTracker _stats;
        public StatTracker Stats { get { return _stats; } }

        public Player(string UserName) : this(UserName, UserName) { }
        public Player(string displayName, string userName) : this(displayName, userName, false) { }
        public Player(string displayName, string userName, bool IsBot)

        {
            _displayName = displayName;
            _userName = userName;
            _identifier = IsBot ? "Bot".GetHashCode() : _userName.GetHashCode();

            _isBot = IsBot;

            _stats = new StatTracker();

            _joinDate = DateTime.Now;
            _lastOnline = DateTime.Now;
        }

        public override int GetHashCode()
        {
            return _isBot ? BOT_HASH : $"{_identifier}{_joinDate.ToShortDateString()}".GetHashCode();
        }

        public override string ToString() => $"{_displayName} ({_userName}) | {_stats}";

        public static bool operator ==(Player a, Player b) => a.GetHashCode() == b.GetHashCode();
        public static bool operator !=(Player a, Player b) => a.GetHashCode() != b.GetHashCode();

    }
}
