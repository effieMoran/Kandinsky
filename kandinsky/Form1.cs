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

        
        bool draw = false;
        bool filled = false;
        int x, y, lx, ly = 0;
        Item currentItem;
        Brush brush = new SolidBrush(Color.Black);

        public enum Item {
            Line, Text, Brush, Pencil, Eraser, ColorPicker,
            Circle,Square, Elipse, Rectangle, Triangle
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
            else if (currentItem == Item.Elipse || currentItem == Item.Circle)
            {
                point2 = e.Location;
                Rectangle r;
                if (currentItem == Item.Circle) { r = get_Square(point1, point2); }
                else
                { r = get_Rectangle(point1, point2); }
                graphics = this.CreateGraphics();
                if (filled) { graphics.FillEllipse(brush, r); }
                else { graphics.DrawEllipse(pen, r); }
            }
            else if (currentItem == Item.Rectangle || currentItem == Item.Square)
            {
                point2 = e.Location;
                Rectangle r;
                if (currentItem == Item.Square)
                {
                    r = get_Square(point1, point2);
                }
                else
                {
                    r = get_Rectangle(point1, point2);
                }
                graphics = this.CreateGraphics();
                if (filled)
                {
                    graphics.FillRectangle(brush, r);
                }
                else
                {
                    graphics.DrawRectangle(pen, r);
                }
            }
            else if (currentItem == Item.Triangle) {
                /* Make some research about how to draw a poligon
                if (draw)
                {
                    point2 = e.Location;
                    Point p1 = new Point();
                    Point p2 = new Point();
                    Point p3 = new Point();

                    int minx, miny, maxx, maxy = 0;
                    if (point1.X < point2.X)
                    {
                        minx = point1.X;
                        maxx = point2.X;
                    }
                    else
                    {
                        minx = point2.X;
                        maxx = point1.X;
                    }
                    if (point1.Y < point2.Y)
                    {
                        miny = point1.Y;
                        maxy = point2.Y;
                    }
                    else
                    {
                        miny = point2.Y;
                        maxy = point1.Y;
                    }
                    p1.X = minx + ((maxx - minx) / 2);
                    p1.Y = miny;
                    p2.X = minx;
                    p2.Y = maxy;
                    p3.X = maxx;
                    p3.Y = maxy;
                    graphics.DrawPolygon(pen, new Point[] { p1, p2, p3 });
                }
                */
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

        private Rectangle get_Square(Point p1, Point p2)
        {
            Rectangle r;
            int x, y = 0;
            int l = 0;
            if (Math.Abs(p1.X - p2.X) == Math.Abs(p1.Y - p2.Y)) { return get_Rectangle(p1, p2); }
            else if (Math.Abs(p1.X - p2.X) < Math.Abs(p1.Y - p2.Y))
            {
                l = Math.Abs(p1.X - p2.X);      
            }
            else {
                l = Math.Abs(p1.Y - p2.Y);
            }
            if (p1.X < p2.X) { x = p1.X; } else { x = p2.X; }
            if (p1.Y < p2.Y) { y = p1.Y; } else { y = p2.Y; }
            r = new Rectangle(x, y, l, l);
            return r;
        }

        private Rectangle get_Rectangle(Point point1, Point point2) {
            Rectangle r;
            if (point1.X <= point2.X && point1.Y <= point2.Y)
            {
                r = new Rectangle(point1.X, point1.Y, Math.Abs(point2.X - point1.X), Math.Abs(point2.Y - point1.Y));
            }
            else if (point1.X > point2.X && point1.Y <= point2.Y)
            {
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
            return r;
        }

        private void color2_Click(object sender, EventArgs e)
        {
            pen = pen2;
            brush = new SolidBrush(color2.BackColor);
        }

        private void color1_Click(object sender, EventArgs e)
        {
            pen = pen1;
            brush = new SolidBrush(color1.BackColor);
        }

       
        //Color selection
        private void colorBox_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            pen.Color = pb.BackColor;
            brush = new SolidBrush(pb.BackColor);
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

        private void fill_Click(object sender, EventArgs e)
        {
            filled = true;
        }

        private void unfill_Click(object sender, EventArgs e)
        {
            filled = false;
        }

        private void circle_Click(object sender, EventArgs e)
        {
            currentItem = Item.Circle;
        }

        private void square_Click(object sender, EventArgs e)
        {
            currentItem = Item.Square;
        }

        private void triangle_Click(object sender, EventArgs e)
        {
            currentItem = Item.Triangle;
        }

        private void pencil_Click(object sender, EventArgs e)
        {
            currentItem = Item.Pencil;
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

