using System;
using System.Threading;

namespace Lab2Pop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int dim = 1_000_000_000;
            int countOfThreads = 2;
            int[] arr = new int[dim];

            for (int i = 0; i < dim; i++)
            {
                arr[i] = i;
            }

            Random rnd = new Random();
            arr[rnd.Next(dim)] *= -1;

            ArrClass arrClass = new ArrClass(countOfThreads, arr);
            int commonMinIndex = arrClass.FindMin(0, arr.Length);
            int parallelMinIndex = arrClass.ParallelFindMin();

            Console.WriteLine("Common");
            Console.WriteLine(commonMinIndex + " " + arr[commonMinIndex]);
            Thread.Sleep(1000);
            Console.WriteLine("Parallel");
            Console.WriteLine(parallelMinIndex + " " + arr[parallelMinIndex]);
        }
    }
}
