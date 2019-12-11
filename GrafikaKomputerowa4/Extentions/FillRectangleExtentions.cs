using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaKomputerowa4.Extentions
{
    public static class FillRectangleExtentions
    {
        public static void FillBetweenPoints(this Graphics g, SolidBrush brush, (int, int)[] tab)
        {
            List<Point> points = new List<Point>();
            foreach(var p in tab)
            {
                points.Add(new Point(p.Item1, p.Item2));
            }

            g.FillPolygon(brush, points.ToArray());
        }
    }
}
