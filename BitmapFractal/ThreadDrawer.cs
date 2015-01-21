using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Threading;

namespace BitmapFractal
{
    class ThreadDrawer
    {
        public const int BITMAP_WIDTH = 2000;
        public const int BITMAP_HEIGHT = 2000;
        public const int SCALE_UNIT = 1000; //100 - 200
        //1 unit = SCALE_UNIT pixels

        private FileStream fileStream;

        DrawingAssistant[] assistants;

        private Bitmap bitmap;

        private Object thisLock = new Object();

        public ThreadDrawer()
        {
            fileStream = new FileStream("./fractal_thread.bmp", FileMode.OpenOrCreate,
                                        FileAccess.Write, FileShare.Write);
            bitmap = new Bitmap(BITMAP_WIDTH, BITMAP_HEIGHT);
            assistants = new DrawingAssistant[DrawingAssistant.PARTS];
        }

        ~ThreadDrawer()
        {
            //Save();
            fileStream.Close();
        }

        private void Initialize()
        {
            int width = 0;
            int height = 0;
            int offsetW = BITMAP_WIDTH / DrawingAssistant.PARTS;
            int offsetH = BITMAP_HEIGHT / DrawingAssistant.PARTS;
            for (int i = 0, k = 0; i < DrawingAssistant.PARTS / 2; i++)
            {
                for (int j = 0; j < DrawingAssistant.PARTS / 2; j++, k++)
                {
                    Point begin = new Point(width, height);
                    Point end = new Point(width + offsetW, height + offsetH);
                    JuliaSetThread jst = new JuliaSetThread(new Fractals.ComplexNumber(-0.10, 0.65), begin, end);
                    assistants[k] = new DrawingAssistant(jst, begin, end);
                    width += offsetW;
                }
                width = 0;
                height += offsetH;
            }
            /*for (int i = 0; i < DrawingAssistant.PARTS; i++)
            {
                Point begin = new Point(width, height);
                Point end = new Point(width + offsetW, height + offsetH);
                JuliaSetThread j = new JuliaSetThread(new Fractals.ComplexNumber(-0.10, 0.65), begin, end);
                assistants[i] = new DrawingAssistant(j, begin, end);
            }*/
        }

        public void Go()
        {
            Clear();
            Initialize();
            for (int i = 0; i < DrawingAssistant.PARTS; i++)
            {
                assistants[i].Go();
            }

            while (!assistants[0].Done && !assistants[1].Done
                    && !assistants[2].Done && !assistants[3].Done) ;

            JoinBitmaps();
            Save();
        }

        /*private void Draw(IDrawable item)
        {
            item.Draw(bitmap);
        }*/

        private void Draw(object item)
        {

        }

        private void JoinBitmaps()
        {
            for (int i = 0; i < DrawingAssistant.PARTS; i++)
            {
                ClonePixels(assistants[i]);
            }
        }

        //ZWIĘKSZ ROZDZIELCZOŚć BITMAPY!!!

        private void ClonePixels(DrawingAssistant da)
        {
            int offsetW = BITMAP_WIDTH / DrawingAssistant.PARTS;
            int offsetH = BITMAP_HEIGHT / DrawingAssistant.PARTS;
            for (int i = 0; i < offsetW; i++)
            {
                for (int j = 0; j < offsetH; j++)
                {
                    lock (thisLock)
                    {
                        Color pixel = da.Bitmap.GetPixel(i, j);
                        bitmap.SetPixel((int)da.BeginWS.X + i, (int)da.BeginWS.Y, pixel);
                    }
                }
            }
        }

        private void Clear()
        {
            for (int x = 0; x < BITMAP_WIDTH; x++)
            {
                for (int y = 0; y < BITMAP_HEIGHT; y++)
                {
                    bitmap.SetPixel(x, y, Color.White);
                }
            }
        }

        private void Save()
        {
            bitmap.Save(fileStream, System.Drawing.Imaging.ImageFormat.Bmp);
        }

    }
}
