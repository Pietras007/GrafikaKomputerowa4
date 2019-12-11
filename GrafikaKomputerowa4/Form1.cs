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
using System.Drawing.Imaging;

namespace GrafikaKomputerowa4
{
    public partial class Form1 : Form
    {
        int FOV = 120;
        List<Vertice> vertices = new List<Vertice>();
        Vertice v1 = new Vertice(0, 0, 0, 1);
        Vertice v2 = new Vertice(1, 0, 0, 1);
        Vertice v3 = new Vertice(1, 1, 0, 1);
        Vertice v4 = new Vertice(0, 1, 0, 1);
        Vertice v5 = new Vertice(0, 0, 1, 1);
        Vertice v6 = new Vertice(1, 0, 1, 1);
        Vertice v7 = new Vertice(1, 1, 1, 1);
        Vertice v8 = new Vertice(0, 1, 1, 1);
        Random random = new Random();
        public double[,] zBufor;

        List<Triangle> triangles1 = new List<Triangle>();
        List<Triangle> triangles2 = new List<Triangle>();
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

            for (int i = 0; i < pictureBox1.Width; i++)
            {
                for (int j = 0; j < pictureBox1.Height; j++)
                {
                    zBufor[i, j] = double.MinValue;
                }
            }

            //List<(float, float, float)> pp = new List<(float, float, float)>();
            //foreach(var v in vertices)
            //{
            //    pp.Add(vertexShader(mProjekcji(), lookAt, mModelu, v));
            //}

            var colorToPaint = new Color[pictureBox1.Width, pictureBox1.Height];
            for (int i = 0; i < triangles1.Count; i++)
            {
                g.PaintTriangle(colorToPaint, triangles1[i], zBufor);
            }

            for (int i = 0; i < triangles2.Count; i++)
            {
                g.PaintTriangle(colorToPaint, triangles2[i], zBufor);
            }

            for (int i = 0; i < triangles1.Count; i++)
            {
                triangles2[i].ppA = vertexShader(mProjekcji(), lookAt, mModelu2, triangles2[i].A);
                triangles2[i].ppB = vertexShader(mProjekcji(), lookAt, mModelu2, triangles2[i].B);
                triangles2[i].ppC = vertexShader(mProjekcji(), lookAt, mModelu2, triangles2[i].C);
            }

            using (Bitmap processedBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height))
            {
                unsafe
                {
                    BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);

                    int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
                    int heightInPixels = bitmapData.Height;
                    int widthInBytes = bitmapData.Width * bytesPerPixel;
                    byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                    Parallel.For(0, heightInPixels, y =>
                    {
                        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                        for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                        {
                                currentLine[x] = colorToPaint[x / 4, y].B;
                                currentLine[x + 1] = colorToPaint[x / 4, y].G;
                                currentLine[x + 2] = colorToPaint[x / 4, y].R;
                                currentLine[x + 3] = colorToPaint[x / 4, y].A;
                            
                        }
                    });
                    processedBitmap.UnlockBits(bitmapData);
                }

                g.DrawImage(processedBitmap, 0, 0);
            }


            for (int i=0;i<triangles1.Count;i++)
            {
                triangles1[i].ppA = vertexShader(mProjekcji(), lookAt, mModelu, triangles1[i].A);
                triangles1[i].ppB = vertexShader(mProjekcji(), lookAt, mModelu, triangles1[i].B);
                triangles1[i].ppC = vertexShader(mProjekcji(), lookAt, mModelu, triangles1[i].C);
            }

            for (int i = 0; i < triangles1.Count; i++)
            {
                triangles2[i].ppA = vertexShader(mProjekcji(), lookAt, mModelu2, triangles2[i].A);
                triangles2[i].ppB = vertexShader(mProjekcji(), lookAt, mModelu2, triangles2[i].B);
                triangles2[i].ppC = vertexShader(mProjekcji(), lookAt, mModelu2, triangles2[i].C);
            }

            //List<(int, int)> p_ = new List<(int, int)>();
            //foreach(var v in pp)
            //{
            //    p_.Add(pointToPixel(v.Item1, v.Item2));
            //}

            for (int i = 0; i < triangles1.Count; i++)
            {
                triangles1[i].p_A = pointToPixel(triangles1[i].ppA.Item1, triangles1[i].ppA.Item2);
                triangles1[i].p_B = pointToPixel(triangles1[i].ppB.Item1, triangles1[i].ppB.Item2);
                triangles1[i].p_C = pointToPixel(triangles1[i].ppC.Item1, triangles1[i].ppC.Item2);
            }

            for (int i = 0; i < triangles1.Count; i++)
            {
                triangles2[i].p_A = pointToPixel(triangles2[i].ppA.Item1, triangles2[i].ppA.Item2);
                triangles2[i].p_B = pointToPixel(triangles2[i].ppB.Item1, triangles2[i].ppB.Item2);
                triangles2[i].p_C = pointToPixel(triangles2[i].ppC.Item1, triangles2[i].ppC.Item2);
            }

            Pen pen = new Pen(Color.Black);
            //for (int i = 0; i < triangles1.Count; i++)
            //{
            //    g.PrintTriangle(pen, triangles1[i]);
            //}

            //for (int i = 0; i < triangles2.Count; i++)
            //{
            //    g.PrintTriangle(pen, triangles2[i]);
            //}
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
            zBufor = new double[pictureBox1.Width, pictureBox1.Height];

            trackBar1.Value = 160;
            Color color;
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles1.Add(new Triangle(v1, v2, v3, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles1.Add(new Triangle(v1, v3, v4, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles1.Add(new Triangle(v2, v6, v7, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles1.Add(new Triangle(v2, v7, v3, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles1.Add(new Triangle(v4, v3, v7, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles1.Add(new Triangle(v4, v7, v8, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles1.Add(new Triangle(v1, v2, v6, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles1.Add(new Triangle(v1, v6, v5, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles1.Add(new Triangle(v1, v5, v8, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles1.Add(new Triangle(v1, v8, v4, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles1.Add(new Triangle(v5, v6, v7, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles1.Add(new Triangle(v5, v7, v8, color));

            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles2.Add(new Triangle(v1, v2, v3, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles2.Add(new Triangle(v1, v3, v4, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles2.Add(new Triangle(v2, v6, v7, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles2.Add(new Triangle(v2, v7, v3, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles2.Add(new Triangle(v4, v3, v7, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles2.Add(new Triangle(v4, v7, v8, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles2.Add(new Triangle(v1, v2, v6, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles2.Add(new Triangle(v1, v6, v5, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles2.Add(new Triangle(v1, v5, v8, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles2.Add(new Triangle(v1, v8, v4, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles2.Add(new Triangle(v5, v6, v7, color));
            color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            triangles2.Add(new Triangle(v5, v7, v8, color));
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

        Matrix4x4 mModelu = new Matrix4x4(1, 0, 0, (float)0.1,
                        0, 1, 0, (float)0.2,
                        0, 0, 1, (float)0.3,
                        0, 0, 0, 1);

        Matrix4x4 mModelu2 = new Matrix4x4(1, 0, 0, (float)0.3,
                0, 1, 0, (float)0.2,
                0, 0, 1, (float)0.1,
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
            angle++;

            double angleRad = Math.PI * angle / 180;

            var cos = Math.Cos(angleRad);
            var sin = Math.Sin(angleRad);

            mModelu2.M11 = (float)cos;
            mModelu2.M13 = (float)-sin;
            mModelu2.M31 = (float)sin;
            mModelu2.M33 = (float)cos;

            mModelu.M11 = (float)cos;
            mModelu.M12 = (float)-sin;
            mModelu.M21 = (float)sin;
            mModelu.M22 = (float)cos;

            pictureBox1.Invalidate();
        }
    }
}
