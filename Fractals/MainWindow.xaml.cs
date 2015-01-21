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

namespace Fractals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Canvas MainCanvas;
        
        public MainWindow()
        {
            InitializeComponent();

            MainWindow.MainCanvas = mainCanvas;

            /*FractalDrawer.DrawCircle((float)MainWindow.MainCanvas.Width / 2,
                                        (float)MainWindow.MainCanvas.Height / 2, 300);*/

            //CircleDrawer cd = new CircleDrawer((float)MainWindow.MainCanvas.Width / 2,
            //                            (float)MainWindow.MainCanvas.Height / 2, 300);
            //cd.Go();

            //FractalDrawer.Cantor(10, 20, (float)MainCanvas.Width - 20);
            //FractalDrawer fd = new FractalDrawer();
            //fd.Cantor(10, 20, (float)MainCanvas.Width - 20);

            MandelbrotDrawer md = new MandelbrotDrawer();
        }
    }
}
