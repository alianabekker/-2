using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using SharpGL.Enumerations;
using MetroFramework.Components;
using MetroFramework.Forms;

namespace grfPGZ
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
            
            OpenGL gl = openGLControl1.OpenGL;

            gl.Enable(OpenGL.GL_TEXTURE_2D);

            
        }

        Texture tex = new Texture();

        public double R = 2.0f;
        public double lookR = 5.0f;
        public int mode = -1;
        public int fi = 0;
        public double cx = 0, cy = 0, cz = 0;
        public static float lx = 0, ly = 0, lz = 0;
        public double lookx = 8.0f, looky = 0f, lookz = 0;

        public float[] mat_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
        public  float[] mat_shininess = { 50.0f };
        public  float[] Glob_amb = { 0.5f, 0.5f, 0.5f, 1.0f };

        public void projection()//Аксонометрическая проекция
        {
            OpenGL gl = openGLControl1.OpenGL;
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Ortho(-1, 1, -1, 1, 0.5, 20);
            gl.LookAt(0, 0, -2.5, 0, 0, 0, 0, 1, 0);
            polygon(12, 12, gl);
            gl.LoadIdentity();
            gl.MatrixMode(OpenGL.GL_MODELVIEW);

        }
        public void Perspective()//Перспективная проекция
        {
            OpenGL gl = openGLControl1.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            gl.LoadIdentity();
            gl.Perspective(100, 1, 18, 100);
            gl.LookAt(0, 0, -15, 0, 0, 0, 0, 1, 0);

            polygon(12, 12, gl);

            gl.LoadIdentity();
            gl.MatrixMode(OpenGL.GL_MODELVIEW);

        }
        public void polygon(float dB, float dL, OpenGL gl)
        {
            gl.LoadIdentity();

            gl.Color(0.0f, 0.5f, 0.2f);
            gl.Rotate(90 + fi, 1, 0, 0);
            gl.Rotate(180, 0, 1, 0);

            double B, L;
            double[] x, y, z;
         
            x = new double[4];
            y = new double[4];
            z = new double[4];
     
            for (B = -90; B < 90; B += dB)
            {
                for (L = 0; L <= 360; L += dL)
                {
                    
                    x[0] = R * Math.Cos(B * 3.14 / 180) * Math.Sin(L * 3.14 / 180) + cx;
                    y[0] = R * Math.Cos(B * 3.14 / 180) * Math.Cos(L * 3.14 / 180) + cy;
                    z[0] = R * Math.Sin(B * 3.14 / 180);
                    if (z[0] > 0)
                        z[0] = R - 0.5 * z[0] + cz;
                    else
                        z[0] += cz;

                    x[1] = R * Math.Cos((B + dB) * 3.14 / 180) * Math.Sin(L * 3.14 / 180) + cx;
                    y[1] = R * Math.Cos((B + dB) * 3.14 / 180) * Math.Cos(L * 3.14 / 180) + cy;
                    z[1] = R * Math.Sin((B + dB) * 3.14 / 180);
                    if (z[1] > 0)
                        z[1] = R - 0.5 * z[1] + cz;
                    else
                        z[1] += cz;


                    x[2] = R * Math.Cos((B + dB) * 3.14 / 180) * Math.Sin((L + dL) * 3.14 / 180) + cx;
                    y[2] = R * Math.Cos((B + dB) * 3.14 / 180) * Math.Cos((L + dL) * 3.14 / 180) + cy;
                    z[2] = R * Math.Sin((B + dB) * 3.14 / 180);
                    if (z[2] > 0)
                        z[2] = R - 0.5 * z[2] + cz;
                    else
                        z[2] += cz;


                    x[3] = R * Math.Cos(B * 3.14 / 180) * Math.Sin((L + dL) * 3.14 / 180) + cx;
                    y[3] = R * Math.Cos(B * 3.14 / 180) * Math.Cos((L + dL) * 3.14 / 180) + cy;
                    z[3] = R * Math.Sin(B * 3.14 / 180);
                    if (z[3] > 0)
                        z[3] = R - 0.5 * z[3] + cz;
                    else
                        z[3] += cz;


                    gl.Begin(OpenGL.GL_POLYGON);
                    gl.Vertex(x[0], y[0], z[0]);
                    gl.Vertex(x[1], y[1], z[1]);
                    gl.Vertex(x[2], y[2], z[2]);
                    gl.Vertex(x[3], y[3], z[3]);
                    gl.End();
                }
            }
            gl.LoadIdentity();
        }
        public void carcas_tmp(float dB, float dL, OpenGL gl)
        {
            double x, y, z, B, L;
       
            gl.LoadIdentity();

            gl.Rotate(90, 1, 0, 0);         
            gl.Rotate(180, 0, 1, 0);

            gl.Begin(OpenGL.GL_LINE_STRIP);
            for (L = 0; L <= 360; L += dL)
            {
                gl.Begin(OpenGL.GL_LINE_STRIP);
                for (B = -90; B <= 90; B += dB)
                {
                    x = R * Math.Cos(B * 3.14 / 180) * Math.Sin(L * 3.14 / 180) + cx;
                    y = R * Math.Cos(B * 3.14 / 180) * Math.Cos(L * 3.14 / 180) + cy;
                    z = R * Math.Sin(B * 3.14 / 180);
                    if (z > 0) 
                        z = R - 0.5 * z + cz;
                    else 
                        z += cz;
                    gl.Vertex(x, y, z);
                }
                gl.End();
            }

            for (B = -90; B <= 90; B += dB)
            {
                gl.Begin(OpenGL.GL_LINE_STRIP);
                for (L = 0; L <= 360; L += dL)
                {
                    x = R * Math.Cos(B * 3.14 / 180) * Math.Sin(L * 3.14 / 180) + cx;
                    y = R * Math.Cos(B * 3.14 / 180) * Math.Cos(L * 3.14 / 180) + cy;
                    z = R * Math.Sin(B * 3.14 / 180);
                    if (z > 0)
                        z = R - 0.5 * z + cz;
                    else
                        z += cz;
                    gl.Vertex(x, y, z);
                }
                gl.End();
            }

            gl.LoadIdentity();

        }
        public void polygon_tex(float dB, float dL, OpenGL gl)
        {
            gl.LoadIdentity();
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Enable(OpenGL.GL_NORMALIZE);

            double B, L;
            double[] x, y, z;
            double ix = 0, iy = 0, 
                   iwx = 1 / (180 / dB), 
                   iwy = 1 / (360 / dL);

         
            tex.Bind(gl);

            gl.Rotate(90, 1, 0, 0);
            gl.Rotate(180, 0, 1, 0);
                      
            x = new double[4];
            y = new double[4];
            z = new double[4];
  
            for (B = -90; B < 90; B += dB)
            {
                for (L = 0; L <= 360; L += dL)
                {

                    x[0] = R * Math.Cos(B * 3.14 / 180) * Math.Sin(L * 3.14 / 180) + cx;
                    y[0] = R * Math.Cos(B * 3.14 / 180) * Math.Cos(L * 3.14 / 180) + cy;
                    z[0] = R * Math.Sin(B * 3.14 / 180);
                    if (z[0] > 0) 
                        z[0] = R - 0.5 * z[0] + cz;
                    else 
                        z[0] += cz;

                    x[1] = R * Math.Cos((B + dB) * 3.14 / 180) * Math.Sin(L * 3.14 / 180) + cx;
                     y[1] = R * Math.Cos((B + dB) * 3.14 / 180) * Math.Cos(L * 3.14 / 180) + cy;
                     z[1] = R * Math.Sin((B + dB) * 3.14 / 180);
                    if (z[1] > 0)
                        z[1] = R - 0.5 * z[1] + cz;
                    else
                        z[1] += cz;

                    x[2] = R * Math.Cos((B + dB) * 3.14 / 180) * Math.Sin((L + dL) * 3.14 / 180) + cx;
                    y[2] = R * Math.Cos((B + dB) * 3.14 / 180) * Math.Cos((L + dL) * 3.14 / 180) + cy;
                    z[2] = R * Math.Sin((B + dB) * 3.14 / 180);
                    if (z[2] > 0)
                        z[2] = R - 0.5 * z[2] + cz;
                    else
                        z[2] += cz;

                    x[3] = R * Math.Cos(B * 3.14 / 180) * Math.Sin((L + dL) * 3.14 / 180) + cx;
                    y[3] = R * Math.Cos(B * 3.14 / 180) * Math.Cos((L + dL) * 3.14 / 180) + cy;
                    z[3] = R * Math.Sin(B * 3.14 / 180);
                    if (z[3] > 0)
                        z[3] = R - 0.5 * z[3] + cz;
                    else
                        z[3] += cz;

                    gl.Begin(OpenGL.GL_POLYGON);
                    gl.TexCoord(ix, iy); gl.Vertex(x[0], y[0], z[0]);
                    gl.TexCoord(ix, iy + iwy); gl.Vertex(x[1], y[1], z[1]);
                    gl.TexCoord(ix + iwx, iy + iwy); gl.Vertex(x[2], y[2], z[2]);
                    gl.TexCoord(ix + iwx, iy); gl.Vertex(x[3], y[3], z[3]);
                    gl.End();
                    ix += iwx;
                }
                iy += iwy;
                ix = 0;
            }

            gl.LoadIdentity();

            gl.Disable(OpenGL.GL_DEPTH_TEST);
            gl.Disable(OpenGL.GL_NORMALIZE);
        }
        private void OpenGLControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            OpenGL gl = openGLControl1.OpenGL;

            switch (e.KeyChar)
            {
                case 'a':
                    cy+= 0.1;
                    break;

                case 's':
                    cz-=0.1;
                    break;

                case 'd':
                    cy-=0.1;
                    break;

                case 'w':
                    cz+=0.1;
                    break;

                case 'q':
                   cx -= 0.1;
                    break;

                 case 'e':
                    cx += 0.1;
                    break;


                case 'g':
                    ly += 0.1f;
                    break;

                case 'y':
                    lz += 0.1f;
                    break;

                case 'j':
                    ly -= 0.1f;
                    break;

                case 'h':
                    lz -= 0.1f;
                    break;



                default:
                    break;
            }

           

      }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            switch (this.comboBox1.SelectedIndex)
            {
                case 0:
                    mode = 0;
                    break;
                case 1:
                    mode = 1;
                    break;
                case 2:
                    mode = 2;
                    break;
                case 3:
                    mode = 3;
                    break;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void OpenGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            OpenGL gl = openGLControl1.OpenGL;

            float[] lpos0 = { 5.0f + lx, 5.0f + ly, -5.0f + lz, 1.0f };

            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, lpos0);

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            
            gl.Color(1.0f, 1.0f, 1.0f);

            switch (mode)
            {
                case 0:
                    carcas_tmp(12, 12, gl);
                    break;
                case 1:
                    polygon_tex(12, 12, gl);
                    break;
                case 2:
                    polygon(12, 12, gl); 
                    break;
                case 3:
                    polygon(12, 12, gl);
                    carcas_tmp(12, 12, gl);
                    break;
                case 4:
                    projection();
                    break;
                case 5:
                    Perspective();
                    break;
            }

            gl.PopMatrix();
           // gl.Flush();
            gl.End();
        }
        private void OpenGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl1.OpenGL;
            
            gl.ClearColor(0, 0, 0, 0);

            gl.Ortho(-15, 15, -15, 15, -25, 25);

            gl.LookAt(lookx, looky, lookz, 0, 0, 0, 0, 1, 0);
            
            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, Glob_amb);
            gl.Material(OpenGL.GL_FRONT, OpenGL.GL_SPECULAR, mat_specular);
            gl.Material(OpenGL.GL_FRONT, OpenGL.GL_SHININESS, mat_shininess);
            
            gl.Enable(OpenGL.GL_LIGHTING);
            gl.Enable(OpenGL.GL_LIGHT0);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Enable(OpenGL.GL_COLOR_MATERIAL);

            gl.ShadeModel(OpenGL.GL_SMOOTH);
        }
        private void OpenGLControl1_Resized(object sender, EventArgs e)
        {            
            OpenGL gl = openGLControl1.OpenGL;

            gl.MatrixMode(OpenGL.GL_PROJECTION);

            gl.LoadIdentity();
           
            gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);
                
            gl.LookAt(lookx, looky, lookz, 0, 0, 0, 0, 1, 0);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }
    }
}
