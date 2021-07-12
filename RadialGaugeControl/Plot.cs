﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace RadialGaugeControl
{
    public partial class Plot : UserControl
    {
        public RectangleF RectTitle { get; set; }
        public RectangleF RectData;
        public PointF Center { get; set; }

        public string Title => "Radial gauge plot";

        public Plot()
        {
            InitializeComponent();
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
            Bitmap bmp = new((int)Width, (int)Height);
            Render(bmp);

            // Set the new bitmap into the control and dispone any previous one
            var oldBitmap = pictureBox1.Image;
            pictureBox1.Image = bmp;
            if (oldBitmap != null)
                oldBitmap.Dispose();
        }

        protected virtual void Render(Bitmap bmp, bool lowQuality = false)
        {
            using Graphics newGraphics = Graphics.FromImage(bmp);
            newGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            newGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            newGraphics.DrawRectangle(new Pen(Color.Black), RectData.X, RectData.Y, RectData.Width, RectData.Height);
            newGraphics.DrawRectangle(new Pen(Color.Black), RectTitle.X, RectTitle.Y, RectTitle.Width, RectTitle.Height);
            newGraphics.DrawString(Title, Font, new SolidBrush(Color.Black), RectTitle);
        }

        /// <summary>
        /// Compute geometric data
        /// </summary>
        protected virtual void ComputeRects()
        {
            // Compute the Title rect
            using Bitmap bmp = new (1, 1);
            using Graphics gfx = System.Drawing.Graphics.FromImage(bmp);
            SizeF size = gfx.MeasureString(Title, Font);
            // compensate for OS-specific differences in font scaling
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                size.Width *= 1;
                size.Height *= 27.16f / 22;
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
            {
                size.Width *= 82.82f / 72;
                size.Height *= 27.16f / 20;
            }

            // ensure the measured height is at least the font size
            size.Height = Math.Max(Font.Size, size.Height);

            RectTitle = new RectangleF(new PointF((Width - size.Width) / 2, size.Height * 0.5f), size);

            // Compute the minimum dimension of the control and substract 2 times the space for the title
            float min = Math.Min(Width, Height);
            min /= 2;
            min -= Title.Length > 0 ? 2 * RectTitle.Height : 0;
            
            RectData = new RectangleF(Center.X, Center.Y, 0, 0);
            RectData.Inflate(min, min);
        }
    }
}
