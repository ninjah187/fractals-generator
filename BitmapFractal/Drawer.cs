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
    //http://msdn.microsoft.com/en-us/library/aa664471%28v=vs.71%29.aspx - różnice między klasą, a strukturą
    // klasy - przekazywane przez referencję, struktury - przez kopię (wartość)

    /*public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
            : this()
        {
            X = x;
            Y = y;
        }
    }*/
    //Lepiej żeby Point był strukturą czy klasą?

    public class Drawer
    {
        public const int BITMAP_WIDTH = 2000;
        public const int BITMAP_HEIGHT = 2000;
        public const int SCALE_UNIT = 1000; //100 - 200
        //1 unit = SCALE_UNIT pixels
        public static int Steps = 0;

        private string savePath;
        private FileStream fileStream;

        private Bitmap bitmap;

        public Bitmap Bitmap { get { return bitmap; } }
        public Fractal Fractal { get; set; }
        public string SavePath
        {
            get { return savePath; }
            set
            {
                savePath = value;
            }
        }

        public Drawer()
        {
            bitmap = new Bitmap(BITMAP_WIDTH, BITMAP_HEIGHT);
        }

        public Drawer(string path)
            : this(path, null)
        {
            
        }

        public Drawer(string path, Fractal fractal)
        {
            SavePath = path;
            bitmap = new Bitmap(BITMAP_WIDTH, BITMAP_HEIGHT);
            Fractal = fractal;
        }

        ~Drawer()
        {
            if(fileStream != null)
                fileStream.Close();
        }

        public void Go()
        {
            Clear();
            Draw(Fractal);
            //Save();
            //fileStream.Close();
        }

        private void Draw(IDrawable item)
        {
            item.Draw(bitmap);
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

        public void Save()
        {
            if (fileStream == null)
            {
                fileStream = new FileStream(savePath, FileMode.OpenOrCreate,
                                            FileAccess.Write, FileShare.Write);
            }
            bitmap.Save(fileStream, System.Drawing.Imaging.ImageFormat.Bmp);
            fileStream.Close();
        }

        //JuliaSetFractal js = new JuliaSetFractal(new Fractals.ComplexNumber(0.4, 0.3));
        //JuliaSetFractal js = new JuliaSetFractal(new Fractals.ComplexNumber(-0.10, 0.65), Coloring.Scheme1);
        //JuliaSetFractal js = new JuliaSetFractal(new Fractals.ComplexNumber(-0.67319, 0.34442));
        //JuliaSetFractal js = new JuliaSetFractal(new Fractals.ComplexNumber(-0.74434, -0.10772));

        //scheme1 1000
        //JuliaSetFractal js = new JuliaSetFractal(new Fractals.ComplexNumber(0.233, 0.53780));
    }
}
