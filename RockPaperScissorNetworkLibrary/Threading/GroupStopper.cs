using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RockPaperScissorNetworkLibrary.Threading
{
    public class GroupStopper
    {
        private ManualResetEvent _stopper;
        public GroupStopper()
        {
            _stopper = new ManualResetEvent(true);
        }
        public virtual void Pause()
        {
            _stopper.Reset();
        }
        public virtual void Resume()
        {
            _stopper.Set();
        }
        public void WaitOne() => _stopper.WaitOne();
    }
}
