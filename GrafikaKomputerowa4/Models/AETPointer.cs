using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaKomputerowa4.Models
{
    public class AETPointer
    {
        public int Ymax { get; set; }
        public double X { get; set; }
        public double m { get; set; }

        public AETPointer(int _Ymax, double _X, double _m)
        {
            Ymax = _Ymax;
            X = _X;
            m = _m;
        }

        public AETPointer((int, int) A, (int, int) B)
        {
            if (A.Item2 > B.Item2)
            {
                Ymax = (int)A.Item2;
                X = B.Item1;
            }
            else
            {
                Ymax = (int)B.Item2;
                X = A.Item1;
            }

            if ((B.Item2 > A.Item2 && B.Item1 > A.Item1) || (B.Item2 < A.Item2 && B.Item1 < A.Item1))
            {
                m = 1 / (Math.Abs(B.Item2 - A.Item2) / (double)Math.Abs(B.Item1 - A.Item1));
            }
            else
            {
                m = -1 / (Math.Abs(B.Item2 - A.Item2) / (double)Math.Abs(B.Item1 - A.Item1));
            }
        }
    }
}
