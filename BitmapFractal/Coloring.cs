using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BitmapFractal
{
    public static class Coloring
    {
        private class Multiplies
        {
            public double R { get; private set; }
            public double G { get; private set; }
            public double B { get; private set; }

            private double rHue;
            private double gHue;
            private double bHue;

            public Multiplies()
            {
                R = random.NextDouble() * 5;
                G = random.NextDouble() * 5;
                B = random.NextDouble() * 5;
            }
        }

        //Obczaj nasycenie i odwracanie barw!!!

        private static readonly Random random = new Random();
        private static Color main;
        private static Multiplies m1;
        private static Multiplies m2;
        private static Multiplies m3;
        private static Multiplies m4;

        public static Color RandomScheme(int n)
        {
            if (n == -1)
                return main;
            if (n >= 0 && n < 50)
                return Color.FromArgb((int)(n * m1.R) % 255, (int)(n * m1.G) % 255, (int)(n * m1.B) % 255);
            if (n >= 50 && n < 100)
                return Color.FromArgb((int)(n * m2.R) % 255, (int)(n * m2.G) % 255, (int)(n * m2.B) % 255);
            if (n >= 100 && n < 150)
                return Color.FromArgb((int)(n * m3.R) % 255, (int)(n * m3.G) % 255, (int)(n * m3.B) % 255);
            return Color.FromArgb((int)(n * m4.R) % 255, (int)(n * m4.G) % 255, (int)(n * m4.B) % 255);
        }

        public static void Randomize()
        {
            main = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
            m1 = new Multiplies();
            m2 = new Multiplies();
            m3 = new Multiplies();
            m4 = new Multiplies();
        }

        public static Color Scheme1(int n)
        {
            if (n == -1)
                return Color.FromArgb(156, 255, 119);
            if(n >= 0 && n < 50)
                return Color.FromArgb(255, (int)(n * 2.5), 0);
            if (n >= 50 && n < 100)
            {
                return Color.FromArgb(0, 255 / (int)(n * 1.25), 255 / (int)(n * 0.75));
                //return Color.FromArgb((int)(n * 0.75), 0, (int)(n * 0.25));
            }
            n -= 100;
            return Color.FromArgb(0, (n * 4) % 255, 0);
        }

        public static Color Scheme2(int n)
        {
            if (n == -1)
            {
                return Color.FromArgb(255, 126, 0);
            }
            if (n >= 0 && n < 50)
                return Color.FromArgb((int)(n * 2), 0, (int)(n * 4));
            if(n >= 50 && n < 100)
                return Color.FromArgb((int)(n * 1.25), 0, 0);
            if (n >= 100 && n < 150)
                return Color.FromArgb((int)(n * 1.25), 0, 0);
            return Color.FromArgb((int)(n * 4) % 255, 0, 0);
        }

    }
}
