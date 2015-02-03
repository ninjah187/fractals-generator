using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Fractals;

namespace BitmapFractal
{
    public class FractFile
    {
        private FileStream fileStream;

        public string FractalType { get; private set; }
        public string ColorScheme { get; private set; }
        public string Re { get; set; }
        public string Im { get; set; }
        private Fractal fractal;

        private string savePath;

        public string SavePath
        {
            get { return savePath; }
            set { savePath = value; }
        }

        public Fractal Fractal
        {
            get { return fractal; }
        }

        private FractFile()
        {

        }

        public FractFile(Fractal fractal)
        {
            Type type = fractal.GetType();
            if (type == typeof(MandelbrotFractal))
                FractalType = "Mandelbrot Set";
            if (type == typeof(JuliaSetFractal))
            {
                FractalType = "Julia Set";
                JuliaSetFractal frac = (JuliaSetFractal)fractal;
                Re = frac.Re.ToString();
                Im = frac.Im.ToString();
            }

            if (fractal.ColorScheme == Coloring.Scheme1)
                ColorScheme = "Scheme 1";
            if (fractal.ColorScheme == Coloring.Scheme2)
                ColorScheme = "Scheme 2";
            if (fractal.ColorScheme == Coloring.RandomScheme)
                ColorScheme = "Random";

            this.fractal = fractal;
        }

        public FractFile(string path)
        {
            Open(path);

            ColorMethodHandler colorHandler = null;

            switch (ColorScheme)
            {
                case "Scheme 1":
                    colorHandler = Coloring.Scheme1;
                    break;

                case "Scheme 2":
                    colorHandler = Coloring.Scheme2;
                    break;

                case "Random":
                    colorHandler = Coloring.RandomScheme;
                    break;

                default:
                    throw new InvalidOperationException();
                    break;
            }

            switch (FractalType)
            {
                case "Mandelbrot Set":
                    fractal = new MandelbrotFractal(colorHandler);
                    break;

                case "Julia Set":
                    ComplexNumber c = new ComplexNumber(
                        double.Parse(Re),
                        double.Parse(Im)
                        );
                    fractal = new JuliaSetFractal(c, colorHandler);
                    break;

                default:
                    throw new InvalidOperationException();
                    break;
            }
        }

        /*public void Foo(int n = 1)
        {

        }*/

        public void Save()
        {
            fileStream = new FileStream(savePath, FileMode.OpenOrCreate,
                                        FileAccess.Write, FileShare.Write);
            StreamWriter sw = new StreamWriter(fileStream);
            sw.WriteLine(FractalType);
            sw.WriteLine(ColorScheme);
            if (FractalType == "Julia Set")
            {
                sw.WriteLine(Re);
                sw.WriteLine(Im);
            }
            sw.Close();
            fileStream.Close();
        }

        public void Open(string path)
        {
            fileStream = new FileStream(path, FileMode.Open,
                                        FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fileStream);

            FractalType = sr.ReadLine();
            ColorScheme = sr.ReadLine();
            Re = sr.ReadLine();
            Im = sr.ReadLine();

            sr.Close();
            fileStream.Close();
        }

        /*public void Open(string path)
        {
            fileStream = new FileStream(path, FileMode.Open,
                                        FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fileStream);

            fractalType = sr.ReadLine();
            colorScheme = sr.ReadLine();
            re = sr.ReadLine();
            im = sr.ReadLine();

            sr.Close();
            fileStream.Close();
        }*/
    }
}
