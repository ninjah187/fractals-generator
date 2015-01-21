using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Fractals
{
    //KILKA UWAG:
    //współrzędne normalne - współrzędne w kartezjańskim układzie współrzędnych
    //współrzędne w window space - współrzędne po przekształceniach
    //rysowanie: wsp. norm -> win. space -> rysowanie

    // TODO: Rysownik fraktali na bitmapie (z opcją zapisywania)
    // http://csharphelper.com/blog/2014/07/draw-a-mandelbrot-set-fractal-in-c/

    class MandelbrotDrawer
    {
        public const int SCALE_UNIT = 200;

        public MandelbrotDrawer()
        {
            DrawAxis();
            //DrawPoint(new Point(1, 2));
            //DrawPoint(new Point(0.33, -1.768));
            DrawScale();
            DrawSet();
            //DrawPoint(0.5, 0.5);
        }

        private void DrawFunc()
        {
            for (double i = 0; i < 10; i += 0.01)
            {
                DrawPoint(i, i);
            }
        }

        private void Normalize(ref double x, ref double y)
        {
            x += MainWindow.MainCanvas.Width / 2;
            y += MainWindow.MainCanvas.Height / 2;
        }

        //Dostaje na wejściu punkt w normalnych współrzędnych
        //zwraca nowy punkt o współrzędnych w przestrzeni okna
        private Point Normalize(Point p)
        {
            return new Point(MainWindow.MainCanvas.Width / 2 + p.X,
                                MainWindow.MainCanvas.Height / 2 - p.Y);
        }

        //Rysuje osie
        private void DrawAxis()
        {
            Line X = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                X1 = 0,
                Y1 = MainWindow.MainCanvas.Height / 2,
                X2 = MainWindow.MainCanvas.Width,
                Y2 = MainWindow.MainCanvas.Height / 2
            };
            Line Y = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                X1 = MainWindow.MainCanvas.Width / 2,
                Y1 = 0,
                X2 = MainWindow.MainCanvas.Width / 2,
                Y2 = MainWindow.MainCanvas.Height,
            };
            MainWindow.MainCanvas.Children.Add(X);
            MainWindow.MainCanvas.Children.Add(Y);
        }

        //Rysuje skalę na osiach
        private void DrawScale()
        {
            //środek w window space
            Point center = Normalize(new Point(0, 0));
            // na osi Y od środka w dół
            for (double i = center.Y + SCALE_UNIT; i < MainWindow.MainCanvas.Height; i += SCALE_UNIT)
            {
                Line l = new Line()
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    X1 = center.X - 10,
                    Y1 = i,
                    X2 = center.X + 10,
                    Y2 = i,
                };
                MainWindow.MainCanvas.Children.Add(l);
            }
            // na osi Y w górę
            for (double i = center.Y - SCALE_UNIT; i > 0; i -= SCALE_UNIT)
            {
                Line l = new Line()
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    X1 = center.X - 10,
                    Y1 = i,
                    X2 = center.X + 10,
                    Y2 = i,
                };
                MainWindow.MainCanvas.Children.Add(l);
            }
            // na osi X w prawo
            for (double i = center.X + SCALE_UNIT; i < MainWindow.MainCanvas.Width; i += SCALE_UNIT)
            {
                Line l = new Line()
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    X1 = i,
                    Y1 = center.Y - 10,
                    X2 = i,
                    Y2 = center.Y + 10,
                };
                MainWindow.MainCanvas.Children.Add(l);
            }
            // na osi X w lewo
            for (double i = center.X - SCALE_UNIT; i > 0; i -= SCALE_UNIT)
            {
                Line l = new Line()
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    X1 = i,
                    Y1 = center.Y - 10,
                    X2 = i,
                    Y2 = center.Y + 10,
                };
                MainWindow.MainCanvas.Children.Add(l);
            }
        }

        //Rysuje punkt o podanych normalnych współrzędnych
        private void DrawPoint(Point p)
        {
            Ellipse e = new Ellipse()
            {
                Width = 2,
                Height = 2,
                StrokeThickness = 0,
                Fill = Brushes.Black
            };
            MainWindow.MainCanvas.Children.Add(e);

            //Point normalized = Normalize(p);
            Point normalized = p.ToWindowSpace();
            Canvas.SetTop(e, normalized.Y - e.Height / 2);
            Canvas.SetLeft(e, normalized.X - e.Width / 2);
        }

        private void DrawPoint(double x, double y)
        {
            DrawPoint(new Point(x, y));
        }

        private void DrawSet()
        {
            double iteration = 1 / (double)SCALE_UNIT;
            //for (double x = -2.5; x <= 1; x += iteration)
            for (double x = -1.6; x <= 1.6; x += iteration)
            {
                //for (double y = -1; y <= 1; y += iteration)
                for (double y = -1.5; y <= 1.5; y += iteration)
                {
                    Point p = new Point(x, y);
                    if (IsInSet(p))
                    {
                        DrawPoint(p);
                    }
                }
            }
        }

        private bool IsInSet(Point p)
        {
            ComplexNumber z = new ComplexNumber();
            ComplexNumber z1 = new ComplexNumber();
            ComplexNumber c = new ComplexNumber(p.X, p.Y);
            for (int n = 0; n < 50; n++)
            {
                z1 = (z * z) + c;
                if (z1.Magnitude >= 2)
                {
                    DrawPointOutOfSet(p, n);
                    return false;
                }
                z = z1;
            }
            return true;
        }

        private void DrawPointOutOfSet(Point p, int n)
        {
            //Color color = Color.FromRgb(0, 0, 0);
            //color.B += (byte)(n * 5);
            Color color = Color.FromRgb(0, 0, 100);
            color.R = color.G = (byte)(n * 5);

            Ellipse e = new Ellipse()
            {
                Width = 2,
                Height = 2,
                Fill = new SolidColorBrush(color),
                StrokeThickness = 0
            };
            MainWindow.MainCanvas.Children.Add(e);
            
            Point winSpace = p.ToWindowSpace();
            Canvas.SetTop(e, winSpace.Y - e.Height / 2);
            Canvas.SetLeft(e, winSpace.X - e.Width / 2);
        }
    }
}
