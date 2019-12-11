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
    }
}
