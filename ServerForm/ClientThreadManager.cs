using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RockPaperScissorNetworkLibrary;

namespace ServerForm
{
    public class ClientThreadManager
    {
        public List<ClientWorker> Workers;
        public GroupStopper Stopper = new GroupStopper();

        public ClientThreadManager()
        {
            Workers = new List<ClientWorker>();
        }

        public void Add(ClientWorker Worker)
        {
            Worker.GroupStopper = Stopper;
            Workers.Add(Worker);
        }

    }
}
