using System;
using System.Threading;

namespace Lab4Pop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count_of_phylosophers = 5;
            Semaphore[] forks = new Semaphore[count_of_phylosophers];
            for (int i = 0; i < forks.Length; i++)
            {
                forks[i] = new Semaphore(1, 1);
            }

            int count_of_loops = 3;
            int thinkingTime = 1000;
            int eatingTime = 1000;

            for (int i = 0; i < count_of_phylosophers; i++)
            {
                if (i == count_of_phylosophers-1)
                {
                    Philosopher phylosopher = new Philosopher(i, 
                                                              forks[i % (count_of_phylosophers-1)], 
                                                              forks[i],
                                                              count_of_loops, 
                                                              thinkingTime,
                                                              eatingTime);
                    new Thread(phylosopher.Start).Start();
                }
                else
                {
                    Philosopher phylosopher = new Philosopher(i,
                                                              forks[i],
                                                              forks[i+1],
                                                              count_of_loops,
                                                              thinkingTime,
                                                              eatingTime);
                    new Thread(phylosopher.Start).Start();
                }
            }
        }
    }
}
