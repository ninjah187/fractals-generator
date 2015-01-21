using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Fractals;

namespace BitmapFractal
{
    //http://pl.wikipedia.org/wiki/Zbi%C3%B3r_Julii

    //JuliaSetFractal może dziedziczyć po MandelbrotFractal

    public class JuliaSetFractal : Fractal
    {
        private ComplexNumber c; // parametr zbioru

        public double Re { get { return c.Re; } }
        public double Im { get { return c.Im; } }

        public JuliaSetFractal(ComplexNumber c, ColorMethodHandler colorHandler)
            : base(colorHandler)
        {
            this.c = c;
        }

        protected override int InSet(Point point)
        {
            z1.Re = z1.Im = 0;
            p.Re = point.X;
            p.Im = point.Y;
            z = p;
            for (int n = 0; n < checkDeep; n++)
            {
                z1 = (z * z) + c;
                if (z1.Magnitude >= 2)
                    return n;
                z = z1;
            }
            return -1;
        }
    }
}
