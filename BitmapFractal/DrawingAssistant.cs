using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace BitmapFractal
{
    class DrawingAssistant
    {
        public static readonly int PARTS = 4;

        private Bitmap bitmap;
        private Thread thread;

        private Point beginWS;
        private Point endWS;

        private IDrawable item;

        public Bitmap Bitmap { get { return bitmap; } }
        public Point BeginWS { get { return beginWS; } }
        public Point EndWS { get { return endWS; } }
        public bool Done { get; private set; }

        public DrawingAssistant(IDrawable item, Point beginWS, Point endWS)
        {
            this.beginWS = beginWS;
            this.endWS = endWS;
            this.item = item;

            bitmap = new Bitmap(ThreadDrawer.BITMAP_WIDTH / 4, ThreadDrawer.BITMAP_HEIGHT / 4);
            thread = new Thread(Draw);

            Done = false;
        }

        public void Go()
        {
            thread.Start();
        }

        private void Draw()
        {
            item.Draw(bitmap);
            Done = true;
        }

    }
}
