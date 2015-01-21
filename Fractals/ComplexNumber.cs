using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals
{
    public class ComplexNumber
    {
        public double Re { get; set; }
        public double Im { get; set; }
        public double Magnitude
        {
            get { return Math.Sqrt((Re * Re) + (Im * Im)); }
        }

        public ComplexNumber()
        {
            Re = 0;
            Im = 0;
        }

        public ComplexNumber(double re, double im)
        {
            Re = re;
            Im = im;
        }

        public static ComplexNumber operator +(ComplexNumber x, ComplexNumber y)
        {
            return new ComplexNumber(x.Re + y.Re, x.Im + y.Im);
        }

        public static ComplexNumber operator -(ComplexNumber x, ComplexNumber y)
        {
            return new ComplexNumber(x.Re - y.Re, x.Im - y.Im);
        }

        public static ComplexNumber operator *(ComplexNumber x, ComplexNumber y)
        {
            //return new ComplexNumber(x.Re * y.Re, x.Im * y.Im);
            return new ComplexNumber(x.Re * y.Re - x.Im * y.Im, x.Re * y.Im + y.Re * x.Im);
        }

        public static ComplexNumber operator /(ComplexNumber x, ComplexNumber y)
        {
            throw new NotImplementedException();
        }
    }
}
