using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BitmapFractal
{
    class CoordinateSystem : IDrawable
    {
        private Axis X;
        private Axis Y;
        private Scale scale;

        private Color color;

        public CoordinateSystem()
        {
            color = Color.Red;

            X = new Axis(Orientation.Horizontal, color);
            Y = new Axis(Orientation.Vertical, color);
            scale = new Scale(color);
        }

        public void Draw(Bitmap bitmap)
        {
            X.Draw(bitmap);
            Y.Draw(bitmap);
            scale.Draw(bitmap);
        }
    }
}
