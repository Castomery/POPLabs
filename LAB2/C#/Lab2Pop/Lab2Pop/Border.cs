using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Pop
{
    public class Border
    {
        public int StartIndex { get; private set; }
        public int EndIndex { get; private set; }

        public Border(int startIndex, int endIndex)
        {
            StartIndex = startIndex;
            EndIndex = endIndex;
        }
    }
}
