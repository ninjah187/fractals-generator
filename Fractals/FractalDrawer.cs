using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace Fractals
{
    class FractalDrawer
    {
        public FractalDrawer()
        {

        }

        public static void DrawCircle(float x, float y, float radius)
        {
            Ellipse e = new Ellipse()
            {
                Width = radius * 2,
                Height = radius * 2,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };

            MainWindow.MainCanvas.Children.Add(e);
            
            float centerX = x - radius;
            float centerY = y - radius;
            
            Canvas.SetTop(e, centerY);
            Canvas.SetLeft(e, centerX);

            if (radius > 15)
            {
                /*diameter *= 0.75f;
                DrawCircle(x, y, diameter);*/
                DrawCircle(x + (radius / 2), y, radius / 2);
                DrawCircle(x - (radius / 2), y, radius / 2);
                DrawCircle(x, y + (radius / 2), radius / 2);
                DrawCircle(x, y - (radius / 2), radius / 2);
            }
        }

        public void Cantor(float x, float y, float len)
        {
            if (len >= 1)
            {
                Line l = new Line()
                {
                    X1 = x,
                    X2 = x + len,
                    Y1 = y,
                    Y2 = y,
                    Stroke = Brushes.Black,
                    StrokeThickness = 3,
                    //Width = 1,
                    //Height = 1,
                };
                MainWindow.MainCanvas.Children.Add(l);

                y += 20;

                float offset = (2 / 3) * len;
                Cantor(x, y, len / 3);
                Cantor(x + offset, y, len / 3);
            }
        }

    }
}
