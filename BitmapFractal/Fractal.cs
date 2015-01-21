using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Fractals;

namespace BitmapFractal
{
    public delegate Color ColorMethodHandler(int n);

    public abstract class Fractal : IDrawable
    {
        protected ComplexNumber z; // Zn
        protected ComplexNumber z1; // Zn+1
        protected ComplexNumber p; // rozpatrywany punkt należący do C

        protected readonly int checkDeep = 200;
        protected readonly double iteration = 1 / (double)Drawer.SCALE_UNIT;
        // iteration - wartość potrzebna przy (iterowaniu) sprawdzaniu pixeli, tak aby sprawdzić każdego

        protected ColorMethodHandler colorHandler;

        public ColorMethodHandler ColorScheme { get { return colorHandler; } }

        public Fractal(ColorMethodHandler colorHandler)
        {
            z = new ComplexNumber();
            z1 = new ComplexNumber();
            p = new ComplexNumber();

            this.colorHandler = colorHandler;
        }

        public virtual void Draw(Bitmap bitmap)
        {
            Point leftTopCorner = new Point(0, 0);
            Point rightBottomCorner = new Point(Drawer.BITMAP_WIDTH - 1, Drawer.BITMAP_HEIGHT - 1);
            Point beginRS = leftTopCorner.ToRealSpace();
            Point endRS = rightBottomCorner.ToRealSpace();

            Point begin = new Point(Tools.Min(beginRS.X, endRS.X),
                                    Tools.Min(beginRS.Y, endRS.Y));
            Point end = new Point(Tools.Max(beginRS.X, endRS.X),
                                    Tools.Max(beginRS.Y, endRS.Y));

            Console.WriteLine(begin.X + " " + begin.Y);
            Console.WriteLine(end.X + " " + end.Y);

            for (double x = begin.X; x <= end.X; x += iteration)
            //for (double x = -2.5; x <= 1; x += iteration)
            {
                for (double y = begin.Y; y <= end.Y; y += iteration)
                //for (double y = -1; y <= 1; y += iteration)
                {
                    Point p = new Point(x, y);
                    int n = InSet(p);
                    DrawPoint(bitmap, p, n);

                    Drawer.Steps++;
                }
            }
        }

        protected abstract int InSet(Point point);

        protected void DrawPoint(Bitmap bitmap, Point p, int n)
        {
            Point ws = p.ToWindowSpace();
            bitmap.SetPixel((int)ws.X, (int)ws.Y, colorHandler(n));
        }
    }
}
