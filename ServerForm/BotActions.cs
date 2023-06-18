using RockPaperScissorNetworkLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForm
{
    public static class BotActions
    {
        private static Random random = new Random();
        public static RoundChoice GetRandomChoice()
        {
            return (RoundChoice)random.Next(3);
        }
    }
}
