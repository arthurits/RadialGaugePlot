using System;
using System.Drawing;
using System.Windows.Forms;

namespace RadialGaugePlot
{
    public class PlotElement
    {
        public Drawing.Font Font { get; set; }
        public Padding Margin { get; set; }
        public Padding Padding { get; set; }
        public string Text { get; set; }

        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public PointF Center { get; set; }

        public override string ToString() => $"Plot element with text: {Text}, center at ";

        public PlotElement()
        {
            
        }

        public void Render(Bitmap bmp, bool lowQuality = false)
        {

        }

    }
}
