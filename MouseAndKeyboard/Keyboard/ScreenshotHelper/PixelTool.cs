using ScreenshotHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.Remoting.Contexts;

namespace ScreenshotHelper
{
    public partial class PixelTool : Form
    {
        public PixelTool()
        {
            InitializeComponent();
        }



        private void BtnTakeScreenshotsAndCompare_Click(object sender, EventArgs e)
        {

            int upperLeftCornerX = int.Parse(TxtUpperLeftCornerCoordinateX.Text);
            int upperLeftCornerY = int.Parse(TxtUpperLeftCornerCoordinateY.Text);
            Point upperLeftCorner = new Point(upperLeftCornerX, upperLeftCornerY);

            int intWidth = int.Parse(TxtSizeWidth.Text);
            int intHeight = int.Parse(TxtSizeHeight.Text);
            Size sizeArea = new Size(intWidth, intHeight);

            List<Bitmap> bitmapList = new List<Bitmap>();
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(200);
                bitmapList.Add(ScreenshotHelper.GetCroppedScreenShot(upperLeftCorner, sizeArea));
            }

            // return CheckAreaWithTolerance(new Point(691, 393), new Size(5, 5), 210, 48, 32, 4);
            string ulcX = TxtUpperLeftCornerCoordinateX.Text;
            string ulcY = TxtUpperLeftCornerCoordinateY.Text;
            string w = TxtSizeWidth.Text;
            string h = TxtSizeHeight.Text;


            string result1 = " return CheckAreaWithTolerance(new Point("+ ulcX + ","+ ulcY + "), new Size("+w+","+h+"), ";
            string result2 = "";
            string result3 = "";
            int redMin = 255;
            int redMax = 0;
            int greenMin = 255;
            int greenMax = 0;
            int blueMin = 255;
            int blueMax = 0;

            for (int i = 0; i < bitmapList.Count; i++)
            {
                for (int x = 0; x < sizeArea.Width; x++)
                {
                    for (int y = 0; y < sizeArea.Height; y++)
                    {
                        Color c = bitmapList[i].GetPixel(x, y);
                        result2 =  c.R + ", " + c.G + ", " + c.B + ", tolerance);" + Environment.NewLine;
                        result3 += "(" + c.R + "," + c.G + "," + c.B + ")";

                        redMin   = c.R < redMin ? c.R : redMin;
                        redMax   = c.R > redMax ? c.R : redMax;
                        greenMin = c.G < greenMin ? c.G : greenMin;
                        greenMax = c.G > greenMax ? c.G : greenMax;
                        blueMin  = c.B < blueMin ? c.B : blueMin;
                        blueMax   = c.B > blueMax ? c.B : blueMax;
                    }
                }
                result3 += Environment.NewLine;
            }
            string result4 = string.Format("Red:{0}-{1} Green:{2}-{3} Blue:{4}-{5}", redMin, redMax, greenMin, greenMax, blueMin, blueMax);

            TxtOverview.Text = result1 +result2 + result3 + Environment.NewLine + result4;

        }
        bool running = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (running) { return; }
            running = true;

            Thread t = new Thread(new ThreadStart(updateCursorlocation));
            t.IsBackground = true;
            t.Start();

        }


        private void updateCursorlocation()
        {
            while (true)
            {
                string mouseLocation = Cursor.Position.X + "," + Cursor.Position.Y;
                WriteLabel(label1, mouseLocation);
                Thread.Sleep(1000/24);
            }
        }



        private void WriteTextBox(TextBox tboxPar, string insertTextPar)
        {
            if (tboxPar.InvokeRequired)
            {
                tboxPar.BeginInvoke((MethodInvoker)delegate () { tboxPar.Text = insertTextPar; ; });
                //  label1.Invalidate();
            }
            else
            {
                tboxPar.Text += insertTextPar;
                // label1.Invalidate();
            }

        }

        private void WriteLabel(Label lblPar, string insertTextPar)
        {
            if (lblPar.InvokeRequired)
            {
                lblPar.BeginInvoke((MethodInvoker)delegate () { lblPar.Text = insertTextPar; ; });
                //  label1.Invalidate();
            }
            else
            {
                lblPar.Text += insertTextPar;
                // label1.Invalidate();
            }

        }
    }
}
