using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LAB1_pop
{
    public class Stopper
    {
        private Calculator[] _calculators;

        public Stopper(Calculator[] calculators)
        {
            _calculators = calculators;

            Array.Sort(_calculators);
        }

        public void Stop()
        {
            int currentWaitedTime = 0;

            for (int i = 0; i < _calculators.Length; i++)
            {
                int waitingTime = _calculators[i].WorkTime - currentWaitedTime;
                Thread.Sleep(waitingTime);
                currentWaitedTime += waitingTime;
                _calculators[i].Stop();
            }
        }
    }
}
