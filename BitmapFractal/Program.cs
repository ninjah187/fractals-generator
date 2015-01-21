using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace BitmapFractal
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //Drawer d = new Drawer();
            //d.Fractal = new MandelbrotFractal(Coloring.Scheme1);
            //d.SavePath = "./fractal2.bmp";
            //d.Go();
            ThreadDrawer td = new ThreadDrawer();
            td.Go();
            sw.Stop();
            Console.WriteLine("Time elapsed: " + sw.ElapsedMilliseconds / (double)1000 + " s");

            Console.ReadKey();
        }
    }
}
