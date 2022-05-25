using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPS__L_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
            List<Rules> rules = new List<Rules>();

            // rules.Add(new Rules { p = "F",Z= "FF-F-F-F-FF" });
            //  string axi = "F-F-F-F";
            // Math.PI / 2  N=4 90

            //rules.Add(new Rules { p = "F",Z= "F+F--F+F" });
            //string axi = "F--F--F";
            //Math.PI / 3 N=4 60

            // ? rules.Add(new Rules { p = "F",Z= "F+f-FF+F+FF+Ff+FF-f+FF-F-FF-Ff-FFF" });
            //rules.Add(new Rules { p = "f", Z = "fffffff" });
            //string axi = "F+F+F+F";
            //Math.PI / 2 N=4 90

            rules.Add(new Rules { p = "X", Z = "FF-[-F]-[+F]" });
            rules.Add(new Rules { p = "F", Z = "FF+[+F-F]-[-F+F]" });

            string axi = "FX";

            string result = axi;
            for (int i=0; i < 5;i++)
            {
                for (int j = 0; j < rules.Count; j++)
                {
                    result = result.Replace(rules[j].p, rules[j].Z);
                }
            }
            DrawF(0, 0, 2, 45, result);
        }
        public void DrawF(double x, double y, int step, double angleOld, string rules)
        {
            Stack<PointF> points = new Stack<PointF>();
            double x1 = x;
            double y1 = y;
            double angle = 0;
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);        
                Graphics g = Graphics.FromImage(bitmap);
            g.TranslateTransform((float)pictureBox1.Height/2, (float)pictureBox1.Height/2); 
            foreach (char a in rules)
            {
                if (a.Equals('F') )
                {
                  
                    x1 += step * Math.Sin(angle);
                    y1 += step * Math.Cos(angle);

              
                    g.DrawLine(new Pen(Color.Blue), (float)x, (float)y, (float)x1, (float)y1);
                    x = x1;
                    y = y1;

                }
                if (a.Equals('f'))
                {
                    x1 += step * Math.Sin(angle);
                    y1 += step * Math.Cos(angle);

                    x = x1;
                    y = y1;
                }
                if (a.Equals('-'))
                {
                    angle -= angleOld * Math.PI / 180;
                }
                if (a.Equals('+'))
                {
                    angle += angleOld * Math.PI / 180;
                }
                if(a.Equals('['))
                {
                    points.Push(new PointF { X=(float)x1,Y=(float)y});
                }
                if(a.Equals(']'))
                {
                    PointF F = points.Pop();
                    x1 = F.X;
                    y1 = F.Y;
                    x = F.X;
                    y = F.Y;
                }
            }
            pictureBox1.Image = bitmap;
        }
    }
    class Rules
    {
        public string p { get; set; }
        public string Z { get; set; }
    }
}
