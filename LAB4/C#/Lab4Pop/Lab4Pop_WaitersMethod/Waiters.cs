using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab4Pop_WaitersMethod
{
    internal class Waiters
    {
        private Semaphore _waiters;
        private bool[] _canServe;
        private int _countOfPhilosophers;

        public Waiters(int countOfWaiters,int countOfPhilosophers)
        {
            _waiters = new Semaphore(countOfWaiters,countOfWaiters);
            _canServe = new bool[countOfPhilosophers].Select(i => true).ToArray();
            _countOfPhilosophers = countOfPhilosophers;
        }

        public bool CallWaiter(int philosopherId)
        {
            _waiters.WaitOne();
            if (_canServe[(_countOfPhilosophers + philosopherId - 1) % _countOfPhilosophers] &&
                _canServe[(_countOfPhilosophers + philosopherId + 1) % _countOfPhilosophers])
            {
                _canServe[philosopherId] = false;
                return true;
            }
            _waiters.Release();
            return false;
        }

        public void ReleseWaiter(int philosopherId)
        {
            _waiters.Release();
            _canServe[philosopherId] = true;
        }
    }
}
