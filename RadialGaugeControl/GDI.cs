using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Drawing
{
    public static class GDI
    {
        private const float xMultiplierLinux = 1;
        private const float yMultiplierLinux = 27.16f / 22;

        private const float xMultiplierMacOS = 82.82f / 72;
        private const float yMultiplierMacOS = 27.16f / 20;

        /// <summary>
        /// Return the display scale ratio being used.
        /// A scaling ratio of 1.0 means scaling is not active.
        /// </summary>
        public static float GetScaleRatio()
        {
            const int DEFAULT_DPI = 96;
            using Graphics gfx = GDI.Graphics(new Bitmap(1, 1));
            return gfx.DpiX / DEFAULT_DPI;
        }

        public static SizeF MeasureString(string text, Font font)
        {
            using Bitmap bmp = new Bitmap(1, 1);
            using Graphics gfx = Graphics(bmp, lowQuality: true);
            return MeasureString(gfx, text, font.Name, font.Size, font.Bold);
        }

        public static SizeF MeasureString(Graphics gfx, string text, Font font)
        {
            return MeasureString(gfx, text, font.Name, font.Size, font.Bold);
        }

        public static SizeF MeasureString(Graphics gfx, string text, string fontName, double fontSize, bool bold = false)
        {
            var fontStyle = (bold) ? FontStyle.Bold : FontStyle.Regular;
            using var font = new System.Drawing.Font(fontName, (float)fontSize, fontStyle, GraphicsUnit.Pixel);
            return MeasureString(gfx, text, font);
        }

        public static SizeF MeasureString(Graphics gfx, string text, System.Drawing.Font font)
        {
            SizeF size = gfx.MeasureString(text, font);

            // compensate for OS-specific differences in font scaling
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                size.Width *= xMultiplierLinux;
                size.Height *= yMultiplierLinux;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                size.Width *= xMultiplierMacOS;
                size.Height *= yMultiplierMacOS;
            }

            // ensure the measured height is at least the font size
            size.Height = Math.Max(font.Size, size.Height);

            return size;
        }

        public static System.Drawing.Graphics Graphics(Bitmap bmp, bool lowQuality = false, double scale = 1.0)
        {
            Graphics gfx = System.Drawing.Graphics.FromImage(bmp);
            gfx.SmoothingMode = lowQuality ? SmoothingMode.HighSpeed : SmoothingMode.AntiAlias;
            gfx.TextRenderingHint = lowQuality ? TextRenderingHint.SingleBitPerPixelGridFit : TextRenderingHint.AntiAliasGridFit;
            gfx.ScaleTransform((float)scale, (float)scale);
            return gfx;
        }

        public static System.Drawing.Graphics Graphics(Bitmap bmp, RectangleF dims, bool lowQuality = false, double scale = 1.0, bool clipToDataArea = true)
        {
            Graphics gfx = Graphics(bmp, lowQuality, scale);

            if (clipToDataArea)
            {
                /* These dimensions are withdrawn by 1 pixel to leave room for a 1px wide data frame.
                 * Rounding is intended to exactly match rounding used when frame placement is determined.
                 */
                float left = (int)Math.Round(dims.X) + 1;
                float top = (int)Math.Round(dims.Y) + 1;
                float width = (int)Math.Round(dims.Width) - 1;
                float height = (int)Math.Round(dims.Height) - 1;
                gfx.Clip = new Region(new RectangleF(left, top, width, height));
            }

            return gfx;
        }
        public static System.Drawing.Font Font(Drawing.Font font) =>
            Font(font.Name, font.Size, font.Bold);

        public static System.Drawing.Font Font(string fontName = null, float fontSize = 12, bool bold = false)
        {
            string validFontName = InstalledFont.ValidFontName(fontName);
            FontStyle fontStyle = bold ? FontStyle.Bold : FontStyle.Regular;
            return new System.Drawing.Font(validFontName, fontSize, fontStyle, GraphicsUnit.Pixel);
        }

        public static Bitmap Resize(Image bmp, int width, int height)
        {
            var bmp2 = new Bitmap(width, height);
            var rect = new Rectangle(0, 0, width, height);

            using (var gfx = System.Drawing.Graphics.FromImage(bmp2))
            using (var attribs = new ImageAttributes())
            {
                gfx.CompositingMode = CompositingMode.SourceCopy;
                gfx.CompositingQuality = CompositingQuality.HighQuality;
                gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gfx.SmoothingMode = SmoothingMode.HighQuality;
                gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
                attribs.SetWrapMode(WrapMode.TileFlipXY);
                gfx.DrawImage(bmp, rect, 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attribs);
            }

            return bmp2;
        }
    }
}
