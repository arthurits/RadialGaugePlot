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

        public bool Visible { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public PointF Center { get; set; }
        public RectangleF Rectangle
        {
            set
            {
                X = value.X;
                Y = value.Y;
                Width = value.Width;
                Height = value.Height;
            }
        }

        /// <summary>
        /// Renders this plot element into the bitmap passed
        /// </summary>
        public Action<Bitmap, bool> Render { get; set; }

        //public delegate void RenderDelegate(Bitmap bmp, bool lowQuality = false);
        //public RenderDelegate RenderTest { get; set; }

        public override string ToString() => $"Plot element with text: {Text}, center at ({Center})";

        
        
        public PlotElement()
        {
            Margin = Padding.Empty;
            Padding = Padding.Empty;
            Visible = true;
        }

        public PlotElement(RectangleF rect)
            :base()
        {
            Rectangle = rect;
        }

        /// <summary>
        /// Retrieves the rectangle defining the plot element
        /// </summary>
        /// <returns></returns>
        public RectangleF GetRectangle()
        {
            return new RectangleF(X, Y, Width, Height);
        }

        /// <summary>
        /// Retrieves the rectangle that includes the margin values
        /// </summary>
        /// <returns></returns>
        public RectangleF GetRectangleEx()
        {
            return new RectangleF(X - Margin.Left,
                Y - Margin.Top,
                Width + Margin.Left + Margin.Right,
                Height + Margin.Top + Margin.Bottom);
        }

        /// <summary>
        /// Retrieves the rectangle without the padding values
        /// </summary>
        /// <returns></returns>
        public RectangleF GetRectangleIn()
        {
            return new RectangleF(X + Padding.Left,
                Y + Padding.Top,
                Width - Padding.Left - Padding.Right,
                Height - Padding.Top - Padding.Bottom);
        }
        //public void Render(Bitmap bmp, bool lowQuality = false)
        //{

        //}

    }
}
