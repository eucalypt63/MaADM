using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lab6._2
{
    public partial class Form1 : Form
    {
        Graphics g;
        public Form1()
        {
            InitializeComponent();
            g = CreateGraphics();
        }
      
        class LineV
        {
            public int PointC;
            public int X;
        }

        class LineH
        {
            public int PointC;
            public int Y;
        }

        List<LineV> LineVert = new List<LineV>();
        List<LineH> LineHor = new List<LineH>();
        Boolean FlagV = false;
        Boolean FlagH = false;
        Boolean Flag = false;

        private void button1_Click(object sender, EventArgs e)
        {
            FlagH = true;
            FlagV = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FlagV = true;
            FlagH = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (LineVert.Count == 2 && LineHor.Count == 2)
            {
                Flag = false;
                foreach (LineV line in LineVert)
                {
                    if ((line.X < LineHor[0].PointC && line.X < LineHor[1].PointC && line.X > LineHor[0].PointC - 60 && line.X > LineHor[1].PointC - 60)
                        || (line.X > LineHor[0].PointC && line.X > LineHor[1].PointC && line.X < LineHor[0].PointC + 60 && line.X < LineHor[1].PointC + 60))
                    {
                        if ((LineHor[0].Y < line.PointC && LineHor[1].Y > line.PointC) || (LineHor[1].Y < line.PointC && LineHor[0].Y > line.PointC))
                        {
                            LineV TempLine = new LineV();
                            if (line == LineVert[0])
                            {
                                TempLine = LineVert[1];
                            }
                            else
                            {
                                TempLine = LineVert[0];
                            }

                            if ((TempLine.PointC - 40 < line.PointC && TempLine.PointC + 40 > line.PointC) || (TempLine.PointC + 40 > line.PointC && TempLine.PointC - 40 < line.PointC))
                            {
                                if (line.X < LineHor[0].PointC)
                                {
                                    if (TempLine.X > LineHor[0].PointC && TempLine.X > LineHor[1].PointC && TempLine.X < LineHor[0].PointC + 60 && TempLine.X < LineHor[1].PointC + 60)
                                    {
                                        Flag = true;
                                    }
                                }
                                else
                                {
                                    if (TempLine.X < LineHor[0].PointC && TempLine.X < LineHor[1].PointC && TempLine.X > LineHor[0].PointC - 60 && TempLine.X > LineHor[1].PointC - 60)
                                    {
                                        Flag = true;
                                    }
                                }
                            }
                        }
                    }
                }

                if (Flag)
                {
                    MessageBox.Show("Соответсвует");
                }
                else
                {
                    MessageBox.Show("Не соответсвует");
                }
            }
            else
            {
                MessageBox.Show("Не все линии нарисованы");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FlagV = false;
            FlagH = false;
            g.Clear(Color.White);

            Pen brushB = new Pen(Color.Black);
            g.DrawLine(brushB, 120, 20, 780, 20);
            g.DrawLine(brushB, 120, 400, 780, 400);
            g.DrawLine(brushB, 120, 20, 120, 400);
            g.DrawLine(brushB, 780, 20, 780, 400);

            LineVert.Clear();
            LineHor.Clear();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            if (FlagH && LineHor.Count < 2  && x > 120 + 40 && x < 780 - 40 )
            {
                Pen brushB = new Pen(Color.Black);
                g.DrawLine(brushB, x - 40, y, x + 40, y);

                LineH LT = new LineH();
                LT.PointC = x;
                LT.Y = y;

                LineHor.Add(LT);
            }
            else if (FlagV && LineVert.Count < 2 && y > 20 + 40 && y < 400 - 40)
            {
                Pen brushB = new Pen(Color.Black);
                g.DrawLine(brushB, x, y - 40, x, y + 40);

                LineV LT = new LineV();
                LT.PointC = y;
                LT.X = x;

                LineVert.Add(LT);
            }
            
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            g.Clear(Color.White);

            Pen brushB = new Pen(Color.Black);
            g.DrawLine(brushB, 120, 20, 780, 20);
            g.DrawLine(brushB, 120, 400, 780, 400);
            g.DrawLine(brushB, 120, 20, 120, 400);
            g.DrawLine(brushB, 780, 20, 780, 400);

            LineVert.Clear();
            LineHor.Clear();
        }
    }
}