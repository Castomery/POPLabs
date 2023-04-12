using System;
using System.Threading;

namespace Lab4Pop_EatingLimitMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count_of_philosophers = 5;
            Semaphore eatingLimit = new Semaphore(count_of_philosophers-1,count_of_philosophers-1);
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
                                                               eatingLimit);
                new Thread(philosopher.Start).Start();
            }
        }
    }
}
