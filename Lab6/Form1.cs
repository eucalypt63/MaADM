using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;

namespace Lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Graphics g;

        public class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        public class Function
        {
            public int I1 { get; set; }
            public int X1 { get; set; }
            public int Y1 { get; set; }
            public int XY2 { get; set; }
        }

        List<Point> Class1 = new List<Point>();
        List<Point> Class2 = new List<Point>();
        Function Func;

        private void button1_Click(object sender, EventArgs e)
        {
            Class1.Clear();
            Class1.Add(new Point { X = int.Parse(textBox1.Text), Y = int.Parse(textBox2.Text) });
            Class1.Add(new Point { X = int.Parse(textBox3.Text), Y = int.Parse(textBox4.Text) });

            Class2.Clear();
            Class2.Add(new Point { X = int.Parse(textBox8.Text), Y = int.Parse(textBox7.Text) });
            Class2.Add(new Point { X = int.Parse(textBox6.Text), Y = int.Parse(textBox5.Text) });
            Func = new Function { I1 = 0, X1 = 0, Y1 = 0, XY2 = 0};
            Func = ReDrawChart(Func);

            label8.Text = "y = (" + Func.X1 * -1 + "X";
            if (Func.I1 * -1 < 0)
                label8.Text += " - " + Func.I1 + ") / (";
            else label8.Text += " + " + Func.I1 + ") / (";

            label8.Text += "" + Func.XY2 + "X";

            if (Func.Y1 < 0)
                label8.Text += " - " + Func.Y1 * -1 + ")";
            else label8.Text += " + " + Func.Y1 + ")";

            g = CreateGraphics();
            g.Clear(Color.White);
            Pen brushB = new Pen(Color.Black);
            Pen brushBl = new Pen(Color.Blue);
            Pen brushR = new Pen(Color.Red);
            g.DrawLine(brushB, 225, 200, 850, 200); //1 корд = 60 px
            g.DrawLine(brushB, 535, 10, 535, 380);
            g.DrawRectangle(brushB, 225, 5, 625, 375);
            Pen pen = new Pen(Color.Green, 1);
            int tmpx = 230;
            double xValue = (tmpx - 535) / 30.0;
            double yValue = (Func.X1 * -1 * xValue + Func.I1 * -1) / (Func.XY2 * xValue + Func.Y1);
            int tmpy = (int)((-yValue) * 30.0) + 200;

            for (int x = 230; x <= 845; x++)
            {
                xValue = (x - 535) / 30.0;
                yValue = (Func.X1 * -1 * xValue + Func.I1 * -1) / (Func.XY2 * xValue + Func.Y1);
                int y = (int)((-yValue) * 30.0) + 200;
                if (y < 5)
                    y = 5;

                if (y > 380)
                    y = 380;

                if (Math.Abs(tmpy - y) < 300)
                    g.DrawLine(pen, x, y, tmpx, tmpy);
                tmpx = x;
                tmpy = y;
            }

            foreach (Point P in Class1)
            {
                int x = P.X * 30 + 535;
                int y = - P.Y * 30 + 200;
                g.DrawRectangle(brushR, x, y, 3, 3);
            }

            foreach (Point P in Class2)
            {
                int x = P.X * 30 + 535;
                int y = - P.Y * 30 + 200;
                g.DrawRectangle(brushBl, x, y, 3, 3);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Brush brushBl = new SolidBrush(Color.Blue);
            Brush brushR = new SolidBrush(Color.Red);
            Random random = new Random();

            for (int t = 0; t < 25000; t++)
            {
                int x = random.Next(226, 848);
                int y = random.Next(6, 379);

                double xValue = (x - 535) / 30.0;
                double yValue = -(y - 200) / 30.0;

                if (Func.I1 + Func.X1 * xValue + Func.Y1 * yValue + Func.XY2 * xValue * yValue > 0)
                {
                    g.FillRectangle(brushR, x, y, 2, 2);
                }
                else
                {
                    g.FillRectangle(brushBl, x, y, 2, 2);
                }
            }
        }

        private Function ReDrawChart(Function Func)
        {
            Function Ki = new Function { I1 = 0, X1 = 0, Y1 = 0, XY2 = 0 };
            Function Kxxi = new Function { I1 = 1, X1 = 0, Y1 = 0, XY2 = 0 };
            int Correction = 1;
            foreach (Point P in Class1)
            {
                if (Ki.I1 + Ki.X1 * P.X + Ki.Y1 * P.Y + Ki.XY2 * P.X * P.Y <= 0)
                    Correction = 1;
                else Correction = 0;
                Kxxi = new Function { I1 = Kxxi.I1, X1 = 4 * P.X, Y1 = 4 * P.Y, XY2 = 16 * P.X * P.Y };
                Ki = new Function { I1 = Ki.I1 + Correction * Kxxi.I1, X1 = Ki.X1 + Correction * Kxxi.X1, Y1 = Ki.Y1 + Correction * Kxxi.Y1, XY2 = Ki.XY2 + Correction * Kxxi.XY2 };
            }

            foreach (Point P in Class2)
            {
                if (Ki.I1 + Ki.X1 * P.X + Ki.Y1 * P.Y + Ki.XY2 * P.X * P.Y > 0)
                    Correction = -1;
                else Correction = 0;
                Kxxi = new Function { I1 = Kxxi.I1, X1 = 4 * P.X, Y1 = 4 * P.Y, XY2 = 16 * P.X * P.Y };
                Ki = new Function { I1 = Ki.I1 + Correction * Kxxi.I1, X1 = Ki.X1 + Correction * Kxxi.X1, Y1 = Ki.Y1 + Correction * Kxxi.Y1, XY2 = Ki.XY2 + Correction * Kxxi.XY2 };
            }

            return (Ki);
        }
    }
}
