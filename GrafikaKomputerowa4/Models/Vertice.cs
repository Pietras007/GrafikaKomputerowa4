using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaKomputerowa4.Models
{
    public class Vertice
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float A { get; set; }
        public Matrix4x4 matrix4X4 { get; set; }

        public Vertice(float x, float y, float z, float a)
        {
            X = x;
            Y = y;
            Z = z;
            A = a;
            matrix4X4 = new Matrix4x4(
                x,0,0,0,
                y,0,0,0,
                z,0,0,0,
                a,0,0,0
            );
        }
    }
}
