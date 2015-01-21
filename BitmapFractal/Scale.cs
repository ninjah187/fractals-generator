using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BitmapFractal
{
    class Scale : IDrawable
    {
        private int size;
        private Color color;
        private Point center;

        public Scale()
            : this(Color.Black)
        {
            
        }

        public Scale(Color color)
        {
            size = 10;
            center = new Point(0, 0).ToWindowSpace();
            this.color = color;
        }

        public void Draw(Bitmap bitmap)
        {
            // na osi Y od środka w dół
            for (double i = center.Y + Drawer.SCALE_UNIT; i < Drawer.BITMAP_HEIGHT; i += Drawer.SCALE_UNIT)
            {
                DrawHorizontal(bitmap, (int)i);
            }
            // na osi Y od środka w górę
            for (double i = center.Y - Drawer.SCALE_UNIT; i > 0; i -= Drawer.SCALE_UNIT)
            {
                DrawHorizontal(bitmap, (int)i);
            }
            // na osi X od środka w prawo
            for (double i = center.X + Drawer.SCALE_UNIT; i < Drawer.BITMAP_WIDTH; i += Drawer.SCALE_UNIT)
            {
                DrawVertical(bitmap, (int)i);
            }
            // na osi X od środka w lewo
            for (double i = center.X - Drawer.SCALE_UNIT; i > 0; i -= Drawer.SCALE_UNIT)
            {
                DrawVertical(bitmap, (int)i);
            }
        }

        // draws horizontal line which has length of size at specific y height
        private void DrawHorizontal(Bitmap bitmap, int y)
        {
            for (int x = (int)center.X - size / 2; x <= (int)center.X + size / 2; x++)
            {
                bitmap.SetPixel(x, y, color);
            }
        }

        private void DrawVertical(Bitmap bitmap, int x)
        {
            for (int y = (int)center.Y - size / 2; y <= (int)center.Y + size / 2; y++)
            {
                bitmap.SetPixel(x, y, color);
            }
        }

    }
}
