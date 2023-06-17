using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorNetworkLibrary
{
    public class ServerResponse : DTO
    {
        public override string Type => nameof(ServerResponse);
        public ResponseType ResponseType { get; set; }
        public object Data { get; set; }

        public ServerResponse(ServerErrorMessage ErrorMessage)
        {
            Data = ErrorMessage;
            ResponseType = ResponseType.ErrorMessage;
        }
        public ServerResponse(ResponseType responseType, object data)
        {
            ResponseType = responseType;
            Data = data;
        }
    }
}
