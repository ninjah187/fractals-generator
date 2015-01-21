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
    class CircleDrawer
    {
        private float x;
        private float y;
        private float radius;

        private DispatcherTimer timer;

        public CircleDrawer()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(1);
            timer.Tick += timer_Tick;
        }

        public CircleDrawer(float x, float y, float radius)
            : this()
        {
            Initialize(x, y, radius);
        }

        public void Initialize(float x, float y, float radius)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }

        public void Go()
        {
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DrawCircle();
            //DrawCircle(x, y, radius);
        }

        //public static void DrawCircle(float x, float y, float radius)
        //{
        //    Ellipse e = new Ellipse()
        //    {
        //        Width = radius * 2,
        //        Height = radius * 2,
        //        Stroke = Brushes.Black,
        //        StrokeThickness = 1,
        //    };

        //    MainWindow.MainCanvas.Children.Add(e);

        //    float centerX = x - radius;
        //    float centerY = y - radius;

        //    Canvas.SetTop(e, centerY);
        //    Canvas.SetLeft(e, centerX);

        //    if (radius > 15)
        //    {
        //        /*diameter *= 0.75f;
        //        DrawCircle(x, y, diameter);*/
        //        DrawCircle(x + (radius / 2), y, radius / 2);
        //        DrawCircle(x - (radius / 2), y, radius / 2);
        //        DrawCircle(x, y + (radius / 2), radius / 2);
        //        DrawCircle(x, y - (radius / 2), radius / 2);
        //    }
        //}

        public void DrawCircle(float x, float y, float radius)
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

        public void DrawCircle()
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

            timer.Stop();

            if (radius > 1)
            {
                /*diameter *= 0.75f;
                DrawCircle(x, y, diameter);*/
                /*DrawCircle(x + (radius / 2), y, radius / 2);
                DrawCircle(x - (radius / 2), y, radius / 2);
                DrawCircle(x, y + (radius / 2), radius / 2);
                DrawCircle(x, y - (radius / 2), radius / 2);*/
                new CircleDrawer(x + (radius / 2), y, radius / 2).Go();
                new CircleDrawer(x - (radius / 2), y, radius / 2).Go();
                new CircleDrawer(x, y + (radius / 2), radius / 2).Go();
                new CircleDrawer(x, y - (radius / 2), radius / 2).Go();
            }
        }

    }
}
