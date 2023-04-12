using System;
using System.Threading;

namespace Lab4Pop_WaitersMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count_of_philosophers = 5;
            int count_of_waiters = 2;
            Waiters waiters = new Waiters(count_of_waiters, count_of_philosophers);
            Semaphore[] forks = new Semaphore[count_of_philosophers];
            for (int i = 0; i < forks.Length; i++)
            {
                forks[i] = new Semaphore(1, 1);
            }

            int count_of_loops = 3;
            int thinkingTime = 1000;
            int eatingTime = 1000;

            for (int i = 0; i < count_of_philosophers; i++)
            {
                Philosopher philosopher = new Philosopher(i,
                                                               forks[i],
                                                               forks[(i + 1) % count_of_philosophers],
                                                               count_of_loops,
                                                               thinkingTime,
                                                               eatingTime,
                                                               waiters);
                new Thread(philosopher.Start).Start();
            }
        }
    }
}
