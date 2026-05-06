using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace ScreenshotHelper
{
    enum LineDirection { Horizontal, Vertical }

    public class PixelLine
    {
        public Point upperLeftCorner;
        LineDirection lineDirection;
        public const string PIXEL_LINE_FOLDER = @"pixellines\";
        public List<Color> colorLine = new List<Color>();
        public int PixelLineLength { get { return colorLine.Count; } }

        public static PixelLine ReadPixelLineFromFile(string filename)
        {
            PixelLine newPixelLine = new PixelLine();
            Dictionary<string, string> dictionary = GetDictionaryFromFile(PIXEL_LINE_FOLDER + filename);
            SetUpperLeftCorner(newPixelLine, dictionary);
            SetLineDirection(newPixelLine, dictionary);
            SetColorLine(newPixelLine, dictionary);
            return newPixelLine;
        }

        public static int HowManyMatchInBitmapToColor(Bitmap bitmap, Color matchColor)
        {
            int matchCount = 0;
            for (int i = 0; i < bitmap.Width; i++)
            {
                Color bitmapColor = bitmap.GetPixel(i, 0);
                if (matchColor.R == bitmapColor.R &&
                    matchColor.G == bitmapColor.G &&
                    matchColor.B == bitmapColor.B)
                    matchCount++;
            }
            return matchCount;
        }

        private static void SetColorLine(PixelLine newPixelLine, Dictionary<string, string> dictionary)
        {
            int numberOfColors = int.Parse(GetValue(dictionary["NumberOfLinesWithPixelColors"]));
            List<Color> newColors = new List<Color>();
            for (int i = 0; i < numberOfColors; i++)
            {
                string[] colorLine = dictionary["Line" + i.ToString()].Split(',');
                int red = int.Parse(colorLine[0]);
                int green = int.Parse(colorLine[1]);
                int blue = int.Parse(colorLine[2]);

                Color newColor = Color.FromArgb(red, green, blue);
                newColors.Add(newColor);
            }
            newPixelLine.colorLine = newColors;
        }

        public static bool DoPixelLineMatchBitmap(Bitmap bitmap, PixelLine pixelLine)
        {
            for (int i = 0; i < pixelLine.colorLine.Count; i++)
            {
                if (pixelLine.lineDirection == LineDirection.Horizontal)
                {
                    Color colorLineColor = pixelLine.colorLine[i];
                    Color bitmapColor = bitmap.GetPixel(i, 0);
                    if (colorLineColor.R != bitmapColor.R ||
                        colorLineColor.G != bitmapColor.G ||
                        colorLineColor.B != bitmapColor.B)
                        return false;
                }
            }
            return true;
        }

        private static void SetLineDirection(PixelLine newPixelLine, Dictionary<string, string> dictionary)
        {
            string direction = GetValue(dictionary["Horizontal or Vertical"]);
            if (direction == "Horizontal")
            {
                newPixelLine.lineDirection = LineDirection.Horizontal;
                return;
            }
            if (direction == "Vertical")
            {
                newPixelLine.lineDirection = LineDirection.Vertical;
                return;
            }
        }

        private static void SetUpperLeftCorner(PixelLine newPixelLine, Dictionary<string, string> dictionary)
        {
            int upperLeftCornerX = int.Parse(GetValue(dictionary["UpperLeftCorner X"]));
            int upperLeftCornerY = int.Parse(GetValue(dictionary["UpperLeftCorner Y"]));
            newPixelLine.upperLeftCorner = new Point(upperLeftCornerX, upperLeftCornerY);
        }

        private static Dictionary<string, string> GetDictionaryFromFile(string filename)
        {
            string[] fileLines = File.ReadAllLines(filename);
            Dictionary<string, string> fileToDictionary = new Dictionary<string, string>();
            for (int i = 0; i < fileLines.Length; i++)
            {
                if (fileLines[i].IndexOf(':') != -1)
                {
                    string stringKey = fileLines[i].Substring(0, fileLines[i].IndexOf(':'));
                    string stringValue = fileLines[i].Substring(fileLines[i].IndexOf(':') + 1);
                    fileToDictionary.Add(stringKey, stringValue);
                }
            }
            return fileToDictionary;
        }
        private static string GetValue(string fullstring)
        {
            return fullstring.Substring(fullstring.IndexOf(':') + 1);
        }
    }
}
