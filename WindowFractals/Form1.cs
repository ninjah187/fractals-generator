using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BitmapFractal;

namespace WindowFractals
{
    public partial class Form1 : Form
    {
        private Drawer d;

        public Form1()
        {
            InitializeComponent();

            d = new Drawer();
            d.Go();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(d.Bitmap, 0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
