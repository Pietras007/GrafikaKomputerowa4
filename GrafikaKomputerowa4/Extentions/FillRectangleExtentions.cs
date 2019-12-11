using GrafikaKomputerowa4.Helpers;
using GrafikaKomputerowa4.Models;
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

        public static void PaintTriangle(this Graphics g, Color[,] colorToPaint, Triangle triangle, double[,] zBufor, object[,] objectLock)
        {
            var data = triangle.GetETTable();
            List<AETPointer>[] ET = data.Item1;
            List<AETPointer> AET = new List<AETPointer>();

           
            for (int y = data.Item2; y <= ET.Length - 1; y++)
            {
                FillingHelper.FillDokladne(colorToPaint, AET, y, triangle.color, zBufor, triangle, objectLock);
                

                for (int i = AET.Count - 1; i >= 0; i--)
                {
                    if (AET[i].Ymax == y)
                    {
                        AET.RemoveAt(i);
                    }
                }

                if (ET[y] != null)
                {
                    AET.AddRange(ET[y]);
                    AET = AET.OrderBy(o => o.X).ThenBy(x => x.m).ToList();
                }

                for (int i = 0; i < AET.Count; i++)
                {
                    AET[i].X += AET[i].m;
                }
            }
        }
    }
}
