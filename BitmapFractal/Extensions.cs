using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapFractal
{
    public static class Extensions
    {
        //Window space - współrzędnie pixela w układzie współrzędnych okna
        //(0, 0) - lewy górny róg; (BITMAP_WEIDTH - 1, BITMAP_HEIGHT - 1) - prawy dolny róg;
        public static Point ToWindowSpace(this Point p)
        {
            double x = p.X * Drawer.SCALE_UNIT;
            double y = p.Y * Drawer.SCALE_UNIT;
            return new Point(Drawer.BITMAP_WIDTH / 2 + x,
                                Drawer.BITMAP_HEIGHT / 2 - y);
        }

        //Real space - współrzędne w kartezjańskim układzie współrzędnych
        //(0, 0) == (BITMAP_WEIDTH / 2, BITMAP_HEIGHT / 2) - początek układu
        public static Point ToRealSpace(this Point p)
        {
            double x = -((double)Drawer.BITMAP_WIDTH / 2 - p.X);//p.X - (double)Drawer.BITMAP_WIDTH / 2;
            double y = ((double)Drawer.BITMAP_HEIGHT / 2 - p.Y);//(double)Drawer.BITMAP_HEIGHT / 2 + p.Y;
            return new Point(x / (double)Drawer.SCALE_UNIT, y / (double)Drawer.SCALE_UNIT);
            /*double x = p.X / Drawer.SCALE_UNIT;
            double y = p.Y / Drawer.SCALE_UNIT;
            return new Point(Drawer.BITMAP_WIDTH / 2 - x,
                                Drawer.BITMAP_HEIGHT / 2 + y);*/
        }
    }
}
