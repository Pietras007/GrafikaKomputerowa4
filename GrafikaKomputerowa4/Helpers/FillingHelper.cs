using GrafikaKomputerowa4.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaKomputerowa4.Helpers
{
    public static class FillingHelper
    {
        public static void FillDokladne(Color[,] colorToPaint, List<AETPointer> AET, int y, Color backColor, double[,] zBufor, Triangle triangle, object[,] objectLock)
        {
            for (int i = 0; i < AET.Count; i += 2)
            {
                for (int x = (int)Math.Round(AET[i].X) + 1; x <= Math.Round(AET[i + 1].X); x++)
                {
                    if (x >= 0 && y >= 0 && x < 776 && y < 426)
                    {
                        double z = CountZ(triangle, x, y);
                        lock (objectLock[x, y])
                        {
                            if (z > zBufor[x, y])
                            {
                                colorToPaint[x, y] = backColor;
                                zBufor[x, y] = z;
                            }
                        }
                    }
                }
            }
        }

        public static double CountZ(Triangle triangle, int x, int y)
        {
            double ABC = TriangleArea(triangle.p_A, triangle.p_B, triangle.p_C);
            double alpha = TriangleArea((x, y), triangle.p_B, triangle.p_C) / ABC;
            double beta = TriangleArea(triangle.p_A, (x, y), triangle.p_C) / ABC;
            double gamma = TriangleArea(triangle.p_A, triangle.p_B, (x, y)) / ABC;
            double z = alpha * triangle.ppA.Item3 + beta * triangle.ppB.Item3 + gamma * triangle.ppC.Item3;
           
            return z;
        }

        public static double TriangleArea((int, int) A, (int, int) B, (int, int) C)
        {
            return (double)Math.Abs((B.Item1 - A.Item1) * (C.Item2 - A.Item2) - (B.Item2 - A.Item2) * (C.Item1 - A.Item1)) / 2;
        }
    }
}
