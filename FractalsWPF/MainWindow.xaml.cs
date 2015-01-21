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

using BitmapFractal;
using Fractals;

using System.Windows.Threading;
using System.Threading;

namespace FractalsWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Random random = new Random();

        //Aby rysować, Drawer musi mieć ustawione properties SavePath oraz Fractal
        private Drawer d;
        private DispatcherTimer dt;
        private Thread t;

        public MainWindow()
        {
            InitializeComponent();

            d = new Drawer();
            dt = new DispatcherTimer();
            dt.Interval = new TimeSpan(0, 0, 0, 0, 250);
            dt.Tick += dt_Tick;
            progressBar.Minimum = 0;
            progressBar.Maximum = Drawer.BITMAP_WIDTH * Drawer.BITMAP_HEIGHT;
            progressBar.Value = 0;

            saveBMPButton.IsEnabled = false;
            saveFractButton.IsEnabled = false;
            previewButton.IsEnabled = false;
        }

        ~MainWindow() // ????
        {
            if (t != null && t.IsAlive)
            {
                t.Abort();
            }
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            progressBar.Value = Drawer.Steps;
            if (progressBar.Value >= 3996001)
            {
                saveBMPButton.IsEnabled = true;
                saveFractButton.IsEnabled = true;
                previewButton.IsEnabled = true;
                dt.Stop();
            }
        }

        private void ListBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            JuliaSetCanvas.Visibility = Visibility.Hidden;
        }

        private void ListBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            JuliaSetCanvas.Visibility = Visibility.Visible;
        }

        private void ListBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {
            JuliaSetCanvas.Visibility = Visibility.Visible;
        }

        private void drawButton_Click(object sender, RoutedEventArgs e)
        {
            Drawer.Steps = 0;

            progressBar.Minimum = 0;
            progressBar.Maximum = Drawer.BITMAP_WIDTH * Drawer.BITMAP_HEIGHT;
            progressBar.Value = 0;

            saveBMPButton.IsEnabled = false;
            saveFractButton.IsEnabled = false;
            previewButton.IsEnabled = false;

            ColorMethodHandler colorHandler;
            Fractal fractal;

            string scheme = "";
            foreach(ListBoxItem item in colorSchemeListBox.Items) 
            {
                if (item.IsSelected)
                    scheme = (string)item.Content;
            }

            switch (scheme)
            {
                case "Scheme 1":
                    colorHandler = Coloring.Scheme1;
                    break;

                case "Scheme 2":
                    colorHandler = Coloring.Scheme2;
                    break;

                case "Random":
                    Coloring.Randomize();
                    colorHandler = Coloring.RandomScheme;
                    break;

                default:
                    colorHandler = Coloring.Scheme1;
                    break;
            }

            string fractalType = "";
            foreach (ListBoxItem item in fractalTypeListBox.Items)
            {
                if (item.IsSelected)
                    fractalType = (string)item.Content;
            }

            switch (fractalType)
            {
                case "Mandelbrot Set":
                    fractal = new MandelbrotFractal(colorHandler);
                    break;

                case "Julia Set":
                    {
                        double re = Double.Parse(reTextBox.Text);
                        double im = Double.Parse(imTextBox.Text);
                        fractal = new JuliaSetFractal(new ComplexNumber(re, im), colorHandler);
                    } break;

                case "Random Julia Set":
                    {
                        double re = random.NextDouble();
                        double im = random.NextDouble();
                        re -= 0.5;
                        im -= 0.5;
                        re *= 2;
                        im *= 2;
                        reTextBox.Text = re.ToString();
                        imTextBox.Text = im.ToString();
                        fractal = new JuliaSetFractal(new ComplexNumber(re, im), colorHandler);
                    } break;

                default:
                    fractal = new MandelbrotFractal(Coloring.Scheme1);
                    break;
            }
            //d.SavePath = savePath;
            d.Fractal = fractal;
            
            t = new Thread(d.Go);

            dt.Start();
            //d.Go();
            t.Start();
            //dt.Stop();
        }

        private void saveBMPButton_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Filter = "Bitmap (*.bmp)|*.bmp";

            Nullable<bool> result = sfd.ShowDialog();

            if (result == true)
            {
                d.SavePath = sfd.FileName;
                d.Save();
            }
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            t.Abort();
            dt.Stop();
            progressBar.Value = 0;
        }

        private void previewButton_Click(object sender, RoutedEventArgs e)
        {
            PreviewWindow pw = new PreviewWindow(d.Bitmap);
            pw.Show();
        }

        private void saveFractButton_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Filter = "Fractal file (*.fract)|*.fract";

            Nullable<bool> result = sfd.ShowDialog();

            if (result == true)
            {
                FractFile ff = new FractFile(d.Fractal);
                ff.SavePath = sfd.FileName;
                ff.Save();
            }
        }

        private void openFractButton_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "Fractal file (*.fract)|*.fract";

            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {

            }
        }

    }
}
