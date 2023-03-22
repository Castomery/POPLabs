using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB1_pop
{
    public class Calculator : IComparable<Calculator>
    {
        private Guid _id;
        private long _step;
        public int WorkTime { get; set; }
        public bool CanStop { get; set; }
        public Calculator(long step, int workTime)
        {
            _id = Guid.NewGuid();
            _step = step;
            CanStop = false;
            WorkTime = workTime;
        }

        public void Stop()
        {
            CanStop = true;
        }

        public void Calculate()
        {
            long sum = 0;
            long countOfSteps = 0;

            do
            {
                sum += _step;
                countOfSteps++;
            } while (!CanStop);

            Console.WriteLine($"id: {_id} sum: {sum} countOfSteps: {countOfSteps}");
        }
        public int CompareTo(Calculator other)
        {
            return WorkTime.CompareTo(other.WorkTime);
        }
    }
}
