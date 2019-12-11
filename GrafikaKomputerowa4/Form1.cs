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
using GrafikaKomputerowa4.Models;
using GrafikaKomputerowa4.Extentions;

namespace GrafikaKomputerowa4
{
    public partial class Form1 : Form
    {
        int FOV = 45;
        List<Vertice> vertices = new List<Vertice>();
        Vertice v1 = new Vertice(0, 0, 0, 1);
        Vertice v2 = new Vertice(1, 0, 0, 1);
        Vertice v3 = new Vertice(1, 1, 0, 1);
        Vertice v4 = new Vertice(0, 1, 0, 1);
        Vertice v5 = new Vertice(0, 0, 1, 1);
        Vertice v6 = new Vertice(1, 0, 1, 1);
        Vertice v7 = new Vertice(1, 1, 1, 1);
        Vertice v8 = new Vertice(0, 1, 1, 1);
        int angle = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush brush = new SolidBrush(Color.White);
            g.FillRectangle(brush, 0, 0, pictureBox1.Width, pictureBox1.Height);
            List<(float, float, float)> pp = new List<(float, float, float)>();
            foreach(var v in vertices)
            {
                pp.Add(vertexShader(mProjekcji(), lookAt, mModelu, v));
            }

            //(float, float, float) pp1 = vertexShader(mProjekcji(), lookAt(), mModelu(), v1);
            //(float, float, float) pp2 = vertexShader(mProjekcji(), lookAt(), mModelu(), v2);
            //(float, float, float) pp3 = vertexShader(mProjekcji(), lookAt(), mModelu(), v3);
            //(float, float, float) pp4 = vertexShader(mProjekcji(), lookAt(), mModelu(), v4);
            //(float, float, float) pp5 = vertexShader(mProjekcji(), lookAt(), mModelu(), v5);
            //(float, float, float) pp6 = vertexShader(mProjekcji(), lookAt(), mModelu(), v6);
            //(float, float, float) pp7 = vertexShader(mProjekcji(), lookAt(), mModelu(), v7);
            //(float, float, float) pp8 = vertexShader(mProjekcji(), lookAt(), mModelu(), v8);

            List<(int, int)> p_ = new List<(int, int)>();
            foreach(var v in pp)
            {
                p_.Add(pointToPixel(v.Item1, v.Item2));
            }

            Pen pen = new Pen(Color.Black);
            //SolidBrush blueBrush = new SolidBrush(Color.Yellow);
            //g.FillBetweenPoints(blueBrush, new (int, int)[] { p_[0], p_[1], p_[2], p_[3] });
            //g.FillBetweenPoints(blueBrush, new (int, int)[] { p_[4], p_[5], p_[6], p_[7] });
            //g.FillBetweenPoints(blueBrush, new (int, int)[] { p_[0], p_[1], p_[5], p_[4] });
            //g.FillBetweenPoints(blueBrush, new (int, int)[] { p_[1], p_[5], p_[6], p_[2] });
            //g.FillBetweenPoints(blueBrush, new (int, int)[] { p_[2], p_[6], p_[7], p_[3] });
            //g.FillBetweenPoints(blueBrush, new (int, int)[] { p_[3], p_[7], p_[2], p_[0] });

            for (int i = 0; i < 4; i++)
            {
                g.DrawLineBetweenPoints(pen, p_[i], p_[(i + 1) % 4]);
                g.DrawLineBetweenPoints(pen, p_[i + 4], p_[(i + 1) % 4 + 4]);
                g.DrawLineBetweenPoints(pen, p_[i], p_[i + 4]);
            }

            //for(int i=4;i<4;i++)
            //{
            //    g.DrawLineBetweenPoints(pen, p_[i], p_[i+4]);
            //}
            //g.DrawLineBetweenPoints(pen, p_[0], p_[1]);
            //var p1_ = pointToPixel(pp1.Item1, pp1.Item2);
            //var p2_ = pointToPixel(pp2.Item1, pp2.Item2);
            //var p3_ = pointToPixel(pp3.Item1, pp3.Item2);
            //var p4_ = pointToPixel(pp4.Item1, pp4.Item2);
            //var p5_ = pointToPixel(pp5.Item1, pp5.Item2);
            //var p6_ = pointToPixel(pp6.Item1, pp6.Item2);
            //var p7_ = pointToPixel(pp7.Item1, pp7.Item2);
            //var p8_ = pointToPixel(pp8.Item1, pp8.Item2);

            //g.DrawLine(pen, p1_.Item1, p1_.Item2, p2_.Item1, p2_.Item2);
            //g.DrawLine(pen, p2_.Item1, p2_.Item2, p3_.Item1, p3_.Item2);
            //g.DrawLine(pen, p3_.Item1, p3_.Item2, p4_.Item1, p4_.Item2);
            //g.DrawLine(pen, p4_.Item1, p4_.Item2, p1_.Item1, p1_.Item2);

            //g.DrawLine(pen, p5_.Item1, p5_.Item2, p6_.Item1, p6_.Item2);
            //g.DrawLine(pen, p6_.Item1, p6_.Item2, p7_.Item1, p7_.Item2);
            //g.DrawLine(pen, p7_.Item1, p7_.Item2, p8_.Item1, p8_.Item2);
            //g.DrawLine(pen, p8_.Item1, p8_.Item2, p5_.Item1, p5_.Item2);

            //g.DrawLine(pen, p1_.Item1, p1_.Item2, p5_.Item1, p5_.Item2);
            //g.DrawLine(pen, p2_.Item1, p2_.Item2, p6_.Item1, p6_.Item2);
            //g.DrawLine(pen, p3_.Item1, p3_.Item2, p7_.Item1, p7_.Item2);
            //g.DrawLine(pen, p4_.Item1, p4_.Item2, p8_.Item1, p8_.Item2);


        }

        private (float, float, float) vertexShader(Matrix4x4 mProj, Matrix4x4 lookAt, Matrix4x4 mModel, Vertice p)
        {
            var res = Matrix4x4.Multiply(Matrix4x4.Multiply(mProj, lookAt), mModel);
            Matrix4x4 a1 = Matrix4x4.Multiply(res, p.matrix4X4);
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
            trackBar1.Value = 45;
            vertices.Add(new Vertice(0, 0, 0, 1));
            vertices.Add(new Vertice(1, 0, 0, 1));
            vertices.Add(new Vertice(1, 1, 0, 1));
            vertices.Add(new Vertice(0, 1, 0, 1));
            vertices.Add(new Vertice(0, 0, 1, 1));
            vertices.Add(new Vertice(1, 0, 1, 1));
            vertices.Add(new Vertice(1, 1, 1, 1));
            vertices.Add(new Vertice(0, 1, 1, 1));
        }

        Matrix4x4 lookAt = //mView
            new Matrix4x4(0, 1, 0, (float)-0.5,
                0, 0, 1, (float)-0.5,
                1, 0, 0, -3,
                0, 0, 0, 1);

        //Matrix4x4 lookAt = //mView
        //    new Matrix4x4(0, 1, 0, (float)0,
        //        0, 0, 1, (float)0,
        //        1, 0, 0, -3,
        //        0, 0, 0, 1);

        Matrix4x4 mModelu = new Matrix4x4(1, 0, 0, (float)0,
                        0, 1, 0, (float)0,
                        0, 0, 1, (float)0,
                        0, 0, 0, 1);

        private Matrix4x4 mProjekcji()
        {
            float n = 1;
            float f = 100;
            double e = 1.0 / Math.Tan(FOV/2.0/180.0*Math.PI);
            double a = pictureBox1.Height / (double)pictureBox1.Width;

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            //angle++;

            //double angleRad = Math.PI * angle / 180;

            //var cos = Math.Cos(angleRad);
            //var sin = Math.Sin(angleRad);

            //mModelu.M22 = (float)cos;
            //mModelu.M23 = (float)-sin;
            //mModelu.M32 = (float)sin;
            //mModelu.M33 = (float)cos;

            //mModelu.M11 = (float)cos;
            //mModelu.M12 = (float)-sin;
            //mModelu.M21 = (float)sin;
            //mModelu.M22 = (float)cos;

            //pictureBox1.Invalidate();
        }
    }
}
