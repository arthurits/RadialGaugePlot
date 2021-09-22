using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace RadialGaugePlot
{
    
    public class Legend
    {
        public Alignment Location = Alignment.LowerRight;
        public bool FixedLineWidth = false;
        public bool ReverseOrder = false;
        public bool AntiAlias = true;
        public bool IsVisible { get; set; } = false;

        public Color FillColor = Color.White;
        public Color OutlineColor = Color.Black;
        public Color FontColor = Color.Black;
        public Color ShadowColor = Color.FromArgb(50, Color.Black);
        public float ShadowOffsetX = 2;
        public float ShadowOffsetY = 2;

        public Font Font = SystemFonts.DefaultFont;
        //public string FontName { set { Font.Name = value; } }
        //public float FontSize { set { Font.Size = value; } }
        //public Color FontColor { set { Font.Color = value; } }
        //public bool FontBold { set { Font.Bold = value; } }

        public float Padding = 5;
        private float SymbolWidth { get { return 40 * Font.Size / 12; } }
        private float SymbolPad { get { return Font.Size / 3; } }
        private float MarkerWidth { get { return Font.Size / 2; } }

        public LegendItem[] LegendItems { get; set; }

        public Legend()
        {
        
        }

        public Legend(LegendItem[] items)
            :base()
        {
            if (items != null && items.Length > 0)
            {
                LegendItems = items;
                IsVisible = true;
            }
        }

        public void Render(RectangleF dims, Bitmap bmp, bool lowQuality = false)
        {
            if (IsVisible is false || LegendItems is null || LegendItems.Length == 0)
                return;

            //using (var gfx = GDI.Graphics(bmp, dims, lowQuality, false))
            using var gfx = Graphics.FromImage(bmp);
            gfx.SmoothingMode = lowQuality ? System.Drawing.Drawing2D.SmoothingMode.HighSpeed : System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gfx.TextRenderingHint = lowQuality ? System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit : System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            var (maxLabelWidth, maxLabelHeight, width, height) = GetDimensions(gfx, LegendItems, Font);
            var (x, y) = GetLocationPx(dims, width, height);
            RenderOnBitmap(gfx, LegendItems, Font, x, y, width, height, maxLabelHeight);
        }

        public Bitmap GetBitmap(bool lowQuality = false, double scale = 1.0)
        {
            if (LegendItems is null)
                throw new InvalidOperationException("must render the plot at least once before getting the legend bitmap");

            if (LegendItems.Length == 0)
                throw new InvalidOperationException("The legend is empty (there are no visible plot objects with a label)");

            // use a temporary bitmap and graphics (without scaling) to measure how large the final image should be
            using var tempBitmap = new Bitmap(1, 1);
            //using var tempGfx = GDI.Graphics(tempBitmap, lowQuality, scale);
            using var tempGfx = Graphics.FromImage(tempBitmap);
            tempGfx.SmoothingMode = lowQuality ? System.Drawing.Drawing2D.SmoothingMode.HighSpeed : System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            tempGfx.TextRenderingHint = lowQuality ? System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit : System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            tempGfx.ScaleTransform((float)scale, (float)scale);
            //using var legendFont = GDI.Font(Font);
            var (maxLabelWidth, maxLabelHeight, totalLabelWidth, totalLabelHeight) = GetDimensions(tempGfx, LegendItems, Font);

            // create the actual legend bitmap based on the scaled measured size
            int width = (int)(totalLabelWidth * scale);
            int height = (int)(totalLabelHeight * scale);
            Bitmap bmp = new(width, height, PixelFormat.Format32bppPArgb);
            //using var gfx = GDI.Graphics(bmp, lowQuality, scale);
            using var gfx = Graphics.FromImage(bmp);
            gfx.SmoothingMode = lowQuality ? System.Drawing.Drawing2D.SmoothingMode.HighSpeed : System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gfx.TextRenderingHint = lowQuality ? System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit : System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            gfx.ScaleTransform((float)scale, (float)scale);
            RenderOnBitmap(gfx, LegendItems, Font, 0, 0, totalLabelWidth, totalLabelHeight, maxLabelHeight);

            return bmp;
        }

        private (float maxLabelWidth, float maxLabelHeight, float width, float height)
            GetDimensions(Graphics gfx, LegendItem[] items, System.Drawing.Font font)
        {
            // determine maximum label size and use it to define legend size
            float maxLabelWidth = 0;
            float maxLabelHeight = 0;
            for (int i = 0; i < items.Length; i++)
            {
                var labelSize = gfx.MeasureString(items[i].label, font);
                maxLabelWidth = Math.Max(maxLabelWidth, labelSize.Width);
                maxLabelHeight = Math.Max(maxLabelHeight, labelSize.Height);
            }

            float width = SymbolWidth + maxLabelWidth + SymbolPad;
            float height = maxLabelHeight * items.Length;

            return (maxLabelWidth, maxLabelHeight, width, height);
        }

        private void RenderOnBitmap(Graphics gfx, LegendItem[] items, System.Drawing.Font font,
            float locationX, float locationY, float width, float height, float maxLabelHeight,
            bool shadow = true, bool outline = true)
        {
            using var fillBrush = new SolidBrush(FillColor);
            using var shadowBrush = new SolidBrush(ShadowColor);
            using var textBrush = new SolidBrush(FontColor);
            using (var outlinePen = new Pen(OutlineColor))
            {
                RectangleF rectShadow = new RectangleF(locationX + ShadowOffsetX, locationY + ShadowOffsetY, width, height);
                RectangleF rectFill = new RectangleF(locationX, locationY, width, height);

                if (shadow)
                    gfx.FillRectangle(shadowBrush, rectShadow);

                gfx.FillRectangle(fillBrush, rectFill);

                if (outline)
                    gfx.DrawRectangle(outlinePen, Rectangle.Round(rectFill));

                for (int i = 0; i < items.Length; i++)
                {
                    var item = items[i];
                    float verticalOffset = i * maxLabelHeight;

                    // draw text
                    gfx.DrawString(item.label, font, textBrush, locationX + SymbolWidth, locationY + verticalOffset);

                    // prepare values for drawing a line
                    outlinePen.Color = item.color;
                    outlinePen.Width = 1;
                    float lineY = locationY + verticalOffset + maxLabelHeight / 2;
                    float lineX1 = locationX + SymbolPad;
                    float lineX2 = lineX1 + SymbolWidth - SymbolPad * 2;

                    // prepare values for drawing a rectangle
                    PointF rectOrigin = new PointF(lineX1, (float)(lineY - item.lineWidth / 2));
                    SizeF rectSize = new SizeF(lineX2 - lineX1, (float)item.lineWidth);
                    RectangleF rect = new RectangleF(rectOrigin, rectSize);

                    if (item.IsRectangle)
                    {
                        // draw a rectangle
                        //using (var legendItemFillBrush = GDI.Brush(item.color, item.hatchColor, item.hatchStyle))
                        using var legendItemFillBrush = new SolidBrush(item.color);
                        using (var legendItemOutlinePen = new Pen(item.borderColor, item.borderWith))
                        {
                            gfx.FillRectangle(legendItemFillBrush, rect);
                            gfx.DrawRectangle(legendItemOutlinePen, rect.X, rect.Y, rect.Width, rect.Height);
                        }
                    }
                    else
                    {
                        // draw a line
                        if (item.lineWidth > 0 && item.lineStyle != LineStyle.None)
                        {
                            //using var linePen = GDI.Pen(item.color, item.lineWidth, item.lineStyle, false);
                            using var linePen = new Pen(item.color);
                            linePen.Width = (float)item.lineWidth;
                            linePen.DashStyle = (DashStyle)(item.lineStyle - 1);
                            //, item.lineWidth, item.lineStyle, false);
                            gfx.DrawLine(linePen, lineX1, lineY, lineX2, lineY);
                        }

                        // and perhaps a marker in the middle of the line
                        float lineXcenter = (lineX1 + lineX2) / 2;
                        PointF markerPoint = new PointF(lineXcenter, lineY);
                        if ((item.markerShape != MarkerShape.none) && (item.markerSize > 0))
                            MarkerTools.DrawMarker(gfx, markerPoint, item.markerShape, MarkerWidth, item.color);
                    }
                }
            }
        }

        //public void UpdateLegendItems(IPlottable[] renderables)
        //{
        //    LegendItems = renderables.Where(x => x.GetLegendItems() != null)
        //                             .Where(x => x.IsVisible)
        //                             .SelectMany(x => x.GetLegendItems())
        //                             .Where(x => !string.IsNullOrWhiteSpace(x.label))
        //                             .ToArray();
        //    if (ReverseOrder)
        //        Array.Reverse(LegendItems);
        //}

        private (float x, float y) GetLocationPx(RectangleF dims, float width, float height)
        {
            float leftX = dims.X + Padding;
            float rightX = dims.X + dims.Width - Padding - width;
            float centerX = dims.X + dims.Width / 2 - width / 2;

            float topY = dims.Y + Padding;
            float bottomY = dims.Y + dims.Height - Padding - height;
            float centerY = dims.Y + dims.Height / 2 - height / 2;

            switch (Location)
            {
                case Alignment.UpperLeft:
                    return (leftX, topY);
                case Alignment.UpperCenter:
                    return (centerX, topY);
                case Alignment.UpperRight:
                    return (rightX, topY);
                case Alignment.MiddleRight:
                    return (rightX, centerY);
                case Alignment.LowerRight:
                    return (rightX, bottomY);
                case Alignment.LowerCenter:
                    return (centerX, bottomY);
                case Alignment.LowerLeft:
                    return (leftX, bottomY);
                case Alignment.MiddleLeft:
                    return (leftX, centerY);
                case Alignment.MiddleCenter:
                    return (centerX, centerY);
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public class LegendItem
    {
        public string label;
        public System.Drawing.Color color;
        public System.Drawing.Color hatchColor;
        public System.Drawing.Color borderColor;
        public float borderWith;

        public LineStyle lineStyle;
        public double lineWidth;
        public MarkerShape markerShape;
        public double markerSize;
        public HatchStyle hatchStyle;
        public bool IsRectangle
        {
            get { return lineWidth >= 10; }
            set { lineWidth = 10; }
        }
    }

    //public enum HatchStyle
    //{
    //    None,
    //    StripedUpwardDiagonal,
    //    StripedDownwardDiagonal,
    //    StripedWideUpwardDiagonal,
    //    StripedWideDownwardDiagonal,
    //    LargeCheckerBoard,
    //    SmallCheckerBoard,
    //    LargeGrid,
    //    SmallGrid,
    //    DottedDiamond
    //}

    public enum MarkerShape
    {
        // TODO: capitalize these in ScottPlot 4.1
        none,
        filledCircle,
        filledSquare,
        openCircle,
        openSquare,
        filledDiamond,
        openDiamond,
        asterisk,
        hashTag,
        cross,
        eks,
        verticalBar,
        triUp,
        triDown,
    }

    public enum LineStyle
    {
        None,
        Solid,
        Dash,
        Dot,
        DashDot,
        DashDotDot,
        Custom
    }

    public class MarkerTools
    {
        public static void DrawMarker(Graphics gfx, PointF pixelLocation, MarkerShape shape, float size, Color color)
        {
            if (size == 0 || shape == MarkerShape.none)
                return;

            Pen pen = new Pen(color);

            Brush brush = new SolidBrush(color);

            PointF corner1 = new PointF(pixelLocation.X - size / 2, pixelLocation.Y - size / 2);
            PointF corner2 = new PointF(pixelLocation.X + size / 2, pixelLocation.Y + size / 2);
            SizeF bounds = new SizeF(size, size);
            RectangleF rect = new RectangleF(corner1, bounds);

            switch (shape)
            {
                case MarkerShape.filledCircle:
                    gfx.FillEllipse(brush, rect);
                    break;
                case MarkerShape.openCircle:
                    gfx.DrawEllipse(pen, rect);
                    break;
                case MarkerShape.filledSquare:
                    gfx.FillRectangle(brush, rect);
                    break;
                case MarkerShape.openSquare:
                    gfx.DrawRectangle(pen, corner1.X, corner1.Y, size, size);
                    break;
                case MarkerShape.filledDiamond:

                    // Create points that define polygon.
                    PointF point1 = new PointF(pixelLocation.X, pixelLocation.Y + size / 2);
                    PointF point2 = new PointF(pixelLocation.X - size / 2, pixelLocation.Y);
                    PointF point3 = new PointF(pixelLocation.X, pixelLocation.Y - size / 2);
                    PointF point4 = new PointF(pixelLocation.X + size / 2, pixelLocation.Y);

                    PointF[] curvePoints = { point1, point2, point3, point4 };

                    //Draw polygon to screen
                    gfx.FillPolygon(brush, curvePoints);
                    break;
                case MarkerShape.openDiamond:

                    // Create points that define polygon.
                    PointF point5 = new PointF(pixelLocation.X, pixelLocation.Y + size / 2);
                    PointF point6 = new PointF(pixelLocation.X - size / 2, pixelLocation.Y);
                    PointF point7 = new PointF(pixelLocation.X, pixelLocation.Y - size / 2);
                    PointF point8 = new PointF(pixelLocation.X + size / 2, pixelLocation.Y);

                    PointF[] curvePoints2 = { point5, point6, point7, point8 };

                    //Draw polygon to screen
                    gfx.DrawPolygon(pen, curvePoints2);

                    break;
                case MarkerShape.asterisk:
                    Font drawFont = new Font("CourierNew", size * 3);
                    SizeF textSize = MeasureString(gfx, "*", drawFont);
                    PointF asteriskPoint = new PointF(pixelLocation.X - textSize.Width / 2, pixelLocation.Y - textSize.Height / 4);
                    gfx.DrawString("*", drawFont, brush, asteriskPoint);

                    break;
                case MarkerShape.hashTag:
                    Font drawFont2 = new Font("CourierNew", size * 2);
                    SizeF textSize2 = MeasureString(gfx, "#", drawFont2);
                    PointF asteriskPoint2 = new PointF(pixelLocation.X - textSize2.Width / 2, pixelLocation.Y - textSize2.Height / 3);
                    gfx.DrawString("#", drawFont2, brush, asteriskPoint2);

                    break;
                case MarkerShape.cross:
                    Font drawFont3 = new Font("CourierNew", size * 2);
                    SizeF textSize3 = MeasureString(gfx, "+", drawFont3);
                    PointF asteriskPoint3 = new PointF(pixelLocation.X - textSize3.Width / (5 / 2), pixelLocation.Y - textSize3.Height / 2);
                    gfx.DrawString("+", drawFont3, brush, asteriskPoint3);

                    break;
                case MarkerShape.eks:
                    Font drawFont4 = new Font("CourierNew", size * 2);
                    SizeF textSize4 = MeasureString(gfx, "x", drawFont4);
                    PointF asteriskPoint4 = new PointF(pixelLocation.X - textSize4.Width / (5 / 2), pixelLocation.Y - textSize4.Height / 2);
                    gfx.DrawString("x", drawFont4, brush, asteriskPoint4);

                    break;
                case MarkerShape.verticalBar:
                    Font drawFont5 = new Font("CourierNew", size * 2);
                    SizeF textSize5 = MeasureString(gfx, "|", drawFont5);
                    PointF asteriskPoint5 = new PointF(pixelLocation.X - textSize5.Width / (5 / 2), pixelLocation.Y - textSize5.Height / 2);
                    gfx.DrawString("|", drawFont5, brush, asteriskPoint5);

                    break;
                case MarkerShape.triUp:
                    // Create points that define polygon.
                    PointF point9 = new PointF(pixelLocation.X, pixelLocation.Y - size);
                    PointF point10 = new PointF(pixelLocation.X, pixelLocation.Y);
                    PointF point11 = new PointF(pixelLocation.X - size * (float)0.866, pixelLocation.Y + size * (float)0.5);
                    PointF point12 = new PointF(pixelLocation.X, pixelLocation.Y);
                    PointF point13 = new PointF(pixelLocation.X + size * (float)0.866, (pixelLocation.Y + size * (float)0.5));


                    PointF[] curvePoints3 = { point12, point9, point10, point11, point12, point13 };

                    //Draw polygon to screen
                    gfx.DrawPolygon(pen, curvePoints3);

                    break;
                case MarkerShape.triDown:
                    // Create points that define polygon.
                    PointF point14 = new PointF(pixelLocation.X, pixelLocation.Y + size);
                    PointF point15 = new PointF(pixelLocation.X, pixelLocation.Y);
                    PointF point16 = new PointF(pixelLocation.X - size * (float)0.866, pixelLocation.Y - size * (float)0.5);
                    PointF point17 = new PointF(pixelLocation.X, pixelLocation.Y);
                    PointF point18 = new PointF(pixelLocation.X + size * (float)0.866, (pixelLocation.Y - size * (float)0.5));


                    PointF[] curvePoints4 = { point17, point14, point15, point16, point17, point18 };

                    //Draw polygon to screen
                    gfx.DrawPolygon(pen, curvePoints4);

                    break;
                case MarkerShape.none:
                    break;
                default:
                    throw new NotImplementedException($"unsupported marker type: {shape}");
            }
        }

        public static SizeF MeasureString(Graphics gfx, string text, System.Drawing.Font font)
        {
            SizeF size = gfx.MeasureString(text, font);

            float xMultiplierLinux = 1;
            float yMultiplierLinux = 27.16f / 22;

            float xMultiplierMacOS = 82.82f / 72;
            float yMultiplierMacOS = 27.16f / 20;

            // compensate for OS-specific differences in font scaling
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                size.Width *= xMultiplierLinux;
                size.Height *= yMultiplierLinux;
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
            {
                size.Width *= xMultiplierMacOS;
                size.Height *= yMultiplierMacOS;
            }

            // ensure the measured height is at least the font size
            size.Height = Math.Max(font.Size, size.Height);

            return size;
        }
    }

    
}
