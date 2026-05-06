using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;

namespace ScreenshotHelper
{
    public class ColorToleranceVars
    {
        public int redMin = 0;
        public int redMax = 255;
        public int bluemin = 0;
        public int blueMax = 255;
        public int greenMin = 0;
        public int greenMax = 255;
        public int howManyWhite = 0;
        public Point upperRightCorner;
        public int sizeX;
        public int sizeY;
    }

    public class PixelMatch
    {
        public Color matchColor;
        public Point pixelPosition;

        public PixelMatch(Point position, Color matchColorPar)
        {
            this.matchColor = matchColorPar;
            this.pixelPosition = position;
        }

        public PixelMatch(int x, int y, int red, int green, int blue)
        {
            this.pixelPosition = new Point(x, y);
            this.matchColor = Color.FromArgb(red, green, blue);
        }
    }

    public static class ScreenshotHelper
    {
        public static bool CheckPixelColor(Color pixelColor, int R, int G, int B)
        {
            return pixelColor.R == R && pixelColor.G == G && pixelColor.B == B;
        }
        public static Bitmap GetCroppedScreenShot(ColorToleranceVars vars)
        {
            return GetCroppedScreenShot(vars.upperRightCorner, vars.sizeX, vars.sizeY);
        }

        public static bool CheckSinglePixel(Point p, int R, int G, int B)
        {
            var pixel = GetCroppedScreenShot(p, 1, 1).GetPixel(0, 0);
            return CheckPixelColor(pixel, R, G, B);
        }


        public static void SaveFullScreenShot(string Filename)
        {
            GetFullScreenShot().Save(Filename);
        }

        public static void SaveCroppedScreenShot(Point start, int width, int height, string Filename)
        {
            GetCroppedScreenShot(start, width, height).Save(Filename);
        }
        public static Bitmap GetFullScreenShot()
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                return bitmap;
            }

        }

        public static Bitmap GetCroppedScreenShot(Point upperLeftCorner, int width, int height)
        {
            return GetCroppedScreenShot(upperLeftCorner, new Size(width, height));
        }
        public static Bitmap GetCroppedScreenShot(Point upperLeftCorner, Size size)
        {
            for (int x = 0; x < 1; x++)
            {
                try
                {
                    Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);

                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                        Bitmap bitmapCropped = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
                        for (int i = 0; i < size.Width; i++)
                        {
                            for (int j = 0; j < size.Height; j++)
                            {
                                //var color = bitmap.GetPixel(start.X + i, start.Y + j);
                                bitmapCropped.SetPixel(i, j, bitmap.GetPixel(upperLeftCorner.X + i, upperLeftCorner.Y + j));
                            }
                        }
                        // bitmapCropped.Save("ForTest.bmp");
                        if (bitmapCropped != null)
                            return bitmapCropped;
                        //else
                        //    Thread.Sleep(2000);
                    }
                }

                catch
                {
                    Thread.Sleep(100);
                }
            }
            Bitmap bitmapCropped2 = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            bitmapCropped2.SetPixel(0, 0, Color.Black);
            return bitmapCropped2;

        }

        public static Bitmap GetCroppedScreenShot2(Point point, int v1, int v2)
        {
            return GetCroppedScreenShot2(point, new Size(v1, v2));
        }
        public static bool DoPixelsMatch(List<PixelMatch> pixelMatches)
        {
            for (int i = 0; i < pixelMatches.Count; i++)
            {
                if (!DoPixelMatch(pixelMatches[i]))
                    return false;
            }

            return true;
        }
        public static bool DoPixelMatch(Point p, Color matchColor)
        {
            var pixel = GetSinglePixelFromScreen(p);
            if (pixel.R == matchColor.R && pixel.G == matchColor.G && pixel.B == matchColor.B)
                return true;
            else
                return false;
        }

        public static bool DoPixelMatch(PixelMatch pixelMatch)
        {
            return DoPixelMatch(pixelMatch.pixelPosition, pixelMatch.matchColor);
        }

        public static bool DoMultiplePixelMatch(List<PixelMatch> pixelMatches)
        {
            for (int i = 0; i < pixelMatches.Count; i++)
            {
                if (!DoPixelMatch(pixelMatches[i].pixelPosition, pixelMatches[i].matchColor))
                    return false;
            }
            return true;
        }

        public static Color GetSinglePixelFromScreen(Point p)
        {
            GC.Collect();
            try
            {
                return GetCroppedScreenShot2(p, 1, 1).GetPixel(0, 0);
            }
            catch
            {
                return Color.Blue;
            }
        }
        public static bool CheckSinglePixelOnScreen(Point p, int r, int g, int b)
        {
            var pixel = GetSinglePixelFromScreen(p);
            return pixel.R == r && pixel.B == b && pixel.G == g;
        }

        static bool GetGetCroppedScreenShot2Used = false;
        public static Bitmap GetCroppedScreenShot2(Point start, Size size)
        {

            while (GetGetCroppedScreenShot2Used)
                Thread.Sleep(10);
            GetGetCroppedScreenShot2Used = true;


            for (int x = 0; x < 1; x++)
            {
                try
                {
                    Bitmap bitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);

                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.CopyFromScreen(start.X, start.Y, 0, 0, size, CopyPixelOperation.SourceCopy);
                        
                        GetGetCroppedScreenShot2Used = false;
                        return bitmap;
                    }
                }

                catch
                {
                    Thread.Sleep(100);
                }
            }
            GetGetCroppedScreenShot2Used = false;
            return null;
        }

        public static Bitmap GetCroppedScreenShotOptimized(Point start, int width, int height)
        {

            try
            {
                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(start.X, start.Y, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
                    Bitmap bitmapCropped = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            //var color = bitmap.GetPixel(start.X + i, start.Y + j);
                            bitmapCropped.SetPixel(i, j, bitmap.GetPixel(start.X + i, start.Y + j));
                        }
                    }
                    // bitmapCropped.Save("ForTest.bmp");
                    if (bitmapCropped != null)
                        return bitmapCropped;
                    //else
                    //    Thread.Sleep(2000);
                }
            }

            catch
            {
                Thread.Sleep(100);
            }

            Bitmap bitmapCropped2 = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            bitmapCropped2.SetPixel(0, 0, Color.Black);
            return bitmapCropped2;

        }

        public static Bitmap MakeContrast(Bitmap source, ColorToleranceVars colorTolerance)
        {
            int counter = 0;
            var newBitmap = new Bitmap(source);
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    Color checkColor = source.GetPixel(x, y);
                    if (IsColorTolerated(checkColor, colorTolerance))
                    {
                        newBitmap.SetPixel(x, y, Color.White);
                        counter++;
                    }
                    else
                        newBitmap.SetPixel(x, y, Color.Black);
                }
            }
            colorTolerance.howManyWhite = counter;//String.Format("Der er {0} af {1} der ligger inden for tolerancen", counter.ToString(),((int)source.Height * source.Width).ToString());
            return newBitmap;
        }

        public static bool IsColorTolerated(Color checkColor, ColorToleranceVars colorTolerance)
        {
            return checkColor.R >= colorTolerance.redMin &&
                        checkColor.R <= colorTolerance.redMax &&
                        checkColor.B >= colorTolerance.bluemin &&
                        checkColor.B <= colorTolerance.blueMax &&
                        checkColor.G >= colorTolerance.greenMin &&
                        checkColor.G <= colorTolerance.greenMax;
        }


    }
}
