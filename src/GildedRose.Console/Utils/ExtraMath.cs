using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console.Utils
{
    public static class ExtraMath
    {
        public static int Clamp(int min, int val, int max)
        {
            val = Math.Min(val, max);
            val = Math.Max(min, val);
            return val;
        }
    }
}
