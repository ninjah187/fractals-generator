using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Fractals;

namespace BitmapFractal
{
    class JuliaSetThread : Fractal
    {
        private ComplexNumber c; // parametr zbioru

        private Point beginWS;
        private Point endWS;

        int offsetW = Drawer.BITMAP_WIDTH / DrawingAssistant.PARTS;
        int offsetH = Drawer.BITMAP_HEIGHT / DrawingAssistant.PARTS;

        private Object thisLock = new Object();

        public JuliaSetThread(ComplexNumber c, Point beginWS, Point endWS)
            : base(Coloring.Scheme2)
        {
            this.c = c;
            this.beginWS = beginWS;
            this.endWS = endWS;
        }

        public override void Draw(Bitmap bitmap)
        {
            Point beginRS = beginWS.ToRealSpace();
            Point endRS = endWS.ToRealSpace();

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
                    Point p2 = p.ToWindowSpace();
                    p2.X -= beginWS.X;
                    p2.Y -= beginWS.Y;
                    DrawPoint(bitmap, p2.ToRealSpace(), n);
                }
            }
        }

        protected override int InSet(Point point)
        {
            ResetVars();
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

        private void ResetVars()
        {
            z1.Re = z1.Im = 0;
        }

    }
}
