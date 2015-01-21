using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BitmapFractal
{
    public class DrawablePoint : Point, IDrawable
    {
        public DrawablePoint()
            : base()
        {

        }

        public DrawablePoint(double x, double y)
            : base(x, y)
        {

        }

        public DrawablePoint(Point p)
            : base(p.X, p.Y)
        {

        }

        public void Draw(Bitmap bitmap)
        {
            DrawablePoint dpWinSpace = new DrawablePoint(this.ToWindowSpace());
            bitmap.SetPixel((int)dpWinSpace.X, (int)dpWinSpace.Y, Color.Black);
        }
    }
}
