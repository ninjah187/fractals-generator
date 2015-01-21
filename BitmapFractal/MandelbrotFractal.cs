using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Fractals;

namespace BitmapFractal
{
    public class MandelbrotFractal : Fractal
    {
        public MandelbrotFractal(ColorMethodHandler colorHandler)
            : base(colorHandler)
        {

        }

        // -1 jeżeli jest; n jeżeli nie ma; n - iteracja, w której okazuje się, że nie należy
        protected override int InSet(Point point)
        {
            z.Re = z.Im = z1.Re = z1.Im = 0;
            p.Re = point.X;
            p.Im = point.Y;
            for (int n = 0; n < checkDeep; n++)
            {
                z1 = (z * z) + p;
                if (z1.Magnitude >= 2)
                    return n;
                z = z1;
            }
            return -1;
        }
    }
}
