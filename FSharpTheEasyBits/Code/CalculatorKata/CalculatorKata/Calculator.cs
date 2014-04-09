using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorKata
{
    /// <summary>
    /// Kata taken from http://osherove.com/tdd-kata-1/
    /// </summary>
    public class Calculator
    {
        public int Add(int number)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException("number");
            return number;
        }

        public int Add(int x, int y)
        {
            return x + y;
        }

        public int Add(int x, int y, int z)
        {
            return x + y + z;
        }
    }
}
