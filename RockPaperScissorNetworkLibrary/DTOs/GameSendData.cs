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
        public GameSendData(DataSentType dataSentType, string Message) : this(dataSentType, (object)Message) { }
        public GameSendData(DataSentType dataSentType, GameData GameData) : this(dataSentType, (object)GameData) { }
        public GameSendData(DataSentType dataSentType, Player PlayerInfo) : this(dataSentType, (object)PlayerInfo) { }
    }
}
