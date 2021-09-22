using System;
using System.Drawing;
using System.Windows.Forms;

namespace RadialGaugePlot
{
    public partial class Plot : UserControl
    {
        public RectangleF RectTitle { get; set; }
        public RectangleF RectData;
        public PointF Center { get; set; }

        public string Title => "Radial gauge plot";

        public string[] LegendLabels { get; set; }

        public Legend Legend { get; set; }

        public Image GetImage => this.pictureBox1.Image;

        public Plot()
        {
            InitializeComponent();
            Legend = new();
        }


        private void OnLoad(object sender, EventArgs e)
        {

        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            // Compute dimensions before any drawing takes place
            Center = new(Width / 2, Height / 2);
            ComputeRects();

            // Create the bitmap and make the corresponding drawing into it
            Render();
        }

        /// <summary>
        /// Set the new bitmap into the control and dispose any previous one
        /// </summary>
        /// <param name="newBmp"></param>
        private void SetImage(Bitmap newBmp)
        {
            var oldBmp = pictureBox1.Image;
            pictureBox1.Image = newBmp;
            if (oldBmp != null && oldBmp != newBmp)
                oldBmp.Dispose();
        }


        public void Render(bool lowQuality = false)
        {
            Bitmap bmp = new((int)Width, (int)Height);
            Render(bmp, lowQuality);

            // Draw the legend onto the bitmap
            var legendItems = GetLegendItems();
            if (legendItems != null && legendItems.Length > 0)
            {
                Legend.LegendItems = legendItems;
                Legend.IsVisible = true;
                Legend.Render(new Rectangle(0, 0, Width, Height), bmp, lowQuality);
            }

            // Set the image into the picture box control
            SetImage(bmp);
        }

        public virtual void Render(Bitmap bmp, bool lowQuality = false)
        {
            using Graphics newGraphics = Graphics.FromImage(bmp);
            newGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            newGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            newGraphics.DrawRectangle(new Pen(Color.Black), RectData.X, RectData.Y, RectData.Width, RectData.Height);
            newGraphics.DrawRectangle(new Pen(Color.Black), RectTitle.X, RectTitle.Y, RectTitle.Width, RectTitle.Height);
            newGraphics.DrawString(Title, Font, new SolidBrush(Color.Black), RectTitle);
        }

        public virtual LegendItem[] GetLegendItems()
        {
            return null;
        }


        /// <summary>
        /// Compute geometric data
        /// </summary>
        protected virtual void ComputeRects()
        {
            // Compute the Title rect
            using Bitmap bmp = new (1, 1);
            using Graphics gfx = System.Drawing.Graphics.FromImage(bmp);
            SizeF sizeText = gfx.MeasureString(Title, Font);
            // compensate for OS-specific differences in font scaling
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                sizeText.Width *= 1;
                sizeText.Height *= 27.16f / 22;
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
            {
                sizeText.Width *= 82.82f / 72;
                sizeText.Height *= 27.16f / 20;
            }

            // ensure the measured height is at least the font size
            sizeText.Height = Math.Max(Font.Size, sizeText.Height);

            RectTitle = new RectangleF(new PointF((Width - sizeText.Width) / 2, sizeText.Height * 0.5f), sizeText);

            // Compute the minimum dimension of the control and substract 2 times the space for the title
            float min = Math.Min(Width, Height);
            min /= 2;
            min -= Title.Length > 0 ? 2 * RectTitle.Height : 0;
            
            RectData = new RectangleF(Center.X, Center.Y, 0, 0);
            RectData.Inflate(min, min);
        }
    }
}
