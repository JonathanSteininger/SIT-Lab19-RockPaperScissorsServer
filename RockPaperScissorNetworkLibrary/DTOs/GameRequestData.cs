using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorNetworkLibrary
{
    public class GameRequestData : DTO
    {
        public override string Type => nameof(GameRequestData);
        public RequestType RequestType { get; set; }

        public GameRequestData(RequestType requestType)
        {
            RequestType = requestType;
        }
    }
}
