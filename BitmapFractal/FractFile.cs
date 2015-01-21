using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BitmapFractal
{
    public class FractFile
    {
        private FileStream fileStream;

        private string fractalType = "";
        private string colorScheme = "";
        private string re = "";
        private string im = "";

        private string savePath;

        public string SavePath
        {
            get { return savePath; }
            set { savePath = value; }
        }

        private FractFile()
        {

        }

        public FractFile(Fractal fractal)
        {
            Type type = fractal.GetType();
            if (type == typeof(MandelbrotFractal))
                fractalType = "mandelbrot";
            if (type == typeof(JuliaSetFractal))
            {
                fractalType = "julia";
                JuliaSetFractal frac = (JuliaSetFractal)fractal;
                re = frac.Re.ToString();
                im = frac.Im.ToString();
            }

            if (fractal.ColorScheme == Coloring.Scheme1)
                colorScheme = "scheme1";
            if (fractal.ColorScheme == Coloring.Scheme2)
                colorScheme = "scheme2";
            if (fractal.ColorScheme == Coloring.RandomScheme)
                colorScheme = "random";
        }

        /*public void Foo(int n = 1)
        {

        }*/

        public void Save()
        {
            fileStream = new FileStream(savePath, FileMode.OpenOrCreate,
                                        FileAccess.Write, FileShare.Write);
            StreamWriter sw = new StreamWriter(fileStream);
            sw.WriteLine(fractalType);
            sw.WriteLine(colorScheme);
            if (fractalType == "julia")
            {
                sw.WriteLine(re);
                sw.WriteLine(im);
            }
            sw.Close();
            fileStream.Close();
        }

        public void Open(string path)
        {
            fileStream = new FileStream(path, FileMode.Open,
                                        FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fileStream);

            fractalType = sr.ReadLine();

            sr.Close();
            fileStream.Close();
        }
    }
}
