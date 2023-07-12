//C# Code:

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;


namespace Matoapp.Infrastructure.Utils
{
    public class ColorUtil
    {
        public static Color GetRandomColor()
        {
            Random randomNum_1 = new Random(Guid.NewGuid().GetHashCode());
            System.Threading.Thread.Sleep(randomNum_1.Next(1));
            int int_Red = randomNum_1.Next(255);

            Random randomNum_2 = new Random((int)DateTime.Now.Ticks);
            int int_Green = randomNum_2.Next(255);

            Random randomNum_3 = new Random(Guid.NewGuid().GetHashCode());

            int int_Blue = randomNum_3.Next(255);
            int_Blue = (int_Red + int_Green > 380) ? int_Red + int_Green - 380 : int_Blue;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;


            return GetDarkerColor(Color.FromRgb(ParseByte(int_Red), ParseByte(int_Green), ParseByte(int_Blue)));
        }

        //获取加深颜色
        public static Color GetDarkerColor(Color color)
        {
            const int max = 255;
            int increase = new Random(Guid.NewGuid().GetHashCode()).Next(30, 255); //还可以根据需要调整此处的值

            var rgb = ToColor(color.ToString());
            int r = Math.Abs(Math.Min(rgb.R - increase, max));
            int g = Math.Abs(Math.Min(rgb.G - increase, max));
            int b = Math.Abs(Math.Min(rgb.B - increase, max));


            return Color.FromRgb(byte.Parse(r.ToString()), byte.Parse(g.ToString()), byte.Parse(b.ToString()));
        }

        private static byte ParseByte(int value)
        {
            return byte.Parse(value.ToString());
        }


        public static Argb32 ToColor(string colorHex)
        {
            if (colorHex.StartsWith("#"))
                colorHex = colorHex.Replace("#", string.Empty);
            int v = int.Parse(colorHex, System.Globalization.NumberStyles.HexNumber);

            var A = Convert.ToByte((v >> 24) & 255);
            var R = Convert.ToByte((v >> 16) & 255);
            var G = Convert.ToByte((v >> 8) & 255);
            var B = Convert.ToByte((v >> 0) & 255);

            var argb = new Argb32() { A = A, R = R, G = G, B = B };
            return argb;

        }

    }
}