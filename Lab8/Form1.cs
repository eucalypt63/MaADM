using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

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

        class Line
        {
            public int X1;
            public int Y1;
            public int ClassPoint1 = -1;
            public Line[] PointLine1 = new Line[4] {null, null, null, null};

            public int X2;
            public int Y2;
            public int ClassPoint2 = -1;
            public Line[] PointLine2 = new Line[4] { null, null, null, null };

            public int lengthX;
            public int lengthY;
            public Boolean draw = false;

            public int DrawX1;
            public int DrawY1;
            public int DrawX2;
            public int DrawY2;
        }

        List<Line> Lines = new List<Line>();

        Boolean Drawing = false;

        int TempX = 0;
        int TempY = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            Drawing = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);

            Pen brushB = new Pen(Color.Black);
            g.DrawLine(brushB, 120, 20, 780, 20);
            g.DrawLine(brushB, 120, 400, 780, 400);
            g.DrawLine(brushB, 120, 20, 120, 400);
            g.DrawLine(brushB, 780, 20, 780, 400);

            Lines[0].DrawX1 = 350;
            Lines[0].DrawY1 = 250;

            if (Lines[0].X1 > Lines[0].X2)
            {
                Lines[0].DrawX2 = 350 - Lines[0].lengthX;
                Lines[0].DrawY2 = 250;
            }
            else if (Lines[0].X1 < Lines[0].X2)
            {
                Lines[0].DrawX2 = 350 + Lines[0].lengthX;
                Lines[0].DrawY2 = 250;
            }
            else if (Lines[0].Y1 > Lines[0].Y2)
            {
                Lines[0].DrawX2 = 350;
                Lines[0].DrawY2 = 250 - Lines[0].lengthY;
            }
            else if (Lines[0].Y1 < Lines[0].Y2)
            {
                Lines[0].DrawX2 = 350;
                Lines[0].DrawY2 = 250 + Lines[0].lengthY;
            }
            Lines[0].draw = true;
            g.DrawLine(brushB, Lines[0].DrawX1, Lines[0].DrawY1, Lines[0].DrawX2, Lines[0].DrawY2);

            Boolean Flag = true;
            while (Flag)
            {
                Flag = false;
                foreach (Line line in Lines)
                {
                    if (line.draw)
                    {
                        int i = 0;
                        foreach (Line lineD in line.PointLine1)
                        {
                            if (lineD != null && !lineD.draw)
                            {
                                Flag = true;
                                lineD.draw = true;

                                if (lineD.ClassPoint1 == i)
                                {
                                    lineD.DrawX1 = line.DrawX1;
                                    lineD.DrawY1 = line.DrawY1;
                                    if (i == 1 || i == 0)
                                    {
                                        lineD.DrawX2 = lineD.DrawX1 - lineD.lengthX;
                                        lineD.DrawY2 = lineD.DrawY1 + lineD.lengthY;
                                    }
                                    else
                                    {
                                        lineD.DrawX2 = lineD.DrawX1 + lineD.lengthX;
                                        lineD.DrawY2 = lineD.DrawY1 - lineD.lengthY;
                                    }
                                }
                                else
                                {
                                    lineD.DrawX2 = line.DrawX1;
                                    lineD.DrawY2 = line.DrawY1;
                                    if (i == 1 || i == 0)
                                    {
                                        lineD.DrawX1 = lineD.DrawX2 - lineD.lengthX;
                                        lineD.DrawY1 = lineD.DrawY2 + lineD.lengthY;
                                    }
                                    else
                                    {
                                        lineD.DrawX1 = lineD.DrawX2 + lineD.lengthX;
                                        lineD.DrawY1 = lineD.DrawY2 - lineD.lengthY;
                                    }
                                }
                                g.DrawLine(brushB, lineD.DrawX1, lineD.DrawY1, lineD.DrawX2, lineD.DrawY2);
                            }
                            i++;
                        }

                        i = 0;
                        foreach (Line lineD in line.PointLine2)
                        {
                            if (lineD != null && !lineD.draw)
                            {
                                Flag = true;
                                lineD.draw = true;
                                if (lineD.ClassPoint1 == i)
                                {
                                    lineD.DrawX1 = line.DrawX2;
                                    lineD.DrawY1 = line.DrawY2;
                                    if (i == 1 || i == 0)
                                    {
                                        lineD.DrawX2 = lineD.DrawX1 - lineD.lengthX;
                                        lineD.DrawY2 = lineD.DrawY1 + lineD.lengthY;
                                    }
                                    else
                                    {
                                        lineD.DrawX2 = lineD.DrawX1 + lineD.lengthX;
                                        lineD.DrawY2 = lineD.DrawY1 - lineD.lengthY;
                                    }
                                }
                                else
                                {
                                    lineD.DrawX2 = line.DrawX2;
                                    lineD.DrawY2 = line.DrawY2;
                                    if (i == 1 || i == 0)
                                    {
                                        lineD.DrawX1 = lineD.DrawX2 - lineD.lengthX;
                                        lineD.DrawY1 = lineD.DrawY2 + lineD.lengthY;
                                    }
                                    else
                                    {
                                        lineD.DrawX1 = lineD.DrawX2 + lineD.lengthX;
                                        lineD.DrawY1 = lineD.DrawY2 - lineD.lengthY;
                                    }
                                }
                                g.DrawLine(brushB, lineD.DrawX1, lineD.DrawY1, lineD.DrawX2, lineD.DrawY2);
                            }
                            i++;
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Drawing = false;
            foreach (Line line in Lines)
            {
                foreach (Line lineCheck in Lines)
                {
                    if (line != lineCheck)
                    {
                        if (Math.Abs(line.X1 - lineCheck.X1) < 20 && Math.Abs(line.Y1 - lineCheck.Y1) < 20)
                        {
                            line.PointLine1[lineCheck.ClassPoint1] = lineCheck;
                        }
                        else if (Math.Abs(line.X1 - lineCheck.X2) < 20 && Math.Abs(line.Y1 - lineCheck.Y2) < 20)
                        {
                            line.PointLine1[lineCheck.ClassPoint2] = lineCheck;
                        }

                        if (Math.Abs(line.X2 - lineCheck.X1) < 20 && Math.Abs(line.Y2 - lineCheck.Y1) < 20)
                        {
                            line.PointLine2[lineCheck.ClassPoint1] = lineCheck;
                        }
                        else if (Math.Abs(line.X2 - lineCheck.X2) < 20 && Math.Abs(line.Y2 - lineCheck.Y2) < 20)
                        {
                            line.PointLine2[lineCheck.ClassPoint2] = lineCheck;
                        }
                    }
                }
            }            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Drawing = false;
            g.Clear(Color.White);

            Pen brushB = new Pen(Color.Black);
            g.DrawLine(brushB, 120, 20, 780, 20);
            g.DrawLine(brushB, 120, 400, 780, 400);
            g.DrawLine(brushB, 120, 20, 120, 400);
            g.DrawLine(brushB, 780, 20, 780, 400);

            Lines.Clear();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            if (Drawing && x > 120 && x < 780 && y > 20 && y < 400)
            {
                if (TempX != 0)
                {
                    Pen brushB = new Pen(Color.Black);
                    Line TempLine = new Line();
                    if (Math.Abs(y - TempY) < Math.Abs(x - TempX))
                    {
                        g.DrawLine(brushB, x, TempY, TempX, TempY);
                        TempLine.X1 = x;
                        TempLine.Y1 = TempY;

                        TempLine.X2 = TempX;
                        TempLine.Y2 = TempY;

                        TempLine.lengthX = Math.Abs(x - TempX);
                        TempLine.lengthY = 0;

                        if (x > TempX)
                        {
                            TempLine.ClassPoint1 = 1;
                            TempLine.ClassPoint2 = 3;
                        }
                        else
                        {
                            TempLine.ClassPoint1 = 3;
                            TempLine.ClassPoint2 = 1;
                        }
                    }
                    else
                    {
                        g.DrawLine(brushB, TempX, y, TempX, TempY);
                        TempLine.X1 = TempX;
                        TempLine.Y1 = y;

                        TempLine.X2 = TempX;
                        TempLine.Y2 = TempY;

                        TempLine.lengthX = 0;
                        TempLine.lengthY = Math.Abs(y - TempY);

                        if (y > TempY)
                        {
                            TempLine.ClassPoint1 = 2;
                            TempLine.ClassPoint2 = 0;
                        }
                        else
                        {
                            TempLine.ClassPoint1 = 0;
                            TempLine.ClassPoint2 = 2;
                        }
                    }
                    Lines.Add(TempLine);

                    TempX = 0;
                    TempY = 0;
                }
                else
                {
                    TempX = x;
                    TempY = y;
                }
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
        }
    }
}