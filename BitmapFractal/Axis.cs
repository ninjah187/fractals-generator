using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BitmapFractal
{
    public enum Orientation
    {
        Horizontal,
        Vertical
    }

    class Axis : IDrawable
    {
        private Orientation orientation;
        private Color color;

        public Axis(Orientation orientation)
            : this(orientation, Color.Black)
        {

        }

        public Axis(Orientation orientation, Color color)
        {
            this.orientation = orientation;
            this.color = color;
        }

        public void Draw(Bitmap bitmap)
        {
            if (orientation == Orientation.Horizontal)
            {
                int y = Drawer.BITMAP_HEIGHT / 2;
                for (int i = 0; i < Drawer.BITMAP_WIDTH; i++)
                {
                    bitmap.SetPixel(i, y, color);
                }
            }
            else
            {
                int x = Drawer.BITMAP_WIDTH / 2;
                for (int i = 0; i < Drawer.BITMAP_HEIGHT; i++)
                {
                    bitmap.SetPixel(x, i, color);
                }
            }
        }

    }
}
