using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;

namespace Kandinsky
{
    public partial class SelectColor : Form
    {
        public Color color { get; set; }
        Color paintcolor = Color.Black;
        public SelectColor()
        {
            InitializeComponent();
        }

        //Color picker
        private void ColorPalette_MouseMove(object sender, MouseEventArgs e)
        {

            Bitmap bmp = (Bitmap)colorpalette.Image.Clone();
     
            //Console.WriteLine("E.X: {0}    E.Y:{1}",e.X,e.Y ); 
            if (5 < e.X && e.X < 120 && 5 < e.Y && e.Y < 120) { 
                paintcolor = bmp.GetPixel(e.X, e.Y);
                redt.Value = paintcolor.R;
                greent.Value = paintcolor.G;
                bluet.Value = paintcolor.B;
                alphat.Value = paintcolor.A;
                redval.Text = paintcolor.R.ToString();
                greenval.Text = paintcolor.G.ToString();
                blueval.Text = paintcolor.B.ToString();
                alphaval.Text = paintcolor.A.ToString();
                topick.BackColor = paintcolor;
            }
        }


        private void colorpalette_Click(object sender, EventArgs e)
        {
            selectedcolor.BackColor = paintcolor;
        }

        public void SelectColor_Close(object sender, FormClosedEventArgs e)
        {
            color = new Color();
            color = (selectedcolor.BackColor);
        }
    }
}
