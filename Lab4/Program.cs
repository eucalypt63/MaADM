using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<int[]> classes = new List<int[]>();
        List<int[]> images = new List<int[]>();   
        int[] array11 = new int[3] { 0, 0, 1 };
        images.Add(array11);
        int[] array12 = new int[3] { 1, 1, 1 };
        images.Add(array12);
        int[] array13 = new int[3] { -1, 1, 1 };
        images.Add(array13);

        int[] array21 = new int[3] { 0, 0, 0 };
        classes.Add(array21);
        int[] array22 = new int[3] { 0, 0, 0 };
        classes.Add(array22);
        int[] array23 = new int[3] { 0, 0, 0 };
        classes.Add(array23);

        Boolean Flag = true;
        while (Flag)
        {
            for (int i = 0; i < classes.Count && Flag; i++)
            {
                int sum = classes[i].Sum();
                int temp = 0;
                foreach(int[] d in classes)
                    if (sum <= d.Sum())
                        temp++;

                if (temp == 1)
                    Flag = false;
                else
                    for (int j = 0; j < classes.Count; j++)                   
                        for (int k = 0; k < classes[0].Length; k++)
                            if (j == i)                          
                                classes[j][k] += images[i][k];
                            else 
                                classes[j][k] -= images[i][k];
            }
        }

        int t = 0;
        foreach (int[] arrays in classes)
        {
            Console.Write("d" + t + "(x) = " + arrays[0] + " * X1 ");

            for (int i = 1; i < classes[0].Length; i++)
                if (arrays[i] >= 0)
                    Console.Write("+ " + arrays[i] + " * X" + (i+1) + " ");
                else
                    Console.Write("- " + (arrays[i] * -1) + " * X" + (i+1) + " ");

            Console.WriteLine();
            t++;
        }
        Console.Read();
    }
}