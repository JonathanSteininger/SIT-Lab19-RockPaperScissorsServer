using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorNetworkLibrary
{
    public class ServerErrorMessage
    {
        public string Message { get; set; }
        public Exception InnerException { get; set; }

        public ServerErrorMessage(string message, Exception exception)
        {
            InnerException = exception;
            Message = message;
        }
    }
}
