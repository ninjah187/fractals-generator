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

using System.Drawing;
using System.IO;

namespace FractalsWPF
{
    public partial class PreviewWindow : Window
    {
        private Canvas mainCanvas;

        public PreviewWindow(Bitmap bitmap)
        {
            this.Title = "Fractal preview";
            this.SizeToContent = SizeToContent.WidthAndHeight;
            Canvas canvas = new Canvas()
            {
                Width = 400,
                Height = 400,
                Name = "mainCanvas"
            };

            this.AddChild(canvas);
            mainCanvas = canvas;

            BitmapImage bitmapImage;
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp); //zapis Bitmap do MemoryStream
                memory.Position = 0;
                bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory; //odczytanie do BitmapImage
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            var previewImage = new System.Windows.Controls.Image(); //obrazek do wyświetlenia
            previewImage.Source = bitmapImage;
            previewImage.Width = 400;
            previewImage.Height = 400;

            mainCanvas.Children.Add(previewImage);
        }

    }
}
