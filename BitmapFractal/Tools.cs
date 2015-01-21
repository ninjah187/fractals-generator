using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapFractal
{
    public static class Tools
    {
        public static double Min(double a, double b)
        {
            if (a <= b)
                return a;
            else return b;
        }

        public static double Max(double a, double b)
        {
            if (a >= b)
                return a;
            else return b;
        }
    }
}
