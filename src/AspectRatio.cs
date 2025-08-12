using System;
using System.Drawing;

namespace WinLatch
{
    public class AspectRatio
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }

        public AspectRatio(int width, int height, string name)
        {
            Width = width;
            Height = height;
            Name = name;
        }

        public double Ratio => (double)Width / Height;

        public override string ToString()
        {
            return Name;
        }

        public static AspectRatio[] CommonAspectRatios = new[]
        {
            new AspectRatio(16, 9, "16:9"),
            new AspectRatio(16, 10, "16:10"),
            new AspectRatio(4, 3, "4:3"),
            new AspectRatio(3, 2, "3:2"),
            new AspectRatio(21, 9, "21:9"),
            new AspectRatio(32, 9, "32:9"),
            new AspectRatio(5, 4, "5:4")
        };

        public static AspectRatio GetClosestAspectRatio(Size size)
        {
            double targetRatio = (double)size.Width / size.Height;
            AspectRatio closest = CommonAspectRatios[0];
            double closestDiff = Math.Abs(targetRatio - closest.Ratio);

            foreach (var ratio in CommonAspectRatios)
            {
                double diff = Math.Abs(targetRatio - ratio.Ratio);
                if (diff < closestDiff)
                {
                    closestDiff = diff;
                    closest = ratio;
                }
            }

            return closest;
        }

        public Size CalculateSize(Size container, bool fitToWidth = true)
        {
            if (fitToWidth)
            {
                int width = container.Width;
                int height = (int)(width / Ratio);
                return new Size(width, height);
            }
            else
            {
                int height = container.Height;
                int width = (int)(height * Ratio);
                return new Size(width, height);
            }
        }
    }
}
