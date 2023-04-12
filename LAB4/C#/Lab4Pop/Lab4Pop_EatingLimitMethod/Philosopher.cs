using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab4Pop_EatingLimitMethod
{
    internal class Philosopher
    {
        private int _id;
        private Semaphore _left_fork;
        private Semaphore _right_fork;
        private Semaphore _eatingLimit;
        private int _count_of_loops;
        private int _time_to_eat;
        private int _time_to_think;

        public Philosopher(int id, Semaphore left_fork, Semaphore right_fork, int count_of_loops, int time_to_eat, int time_to_think, Semaphore eatingLimit)
        {
            _id = id;
            _left_fork = left_fork;
            _right_fork = right_fork;
            _count_of_loops = count_of_loops;
            _time_to_eat = time_to_eat;
            _time_to_think = time_to_think;
            _eatingLimit = eatingLimit;
        }

        public void Start()
        {
            for (int i = 0; i < _count_of_loops; i++)
            {
                Console.WriteLine($"Philosopher {_id} thinking {i} time");
                Thread.Sleep(_time_to_think);

                _eatingLimit.WaitOne();

                _left_fork.WaitOne();
                Console.WriteLine($"Philosopher {_id} take left fork");
                _right_fork.WaitOne();
                Console.WriteLine($"Philosopher {_id} take right fork");

                Console.WriteLine($"Philosopher {_id} eating {i} time");
                Thread.Sleep(_time_to_eat);    

                _right_fork.Release();
                Console.WriteLine($"Philosopher {_id} put right fork");
                _left_fork.Release();
                Console.WriteLine($"Philosopher {_id} put left fork");

                _eatingLimit.Release();
            }
        }
    }
}
