using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorNetworkLibrary
{
    public class GameCommand : DTO
    {
        public override string Type => nameof(GameCommand);
        public CommandType CommandType { get; set; }

        public GameCommand(CommandType commandType)
        {
            CommandType = commandType;
        }
    }
}
