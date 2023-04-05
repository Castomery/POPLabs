using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2Pop
{
    public class ArrClass
    {
        private int _countOfThreads;
        private int[] _arr;

        private object lockerForCount = new object();
        private object lockerForMin = new object();
        private int _threadCount;
        int ArrMinIndex = 0;

        public ArrClass(int countOfThreads, int[] arr)
        {
            _countOfThreads = countOfThreads;
            _arr = arr;
        }

        public int FindMin(int startIndex, int endIndex)
        {
            int min = int.MaxValue;
            int index = 0;

            for (int i = startIndex; i < endIndex; i++)
            {
                if (min > _arr[i])
                {
                    min = _arr[i];
                    index = i;
                }
            }

            return index;
        }

        public int ParallelFindMin()
        {
            
            Thread[] threads = new Thread[_countOfThreads];
            int len = _arr.Length / _countOfThreads;

            for (int i = 0; i < _countOfThreads; i++)
            {
                int startIndex = len * i;
                int endIndex = (i == _countOfThreads - 1) ? _arr.Length : len * (i + 1);
                threads[i] = new Thread(StartThread);
                threads[i].Start(new Border(startIndex, endIndex));
            }

            lock (lockerForCount)
            {
                while (_threadCount < _countOfThreads)
                {
                    Monitor.Wait(lockerForCount);
                }
            }

            return ArrMinIndex;
        }

        private void StartThread(object param)
        {
            if (param is Border)
            {
                int minIndex = FindMin((param as Border).StartIndex,(param as Border).EndIndex);

                lock (lockerForMin)
                {
                    SetMinIndex(minIndex);
                }
             
                IncreaseThreadCount();
            }
        }

        private void SetMinIndex(int minIndex)
        {
            if (_arr[ArrMinIndex] > _arr[minIndex])
            {
                ArrMinIndex = minIndex;
            }
        }

        private void IncreaseThreadCount()
        {
            lock (lockerForCount)
            {
                _threadCount++;
                Monitor.Pulse(lockerForCount);
            }
        }
    }
}
