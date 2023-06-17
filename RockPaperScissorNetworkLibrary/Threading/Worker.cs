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
        //common
        public int TickMS { get; set; }

        public bool Completed = false;


        //Stoppers
        private ManualResetEvent _stopper = new ManualResetEvent(true);
        public GroupStopper GroupStopper { get; set; }


        //Constructors
        public Worker(GroupStopper groupStopper, int TickMS)
        {
            GroupStopper = groupStopper;
            this.TickMS = TickMS;
        }
        public Worker() : this(null, 1000) { }


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
                if(TickMS > 0) Thread.Sleep(TickMS);
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
}
