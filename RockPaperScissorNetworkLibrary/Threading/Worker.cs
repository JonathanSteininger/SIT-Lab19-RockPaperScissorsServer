using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RockPaperScissorNetworkLibrary
{
    public abstract class Worker
    {
        public int TickMS {  get; set; }

        private ManualResetEvent _stopper = new ManualResetEvent(true);

        public bool Completed = false;

        public GroupStopper GroupStopper { get; set; }
        public Worker(GroupStopper groupStopper)
        {
            GroupStopper = groupStopper;
        }
        public Worker()
        {
            GroupStopper = null;
        }
        public abstract void Update();
        public abstract void Start();

        public void Run()
        {
            Start();
            while (!Completed)
            {
                GroupStopper?.WaitOne();
                _stopper.WaitOne();
                Update();
                if(TickMS > 0)Thread.Sleep(TickMS);
            }
        }

        public virtual void Pause()
        {
            _stopper.Reset();
        }
        public virtual void Resume()
        {
            _stopper.Set();
        }
        public virtual void Stop()
        {
            Completed = true;
            _stopper.Set();
        }
    }

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
