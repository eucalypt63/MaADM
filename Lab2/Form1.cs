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
        int b = 0;

        private void Starter_Click(object sender, EventArgs e)
        {
            pointsObj.Clear();
            pointsClass.Clear();
            a = trackBar1.Value;
            b = 1;

            button1.Enabled = false;
            button2.Enabled = false;

            g = CreateGraphics();
            g.Clear(Color.White);
            g.DrawLine(Pens.Black, 515, 330, 10, 330);
            g.DrawLine(Pens.Black, 515, 10, 515, 330);
            g.DrawLine(Pens.Black, 10, 10, 515, 10);
            g.DrawLine(Pens.Black, 10, 10, 10, 330);
                
            Random random = new Random();
            SolidBrush brush = new SolidBrush(Color.Black);

            Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            SolidBrush brushs = new SolidBrush(randomColor);

            PointsCl tmpPointC = new PointsCl();
                
            tmpPointC.x = random.Next(496);
            tmpPointC.y = random.Next(310);
            tmpPointC.brush = brushs;

            pointsClass.Add(tmpPointC);
       
            for (int t = 0; t < a; t++)
            {
                PointsOb tmpPoint = new PointsOb();

                tmpPoint.x = random.Next(500);
                tmpPoint.y = random.Next(314);

                /// Расчёт ближайшего                    
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

                pointsObj.Add(tmpPoint);
                g.FillRectangle(tmpPoint.pointClass.brush, tmpPoint.x + 12, tmpPoint.y + 12, 4, 4);
            }

            g.FillRectangle(brush, pointsClass[0].x + 12, pointsClass[0].y + 12, 6, 6);

            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean Flag = true;
            while (Flag)
            {

                PointsOb Pt = new PointsOb();

                Flag = false;
                double distanceMaxMax = 0;
                for (int t = 0; t < b; t++)
                {
                    int ListTmp = 0;
                    double distanceMax = 0;
                    PointsOb PtF = new PointsOb();
                    foreach (var item in pointsClass[t].pointsObjs)
                    {
                        double distance = Math.Sqrt(Math.Pow(item.x - pointsClass[t].x, 2) + Math.Pow(item.y - pointsClass[t].y, 2));
                        if (distance > distanceMax)
                        {
                            distanceMax = distance;
                            PtF = item;
                        }
                        ListTmp++;
                    }

                    if (distanceMax > distanceMaxMax)
                    {
                        distanceMaxMax = distanceMax;
                        Pt = PtF;
                    }

                    pointsClass[t].pointsObjs.Clear();
                }

                double dist = 0;
                int Count = 0;
                for (int t = 0; t < b; t++)
                {
                    for (int k = 0; k < b; k++)
                    {
                        dist += Math.Sqrt(Math.Pow(pointsClass[k].x - pointsClass[t].x, 2) + Math.Pow(pointsClass[k].y - pointsClass[t].y, 2));
                        Count++;
                    }
                }

                //if (dist != 0)
                //  dist /= Count - b;////
                dist /= Count;

                if (dist / 2 < distanceMaxMax)
                {
                    PointsCl PtC = new PointsCl();
                    PtC.x = Pt.x;
                    PtC.y = Pt.y;

                    Random random = new Random();
                    Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                    SolidBrush brushs = new SolidBrush(randomColor);
                    PtC.brush = brushs;

                    pointsClass.Add(PtC);
                    b++;
                    Flag = true;
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
                //
            }

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
           
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            PointsOb Pt = new PointsOb();
            Boolean Flag = false;
            double distanceMaxMax = 0;
            for (int t = 0; t < b; t++)
            {
                int ListTmp = 0;
                double distanceMax = 0;
                PointsOb PtF = new PointsOb();
                foreach (var item in pointsClass[t].pointsObjs)
                {
                    double distance = Math.Sqrt(Math.Pow(item.x - pointsClass[t].x, 2) + Math.Pow(item.y - pointsClass[t].y, 2));
                    if (distance > distanceMax)
                    {
                        distanceMax = distance;
                        PtF = item;
                    }
                    ListTmp++;
                }

                if (distanceMax > distanceMaxMax)
                {
                    distanceMaxMax = distanceMax;
                    Pt = PtF;
                }

                pointsClass[t].pointsObjs.Clear();
            }

            double dist = 0;
            int Count = 0;
            for (int t = 0; t < b; t++)
            {
                for (int k = 0; k < b; k++)
                {
                    dist += Math.Sqrt(Math.Pow(pointsClass[k].x - pointsClass[t].x, 2) + Math.Pow(pointsClass[k].y - pointsClass[t].y, 2));
                    Count++;
                }
            }

            //if (dist != 0)
            //  dist /= Count - b;
            dist /= Count;


            if (dist / 2 < distanceMaxMax)
            {
                PointsCl PtC = new PointsCl();
                PtC.x = Pt.x;
                PtC.y = Pt.y;

                Random random = new Random();
                Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                SolidBrush brushs = new SolidBrush(randomColor);
                PtC.brush = brushs;

                pointsClass.Add(PtC);
                b++;
                Flag = true;
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
            //

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
