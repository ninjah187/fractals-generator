using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fractals
{
    static class ExtensionMethods
    {
        public static Point ToWindowSpace(this Point p)
        {
            double x = p.X * MandelbrotDrawer.SCALE_UNIT;
            double y = p.Y * MandelbrotDrawer.SCALE_UNIT;
            return new Point((MainWindow.MainCanvas.Width / 2 + x),
                                (MainWindow.MainCanvas.Height / 2 - y));
        }
    }
}
