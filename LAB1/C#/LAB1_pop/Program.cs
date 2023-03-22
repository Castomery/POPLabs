using System;
using System.Threading;

namespace LAB1_pop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator[] calculators = { new Calculator(3,2000), new Calculator(4,5000), new Calculator(5,10000)};
            Thread[] threads = new Thread[calculators.Length];

            Stopper stopper = new Stopper(calculators);

            for (int i = 0; i < calculators.Length; i++)
            {
                threads[i] = new Thread(calculators[i].Calculate);
                threads[i].Start();
            }
            new Thread(stopper.Stop).Start();
        }
    }
}
