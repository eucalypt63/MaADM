using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lab1.Form1;
using System.Collections;

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Graphics g;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = CreateGraphics();
            g.Clear(Color.White);

            g.DrawLine(Pens.Black, 515, 330, 10, 330);
            g.DrawLine(Pens.Black, 515, 10, 515, 330);

            g.DrawLine(Pens.Black, 10, 10, 515, 10);
            g.DrawLine(Pens.Black, 10, 10, 10, 330);

            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBar2.Value.ToString();
        }

        public class PointsCl
        {
            public int x = 0;
            public int y = 0;
            public SolidBrush brush;
            public List<PointsOb> pointsObjs = new List<PointsOb>();
        }

        public class PointsOb
        {
            public int x = 0;
            public int y = 0;
            public PointsCl pointClass;
        }

        List<PointsOb> pointsObj = new List<PointsOb>();
        List<PointsCl> pointsClass = new List<PointsCl>();
        int a = 100;
        int b = 2;

        private void Starter_Click(object sender, EventArgs e)
        {
            pointsObj.Clear();
            pointsClass.Clear();
            a = trackBar1.Value;
            b = trackBar2.Value;

            button1.Enabled = false;
            button2.Enabled = false;
            //!!!

            g = CreateGraphics();
            g.Clear(Color.White);

            g.DrawLine(Pens.Black, 515, 330, 10, 330);
            g.DrawLine(Pens.Black, 515, 10, 515, 330);
            g.DrawLine(Pens.Black, 10, 10, 515, 10);
            g.DrawLine(Pens.Black, 10, 10, 10, 330);
            ////
                
            Random random = new Random();

            SolidBrush brush = new SolidBrush(Color.Black);
            for (int t = 0; t < b; t++)
            {
                Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                SolidBrush brushs = new SolidBrush(randomColor);
                PointsCl tmpPoint = new PointsCl();
                
                tmpPoint.x = random.Next(496);
                tmpPoint.y = random.Next(310);
                tmpPoint.brush = brushs;

                pointsClass.Add(tmpPoint);
            }

            for (int t = 0; t < a; t++)
            {
                PointsOb tmpPoint = new PointsOb();

                tmpPoint.x = random.Next(500);
                tmpPoint.y = random.Next(314);

                ///
                /// Расчёт ближайшего
                    
                double distanceMin = 1000;
                PointsCl tmpPointCl = new PointsCl();

                int dT = 0;
                for (int d = 0; d < b; d++)
                {
                    double distance = Math.Sqrt(Math.Pow(tmpPoint.x - pointsClass[d].x, 2) + Math.Pow(tmpPoint.y - pointsClass[d].y, 2));
                    if (distance < distanceMin)
                    {
                        distanceMin = distance;
                        dT = d;
                    }
                }
                tmpPoint.pointClass = pointsClass[dT];
                pointsClass[dT].pointsObjs.Add(tmpPoint);

                pointsObj.Add(tmpPoint);
                g.FillRectangle(tmpPoint.pointClass.brush, tmpPoint.x + 12, tmpPoint.y + 12, 4, 4);
            }

            for (int t = 0; t < b; t++)
                g.FillRectangle(brush, pointsClass[t].x + 12, pointsClass[t].y + 12, 6, 6);

            //!!!
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean Flag = true;
            button1.Enabled = false;
            button2.Enabled = false;
            while (Flag)
            {
                Flag = false;
                double SumX = 0;
                double SumY = 0;
                for (int t = 0; t < b; t++)
                {
                    int Count = 0;
                    foreach (var item in pointsClass[t].pointsObjs)
                    {
                        SumX += item.x;
                        SumY += item.y;
                        Count += 1;
                    }

                    if (Count > 0)
                    {
                        SumX /= Count;
                        SumY /= Count;
                    }

                    if ((int)Math.Round(SumX) != pointsClass[t].x || (int)Math.Round(SumY) != pointsClass[t].y)
                    {
                        Flag = true;
                    }
                    pointsClass[t].x = (int)Math.Round(SumX);
                    pointsClass[t].y = (int)Math.Round(SumY);

                    pointsClass[t].pointsObjs.Clear();
                }

                for (int t = 0; t < a; t++)
                {
                    PointsOb tmpPoint = pointsObj[t];
                    double distanceMin = 1000;

                    int dT = 0;
                    for (int d = 0; d < b; d++)
                    {
                        double distance = Math.Sqrt(Math.Pow(tmpPoint.x - pointsClass[d].x, 2) + Math.Pow(tmpPoint.y - pointsClass[d].y, 2));
                        if (distance < distanceMin)
                        {
                            distanceMin = distance;
                            dT = d;
                        }
                    }
                    tmpPoint.pointClass = pointsClass[dT];
                    pointsClass[dT].pointsObjs.Add(tmpPoint);

                    pointsObj[t] = tmpPoint;
                }
            }
            ///

            g = CreateGraphics();
            g.Clear(Color.White);

            g.DrawLine(Pens.Black, 515, 330, 10, 330);
            g.DrawLine(Pens.Black, 515, 10, 515, 330);
            g.DrawLine(Pens.Black, 10, 10, 515, 10);
            g.DrawLine(Pens.Black, 10, 10, 10, 330);

            for (int t = 0; t < a; t++)
            {
                PointsOb tmpPoint = pointsObj[t];
                g.FillRectangle(tmpPoint.pointClass.brush, tmpPoint.x + 12, tmpPoint.y + 12, 4, 4);
            }

            SolidBrush brush = new SolidBrush(Color.Black);
            for (int t = 0; t < b; t++)
                g.FillRectangle(brush, pointsClass[t].x + 12, pointsClass[t].y + 12, 6, 6);
            ///            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double SumX = 0;
            double SumY = 0;
            Boolean Flag = true;
            for (int t = 0; t < b; t++)
            {
                Flag = false;
                int Count = 0;
                foreach (var item in pointsClass[t].pointsObjs)
                {
                    SumX += item.x;
                    SumY += item.y;
                    Count += 1;
                }

                if (Count > 0)
                {
                    SumX /= Count;
                    SumY /= Count;
                }

                if ((int)Math.Round(SumX) != pointsClass[t].x || (int)Math.Round(SumY) != pointsClass[t].y)
                {
                    Flag = true;
                }
                pointsClass[t].x = (int)Math.Round(SumX);
                pointsClass[t].y = (int)Math.Round(SumY);

                pointsClass[t].pointsObjs.Clear();
            }

            if (!Flag)
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }

            for (int t = 0; t < a; t++)
            {
                PointsOb tmpPoint = pointsObj[t];
                double distanceMin = 1000;

                int dT = 0;
                for (int d = 0; d < b; d++)
                {
                    double distance = Math.Sqrt(Math.Pow(tmpPoint.x - pointsClass[d].x, 2) + Math.Pow(tmpPoint.y - pointsClass[d].y, 2));
                    if (distance < distanceMin)
                    {
                        distanceMin = distance;
                        dT = d;
                    }
                }
                tmpPoint.pointClass = pointsClass[dT];
                pointsClass[dT].pointsObjs.Add(tmpPoint);

                pointsObj[t] = tmpPoint;
            }

            ///

            g = CreateGraphics();
            g.Clear(Color.White);

            g.DrawLine(Pens.Black, 515, 330, 10, 330);
            g.DrawLine(Pens.Black, 515, 10, 515, 330);
            g.DrawLine(Pens.Black, 10, 10, 515, 10);
            g.DrawLine(Pens.Black, 10, 10, 10, 330);

            for (int t = 0; t < a; t++)
            {
                PointsOb tmpPoint = pointsObj[t];
                g.FillRectangle(tmpPoint.pointClass.brush, tmpPoint.x + 12, tmpPoint.y + 12, 4, 4);
            }

            SolidBrush brush = new SolidBrush(Color.Black);
            for (int t = 0; t < b; t++)
                g.FillRectangle(brush, pointsClass[t].x + 12, pointsClass[t].y + 12, 6, 6);

        }
    }
}
