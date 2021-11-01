using System.Windows.Media;
using static MathLibrary.DrawingPipeline;

namespace MathLibrary
{
    public static class PipelineExtensions
    {
        public static Brush ToBrush(this Pixel p)
        {
            return new SolidColorBrush(Color.FromArgb((byte)p.a, (byte)p.r, (byte)p.g, (byte)p.b));
        }

        public static Pixel ToPixel(this SolidColorBrush b)
        {
            return new Pixel(b.Color.R, b.Color.G, b.Color.B, b.Color.A);
        }
    }
}
