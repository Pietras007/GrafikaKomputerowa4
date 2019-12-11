using GrafikaKomputerowa4.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaKomputerowa4.Extentions
{
    public static class DrawLineExtention
    {
        public static void DrawLineBetweenPoints(this Graphics g, Pen pen, (int,int) p1, (int, int)p2)
        {
            g.DrawLine(pen, p1.Item1, p1.Item2, p2.Item1, p2.Item2);
        }

        public static void PrintTriangle(this Graphics g, Pen pen, Triangle triangle)
        {
            //g.DrawLine(pen, triangle.p_A.Item1, triangle.p_A.Item2, triangle.p_B.Item1, triangle.p_B.Item2);
            //g.DrawLine(pen, triangle.p_B.Item1, triangle.p_B.Item2, triangle.p_C.Item1, triangle.p_C.Item2);
            //g.DrawLine(pen, triangle.p_C.Item1, triangle.p_C.Item2, triangle.p_A.Item1, triangle.p_A.Item2);
            g.DrawLineBetweenPoints(pen, triangle.p_A, triangle.p_B);
            g.DrawLineBetweenPoints(pen, triangle.p_B, triangle.p_C);
            g.DrawLineBetweenPoints(pen, triangle.p_C, triangle.p_A);
        }
    }
}
