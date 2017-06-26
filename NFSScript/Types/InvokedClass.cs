using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;


using Tick = System.Timers.Timer;

namespace NFSScript.Types
{
    internal class InvokedClass
    {
        private Tick t;
        public uint ID { get; private set; }
        private double interval;
        private Action action;
        public string Name { get; private set; }

        internal InvokedClass(string name, uint id, double interval, Action action)
        {
            ID = id;
            this.interval = interval;
            this.action = action;
            Name = name;
            t = new Tick(interval);
            t.Elapsed += TickCall;
            t.Enabled = false;
        }

        public void InvokeRepeat()
        {
            t.Enabled = true;
            t.Start();
        }

        public void Stop()
        {
            t.Stop();
        }

        private void TickCall(object sender, System.Timers.ElapsedEventArgs e)
        {
            action();
        }
    }
}
