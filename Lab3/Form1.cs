using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
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
        private readonly Random _random = new Random();
        private const int PointsCount = 10000;
        private double _pc1;
        private double _pc2;

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label1.Text = "0." + Convert.ToString(trackBar2.Value);
            trackBar3.Value = 100000 - trackBar2.Value;
            label2.Text = "0." + Convert.ToString(trackBar3.Value);
            ReDrawChart();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label2.Text = "0." + Convert.ToString(trackBar3.Value);
            trackBar2.Value = 100000 - trackBar3.Value;
            label1.Text = "0." + Convert.ToString(trackBar2.Value);
            ReDrawChart();
        }

        private void ReDrawChart()
        {
            _pc1 = trackBar2.Value / 1000;
            _pc2 = trackBar3.Value / 1000;
            g = CreateGraphics();
            g.Clear(Color.White);

            SolidBrush brushR = new SolidBrush(Color.Red);
            SolidBrush brushB = new SolidBrush(Color.Blue);
            Pen brushG = new Pen(Color.Green);

            var points1 = new int[PointsCount];
            var points2 = new int[PointsCount];
            double mx1 = 0;
            double mx2 = 0;

            for (int i = 0; i < PointsCount; i++)
            {
                points1[i] = _random.Next(100, 740);
                points2[i] = _random.Next(-100, 540);
                mx1 += points1[i];
                mx2 += points2[i];
            }
            //Мат ожидание mx
            mx1 /= PointsCount;
            mx2 /= PointsCount;

            double sigma1 = 0;
            double sigma2 = 0;
            for (int i = 0; i < PointsCount; i++)
            {
                sigma1 += Math.Pow(points1[i] - mx1, 2);
                sigma2 += Math.Pow(points2[i] - mx2, 2);
            }
            //Средня квадратичная Сигма
            sigma1 = Math.Sqrt(sigma1 / PointsCount);
            sigma2 = Math.Sqrt(sigma2 / PointsCount);

            var result1 = new double[1200];
            var result2 = new double[1200];
            int d = 0;

            double p1 = (sigma1 * Math.Sqrt(2 * Math.PI));
            double p2 = (sigma2 * Math.Sqrt(2 * Math.PI));
            for (int x = 0; x < 850; x++)
            {
                result1[x] =
                    (Math.Exp(-0.5 * Math.Pow((x - 100 - mx1) / sigma1, 2)) /
                        p1 * _pc1);

                result2[x] =
                    (Math.Exp(-0.5 * Math.Pow((x - 100 - mx2) / sigma2, 2)) /
                        p2 * _pc2);

                if (Math.Abs(result1[x] * 500 - result2[x] * 500) < 0.2)
                {
                    d = x;
                }

                g.FillRectangle(brushR, x, (int)(400 - (result1[x] * 40 * 50)), 1, 1);
                g.FillRectangle(brushB, x, (int)(400 - (result2[x] * 40 * 50)), 1, 1);
            }

            g.DrawLine(brushG, d, 80, d, 400);

            var error1 = result1.Take((int)d).Sum() / 10;
            var error2 = p1 > p2 ? result1.Skip((int)d).Sum() / 10 : result2.Skip((int)d).Sum() / 10;
            //var error2 = result2.Skip((int)d).Sum();
            label6.Text = (error1/10).ToString("F10");
            label7.Text = (error2/10).ToString("F10");
            label8.Text = ((error1 + error2)/10).ToString("F10");
        }
    }
}
