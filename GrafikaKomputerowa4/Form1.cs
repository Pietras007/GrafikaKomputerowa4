using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace GrafikaKomputerowa4
{
    public partial class Form1 : Form
    {
        int FOV = 45;
        Matrix4x4 p1 = new Matrix4x4(0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0,
            1, 0, 0, 0);
        Matrix4x4 p2 = new Matrix4x4(1,0,0,0,
            0,0,0,0,
            0,0,0,0,
            1,0,0,0);
        Matrix4x4 p3 = new Matrix4x4(1, 0, 0, 0,
            1, 0, 0, 0,
            0, 0, 0, 0,
            1, 0, 0, 0);
        Matrix4x4 p4 = new Matrix4x4(0, 0, 0, 0,
            1, 0, 0, 0,
            0, 0, 0, 0,
            1, 0, 0, 0);
        Matrix4x4 p5 = new Matrix4x4(0, 0, 0, 0,
            0, 0, 0, 0,
            1, 0, 0, 0,
            1, 0, 0, 0);
        Matrix4x4 p6 = new Matrix4x4(1, 0, 0, 0,
            0, 0, 0, 0,
            1, 0, 0, 0,
            1, 0, 0, 0);
        Matrix4x4 p7 = new Matrix4x4(1, 0, 0, 0,
            1, 0, 0, 0,
            1, 0, 0, 0,
            1, 0, 0, 0);
        Matrix4x4 p8 = new Matrix4x4(0, 0, 0, 0,
            1, 0, 0, 0,
            1, 0, 0, 0,
            1, 0, 0, 0);
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //var res = Matrix4x4.Multiply(Matrix4x4.Multiply(mProjekcji(), lookAt()), mModelu());
            //Matrix4x4 a1 = Matrix4x4.Multiply(res, p1);
            //Matrix4x4 a2 = Matrix4x4.Multiply(res, p2);
            //Matrix4x4 a3 = Matrix4x4.Multiply(res, p3);
            //Matrix4x4 a4 = Matrix4x4.Multiply(res, p4);
            //Matrix4x4 a5 = Matrix4x4.Multiply(res, p5);
            //Matrix4x4 a6 = Matrix4x4.Multiply(res, p6);
            //Matrix4x4 a7 = Matrix4x4.Multiply(res, p7);
            //Matrix4x4 a8 = Matrix4x4.Multiply(res, p8);


            //Matrix4x4 x1 = Matrix4x4.Multiply(a1, (float)(1.0 / a1.M41));
            //Matrix4x4 x2 = Matrix4x4.Multiply(a2, (float)(1.0 / a2.M41));
            //Matrix4x4 x3 = Matrix4x4.Multiply(a3, (float)(1.0 / a3.M41));
            //Matrix4x4 x4 = Matrix4x4.Multiply(a4, (float)(1.0 / a4.M41));
            //Matrix4x4 x5 = Matrix4x4.Multiply(a5, (float)(1.0 / a5.M41));
            //Matrix4x4 x6 = Matrix4x4.Multiply(a6, (float)(1.0 / a6.M41));
            //Matrix4x4 x7 = Matrix4x4.Multiply(a7, (float)(1.0 / a7.M41));
            //Matrix4x4 x8 = Matrix4x4.Multiply(a8, (float)(1.0 / a8.M41));

            (float, float, float) pp1 = vertexShader(mProjekcji(), lookAt(), mModelu(), p1);
            (float, float, float) pp2 = vertexShader(mProjekcji(), lookAt(), mModelu(), p2);
            (float, float, float) pp3 = vertexShader(mProjekcji(), lookAt(), mModelu(), p3);
            (float, float, float) pp4 = vertexShader(mProjekcji(), lookAt(), mModelu(), p4);
            (float, float, float) pp5 = vertexShader(mProjekcji(), lookAt(), mModelu(), p5);
            (float, float, float) pp6 = vertexShader(mProjekcji(), lookAt(), mModelu(), p6);
            (float, float, float) pp7 = vertexShader(mProjekcji(), lookAt(), mModelu(), p7);
            (float, float, float) pp8 = vertexShader(mProjekcji(), lookAt(), mModelu(), p8);

            Pen pen = new Pen(Color.Black);
            var p1_ = pointToPixel(pp1.Item1, pp1.Item2);
            var p2_ = pointToPixel(pp2.Item1, pp2.Item2);
            var p3_ = pointToPixel(pp3.Item1, pp3.Item2);
            var p4_ = pointToPixel(pp4.Item1, pp4.Item2);
            var p5_ = pointToPixel(pp5.Item1, pp5.Item2);
            var p6_ = pointToPixel(pp6.Item1, pp6.Item2);
            var p7_ = pointToPixel(pp7.Item1, pp7.Item2);
            var p8_ = pointToPixel(pp8.Item1, pp8.Item2);

            g.DrawLine(pen, p1_.Item1, p1_.Item2, p2_.Item1, p2_.Item2);
            g.DrawLine(pen, p2_.Item1, p2_.Item2, p3_.Item1, p3_.Item2);
            g.DrawLine(pen, p3_.Item1, p3_.Item2, p4_.Item1, p4_.Item2);
            g.DrawLine(pen, p4_.Item1, p4_.Item2, p1_.Item1, p1_.Item2);

            g.DrawLine(pen, p5_.Item1, p5_.Item2, p6_.Item1, p6_.Item2);
            g.DrawLine(pen, p6_.Item1, p6_.Item2, p7_.Item1, p7_.Item2);
            g.DrawLine(pen, p7_.Item1, p7_.Item2, p8_.Item1, p8_.Item2);
            g.DrawLine(pen, p8_.Item1, p8_.Item2, p5_.Item1, p5_.Item2);

            g.DrawLine(pen, p1_.Item1, p1_.Item2, p5_.Item1, p5_.Item2);
            g.DrawLine(pen, p2_.Item1, p2_.Item2, p6_.Item1, p6_.Item2);
            g.DrawLine(pen, p3_.Item1, p3_.Item2, p7_.Item1, p7_.Item2);
            g.DrawLine(pen, p4_.Item1, p4_.Item2, p8_.Item1, p8_.Item2);


        }

        private (float, float, float) vertexShader(Matrix4x4 mProj, Matrix4x4 lookAt, Matrix4x4 mModel, Matrix4x4 p)
        {
            var res = Matrix4x4.Multiply(Matrix4x4.Multiply(mProj, lookAt), mModel);
            Matrix4x4 a1 = Matrix4x4.Multiply(res, p);
            Matrix4x4 x1 = Matrix4x4.Multiply(a1, (float)(1.0 / a1.M41));
            return (x1.M11, x1.M21, x1.M31);
        }

        private (int,int) pointToPixel(float pointX, float pointY)
        {
            int width = pictureBox1.Width;
            int height = pictureBox1.Height;
            float px = pointX + 1;
            float py = pointY + 1;
            return ((int)(px*width/2.0), (int)(py * height / 2.0));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private Matrix4x4 lookAt()//mView
        {
            return new Matrix4x4(0,1,0,(float)-0.5,
                0,0,1, (float)-0.5,
                1,0,0,-3,
                0,0,0,1);
        }

        private Matrix4x4 mModelu()
        {
            return new Matrix4x4(1, 0, 0, 0,
                        0, 1, 0, 0,
                        0, 0, 1, 0,
                        0, 0, 0, 1);
        }

        private Matrix4x4 mProjekcji()
        {
            float n = 1;
            float f = 100;
            double e = 1.0 / Math.Tan(FOV/2.0/180.0*Math.PI);
            double a = pictureBox1.Height / (double)pictureBox1.Width;

            //            return new Matrix4x4((float)2.414, 0, 0, 0,
            //0, (float)2.414, 0, 0,
            //0, 0, (float)-1.02, (float)-2.02,
            //0, 0, -1, 0);
            return new Matrix4x4((float)e, 0, 0, 0,
                0, (float)(e / a), 0, 0,
                0, 0, (float)((f + n) / (f - n)), (float)((2 * f * n) / (f - n)),
                0, 0, -1, 0);

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            FOV = trackBar1.Value;
            pictureBox1.Invalidate();
        }
    }
}
