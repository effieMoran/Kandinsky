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
    
    public partial class Form1 : Form
    {

        Graphics graphics;
        Pen pen1 = new Pen(Color.Black, 1);
        Pen pen2 = new Pen(Color.White, 1);
        Pen pen  = new Pen(Color.Black, 1);

        Point point1 = new Point(0, 0);
        Point point2 = new Point(0, 0);

        Color paintcolor;
        bool choose = false;
        bool draw = false;
        int x, y, lx, ly = 0;
        Item currentItem;

        public enum Item {
            Rectangle, Elipse, Line, Text, Brush, Pencil, Eraser, ColorPicker,Circle
        }
        public Form1()
        {
            InitializeComponent();
        }

        public void setPenColor(Color color) {
            pen.Color = color;
            if (pen.Equals(pen2)) { color2.BackColor = color; }
            else { color1.BackColor = color; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            currentItem = Item.Pencil;
            pen = pen1;
            FontFamily[] family = FontFamily.Families;
            foreach (FontFamily font in family)
            {
                fontbox.Items.Add(font.GetName(1).ToString());
            }
        }
        //Mouse Events
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {  
                if (e.Button == MouseButtons.Left)
                {
                    draw = true;
                    point1 = e.Location;
                }    
        }
        

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {

            if (currentItem == Item.Pencil) { draw = false; }
            else if (currentItem == Item.Line)
            {
                if (draw)
                {
                    point2 = e.Location;
                    graphics = this.CreateGraphics();
                    graphics.DrawLine(pen, point1, point2);
                }
            }
            else if (currentItem == Item.Elipse) {
                point2 = e.Location;
                Rectangle r = new Rectangle(point1.X, point1.Y,point2.X-point1.X,point2.Y-point1.Y);
                graphics = this.CreateGraphics();
                graphics.DrawEllipse(pen, r);
            }//Maybe it will be useful to add a method call draw rectangle that returns a rectangle
            else if (currentItem == Item.Rectangle)
            {
                point2 = e.Location;
                Rectangle r;
                if (point1.X <= point2.X && point1.Y <= point2.Y)
                {
                    r = new Rectangle(point1.X, point1.Y, Math.Abs(point2.X - point1.X), Math.Abs(point2.Y - point1.Y));
                } else if (point1.X > point2.X && point1.Y <= point2.Y) {
                    r = new Rectangle(point2.X, point1.Y, Math.Abs(point2.X - point1.X), Math.Abs(point2.Y - point1.Y));
                }
                else if (point1.X <= point2.X && point1.Y > point2.Y)
                {
                    r = new Rectangle(point1.X, point2.Y, Math.Abs(point2.X - point1.X), Math.Abs(point2.Y - point1.Y));
                }
                else
                {
                    r = new Rectangle(point2.X, point2.Y, Math.Abs(point2.X - point1.X), Math.Abs(point2.Y - point1.Y));
                }
                graphics = this.CreateGraphics();
                graphics.DrawRectangle(pen, r);    
            }
            draw = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentItem == Item.Pencil)
            {
                if (draw)
                {
                    point2 = e.Location;
                    graphics = this.CreateGraphics();
                    graphics.DrawLine(pen, point1, point2);
                }
                point1 = point2;
            }
        }

        private void color2_Click(object sender, EventArgs e)
        {
            pen = pen2;
        }

        private void color1_Click(object sender, EventArgs e)
        {
            pen = pen1;
        }

       
        //Color selection
        private void colorBox_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            pen.Color = pb.BackColor;
            if (pen.Equals(pen2)) { color2.BackColor = pb.BackColor; }
            else { color1.BackColor = pb.BackColor; }
        }

        private void rubber_Click(object sender, EventArgs e)
        {
            pen.Color = Color.White;
        }

       
        private void line_Click(object sender, EventArgs e)
        {
            currentItem = Item.Line;
        }

      
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pen = new Pen(new SolidBrush(pen.Color),20);
        }

       

        private void elipse_Click(object sender, EventArgs e)
        {
            currentItem = Item.Elipse;
        }

        private void rectangle_Click(object sender, EventArgs e)
        {
            currentItem = Item.Rectangle;

        }

        private void pencil_Click(object sender, EventArgs e)
        {
            if (pen.Equals(pen2)) { pen.Color = color2.BackColor; }
            else { pen.Color = color1.BackColor; }
            
        }

        private void pipette_Click(object sender, EventArgs e)
        {
            Form sc = new SelectColor();
            sc.Show();
        }
        public void ColorSeleccionado() {
            //color1 = SelectColor.color;
        }
    }
}

