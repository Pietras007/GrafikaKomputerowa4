using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaKomputerowa4.Models
{
    public class Triangle
    {
        public Vertice A { get; set; }
        public Vertice B { get; set; }
        public Vertice C { get; set; }
        public (float, float, float) ppA { get; set; }
        public (float, float, float) ppB { get; set; }
        public (float, float, float) ppC { get; set; }
        public (int, int) p_A { get; set; }
        public (int, int) p_B { get; set; }
        public (int, int) p_C { get; set; }
        public List<Edge> edges { get; set; }
        public List<(int, int)> vertices { get; set; }
        public Color color { get; set; }

        public List<((int, int), (int, int))> eedges { get; set; }
        public Triangle(Vertice A, Vertice B, Vertice C, Color _color)
        {
            //Random random = new Random();
            //color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            color = _color;
            vertices = new List<(int, int)>();
            eedges = new List<((int, int), (int, int))>();
            edges = new List<Edge>();
            this.A = A;
            this.B = B;
            this.C = C;
            edges.Add(new Edge(A, B));
            edges.Add(new Edge(B, C));
            edges.Add(new Edge(C, A));
        }

        public (List<AETPointer>[], int) GetETTable()
        {
            eedges.Clear();
            eedges.Add((p_A, p_B));
            eedges.Add((p_B, p_C));
            eedges.Add((p_C, p_A));

            List<AETPointer>[] aETPointers = new List<AETPointer>[GetYmaxFromAll() + 1];
            foreach (var e in eedges)
            {
                AETPointer aETPointer = new AETPointer(e.Item1, e.Item2);
                if (1 / aETPointer.m != 0)
                {
                    int yMin = Math.Min(e.Item1.Item2, e.Item2.Item2);
                    //if (yMin >= 0)
                    {
                        if (aETPointers[yMin] == null)
                        {
                            aETPointers[yMin] = new List<AETPointer>();
                        }
                        aETPointers[yMin].Add(aETPointer);
                    }
                }
            }


            return (aETPointers, GetYminFromAll());
        }

        public int GetYmaxFromAll()
        {
            vertices.Clear();
            vertices.Add(p_A);
            vertices.Add(p_B);
            vertices.Add(p_C);

            int yMax = 0;
            foreach (var v in vertices)
            {
                if (v.Item2 > yMax)
                {
                    yMax = v.Item2;
                }
            }
            return yMax;
        }

        public int GetYminFromAll()
        {
            vertices.Clear();
            vertices.Add(p_A);
            vertices.Add(p_B);
            vertices.Add(p_C);

            int yMin = 0;
            foreach (var v in vertices)
            {
                if (v.Item2 < yMin)
                {
                    yMin = v.Item2;
                }
            }
            return yMin;
        }
    }
}
