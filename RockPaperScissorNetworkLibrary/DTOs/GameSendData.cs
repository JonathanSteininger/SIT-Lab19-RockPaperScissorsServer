using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorNetworkLibrary
{
    public class GameSendData : DTO
    {
        public override string Type => nameof(GameSendData);
        public DataSentType DataType { get; set; }
        public object Data { get; set; }

        public GameSendData(DataSentType dataSentType, object data) {
            DataType = dataSentType;
            Data = data; 
        }
    }

    public struct SentGameData
    {
        private bool _singlePlayer;
        public bool IsSinglePlayer => _singlePlayer;

        public SentGameData(bool isSinglePlayer)
        {
            _singlePlayer = isSinglePlayer;
        }
    }
}
