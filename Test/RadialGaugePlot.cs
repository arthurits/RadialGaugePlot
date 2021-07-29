using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ScottPlot.Drawing;

// Inspired (and expanding) by https://github.com/dotnet-ad/Microcharts/blob/main/Sources/Microcharts/Charts/RadialGaugeChart.cs

// Lighten or darken color
// https://stackoverflow.com/questions/801406/c-create-a-lighter-darker-color-based-on-a-system-color
// https://www.pvladov.com/2012/09/make-color-lighter-or-darker.html
// https://gist.github.com/zihotki/09fc41d52981fb6f93a81ebf20b35cd5

// Circular Segment
// https://github.com/falahati/CircularProgressBar/blob/master/CircularProgressBar/CircularProgressBar.cs
// https://github.com/aalitor/AltoControls/blob/on-development/AltoControls/Controls/Circular%20Progress%20Bar.cs

// http://csharphelper.com/blog/2015/02/draw-lines-with-custom-end-caps-in-c/

// Text on path
// http://csharphelper.com/blog/2018/02/draw-text-on-a-circle-in-c/
// http://csharphelper.com/blog/2016/01/draw-text-on-a-curve-in-c/

// https://github.com/ScottPlot/ScottPlot/tree/master/src/ScottPlot/Plottable
// under RadialGaugePlot.cs
namespace ScottPlot.Plottable
{
    /// <summary>
    /// A radial gauge chart is a graphical method of displaying multivariate data in the form of 
    /// a two-dimensional chart of three or more quantitative variables represented on axes 
    /// starting from the same point.
    /// 
    /// Data is managed using 2D arrays where groups (colored shapes) are rows and categories (arms of the web) are columns.
    /// </summary>
    public class RadialGaugePlot : IPlottable
    {
        /// <summary>
        /// Data to be plotted.
        /// It's copied from of the data passed to either the constructor or the <see cref="Update(double[], bool)"/> method.
        /// </summary>
        protected double[] DataRaw;

        /// <summary>
        /// Angular data (first column: initial angle; second column: swept angle) computed from <see cref="DataRaw"/>
        /// </summary>
        protected double[,] DataAngular;

        /// <summary>
        /// Tha maximum value for scaling the gauges.
        /// This value is associated to <see cref="StartingAngleGauges"/> + <see cref="AngleRange"/>.
        /// </summary>
        protected double MaxScale
        {
            get => _MaxScale;
            set
            {
                _MaxScale = value;
                ComputeAngularData();
            }
        }
        private double _MaxScale;

        /// <summary>
        /// Tha minimum value for scaling the gauges.
        /// This value is associated to <see cref="StartingAngleGauges"/> + <see cref="AngleRange"/>.
        /// </summary>
        protected double MinScale
        {
            get => _MinScale;
            set
            {
                _MinScale = value;
                ComputeAngularData();
            }
        }
        private double _MinScale = 0;

        /// <summary>
        /// The maximum angular interval that the gauges will consist of.
        /// It takes values in the range [0-360], default value is 360. Outside this range, unexpected side-effects might happen.
        /// </summary>
        public double AngleRange
        {
            set
            {
                // Modify value to be in the range [0, 360]
                _AngleRange = value > 360 ? 360 : (value < 0 ? 0 : value);
                ComputeAngularData();
            }
            get => _AngleRange;
            
        }
        private double _AngleRange = 360;

        /// <summary>
        /// Labels for each gauge.
        /// Length must be equal to the length of data passed to either the constructor or the <see cref="Update(double[], bool)"/> method.
        /// </summary>
        public string[] GaugeLabels;

        /// <summary>
        /// Colors for each gauge. These colors are dimmed according to <see cref="DimPercentage"/> to draw the gauges' background.
        /// Length must be equal to the length of data passed to either the constructor or the <see cref="Update(double[], bool)"/> method.
        /// </summary>
        public Color[] GaugeColors;

        /// <summary>
        /// Color of the axis lines and concentric circles representing ticks
        /// </summary>
        public Color WebColor { get; set; } = Color.Gray;

        /// <summary>
        /// Gets or sets the size (in pixels) of each gauge.
        /// If <0, then it will be calculated from the available space.
        /// </summary>
        public float LineWidth { get; set; } = -1;

        /// <summary>
        /// Dimmed percentage used to draw the gauges' background.
        /// Values in the range [0-100], default value is 90 [percent]. Outside this range, unexpected side-effects might happen.
        /// </summary>
        public float DimPercentage
        {
            get => _DimPercentage;
            set => _DimPercentage = (value > 100 ? 100 : (value < 0 ? 0 : value));
        }
        private float _DimPercentage = 90f;

        /// <summary>
        /// Determines whether the gauges are drawn clockwise (default value) or anti-clockwise (counter clockwise).
        /// </summary>
        public RadialGaugeDirection GaugeDirection
        {
            get => _GaugeDirection;
            set
            {
                _GaugeDirection = value;
                ComputeAngularData();
            }
        }
        private RadialGaugeDirection _GaugeDirection = RadialGaugeDirection.Clockwise;
        
        /// <summary>
        /// Determins whether the gauges are drawn stacked (dafault value), sequentially, or as a single gauge (ressembling a pie plot).
        /// </summary>
        public RadialGaugeMode GaugeMode
        {
            get => _GaugeMode;
            set
            {
                _GaugeMode = value;
                ComputeMaxMin();
                ComputeAngularData();
            }
        }
        private RadialGaugeMode _GaugeMode = RadialGaugeMode.Stacked;

        // Determines the gauge label position: beginning, middle, ending (default value)
        public float GaugeLabelPos
        {
            get => _GaugeLabelPos;
            set => _GaugeLabelPos = value;
        }
        private float _GaugeLabelPos = 100;

        /// <summary>
        /// Determines whether the gauges are drawn starting from the inside (default value) or from the outside.
        /// </summary>
        public RadialGaugeStart GaugeStart { get; set; } = RadialGaugeStart.InsideToOutside;

        /// <summary>
        /// <see langword="True"/> if the gauges' background is adjusted to <see cref="StartingAngleGauges"/>.
        /// Default value is set to <see langword="False"/>.
        /// </summary>
        public bool NormBackGauge { get; set; } = false;

        /// <summary>
        /// Angle (in degrees) at which the gauges start: 270� for North (default value), 0� for East, 90� for South, 180� for West, and so on.
        /// Expected values in the range [0�-360�], otherwise unexpected side-effects might happen.
        /// </summary>
        public float StartingAngleGauges
        {
            get => _StartingAngleGauges;
            set
            {
                _StartingAngleGauges = (float)ReduceAngle(value);
                ComputeAngularData();
            }
        }
        private float _StartingAngleGauges = 270f;

        /// <summary>
        /// The initial angle (in degrees) where the background gauges begin. Default value is 270� the same as <see cref="StartingAngleGauges"/>.
        /// </summary>
        public float StartingAngleBackGauges
        {
            get => _StartingAngleBackGauges;
            set
            {
                _StartingAngleBackGauges = (float)ReduceAngle(value);
                ComputeAngularData();
            }
        }
        private float _StartingAngleBackGauges = 270f;

        /// <summary>
        /// The empty space between gauges as a percentage of the gauge width.
        /// Values in the range [0-100], default value is 50 [percent]. Other values might produce unexpected side-effects.
        /// </summary>
        public float GaugeSpacePercentage
        {
            get => _GaugeSpacePercentage;
            set => _GaugeSpacePercentage = (value > 100 ? 100 : (value < 0 ? 0 : value));
        }
        private float _GaugeSpacePercentage = 50f;

        /// <summary>
        /// <see langword="Color"/> of the value labels drawn inside the gauges.
        /// </summary>
        public Color GaugeLabelsColor { get; set; } = Color.White;

        /// <summary>
        /// Size of the gague label text as a percentage of the gauge width.
        /// Values in the range [0-100], default value is 75 [percent]. Other values might produce unexpected side-effects.
        /// </summary>
        public float GaugeLabelsFontPct
        {
            get => _GaugeLabelsFontPct;
            set => _GaugeLabelsFontPct = (value > 100 ? 100 : (value < 0 ? 0 : value));
        }
        private float _GaugeLabelsFontPct = 75f;

        /// <summary>
        /// <see langword="Font"/> used for labeling values on the plot
        /// </summary>
        public Drawing.Font Font { get; set; } = new();

        /// <summary>
        /// <see langword="True"/> if value labels are shown inside the gauges.
        /// Size of the text is set by <see cref="GaugeLabelsFontPct"/> and color by <see cref="GaugeLabelsColor"/>.
        /// </summary>
        public bool ShowGaugeValues { get; set; } = true;

        public System.Drawing.Drawing2D.LineCap EndCap { get; set; } = System.Drawing.Drawing2D.LineCap.Triangle;

        public System.Drawing.Drawing2D.LineCap StartCap { get; set; } = System.Drawing.Drawing2D.LineCap.Round;

        /// <summary>
        /// Controls rendering style of the concentric circles (ticks) of the web
        /// </summary>
        //public RadarAxis AxisType { get; set; } = RadarAxis.None;

        // These 3 properties are needed as part of IPlottable
        public bool IsVisible { get; set; } = true;
        public int XAxisIndex { get; set; } = 0;
        public int YAxisIndex { get; set; } = 0;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="values">Array of (positive) values to be plotted as gauges.</param>
        /// <param name="lineColors">Array colors for the gauges.</param>
        public RadialGaugePlot(double[] values, Color[] lineColors)
        {
            GaugeColors = lineColors;
            Update(values);
        }

        public override string ToString() =>
            $"RadialGauge with {DataRaw.Length} points.";

        /// <summary>
        /// Replace the data values with new ones. This data is copied and stored in <see cref="DataRaw"/>.
        /// </summary>
        /// <param name="values">Array of (positive) values to be plotted as gauges.</param>
        public void Update(double[] values)
        {
            DataRaw = new double[values.Length];
            Array.Copy(values, 0, DataRaw, 0, values.Length);

            // Sets MaxScale value and triggers ComputeAngularData
            ComputeMaxMin();
            //ComputeAngularData();
        }

        public double[] GetData() => DataRaw;
        public double[,] GetAngularData() => DataAngular;

        /// <summary>
        /// Converts <see cref="DataRaw"/> into <see cref="DataAngular"/>.
        /// Depends on <see cref="DataRaw"/>, <see cref="GaugeMode"/>, <see cref="GaugeDirection"/>, <see cref="StartingAngleGauges"/>, <see cref="AngleRange"/>, and <see cref="MaxScale"/>
        /// </summary>
        private void ComputeAngularData()
        {
            // Check if there's data
            if (DataRaw == null) return;
            DataAngular = new double[DataRaw.Length, 2];
            
            // Internal variables
            float AngleSumPos = _StartingAngleGauges;
            float AngleSumNeg = _StartingAngleGauges;
            float AngleSwept;
            float AngleInit;
            //System.Diagnostics.Debug.Print("ComputeAngularData init");
            // Loop through DataRaw and compute DataAngular
            for (int i=0; i<DataRaw.Length; i++)
            {
                AngleInit = (DataRaw[i] >= 0 ? AngleSumPos : AngleSumNeg);
                AngleSwept = (_GaugeDirection == RadialGaugeDirection.AntiClockwise ? -1 : 1) * (float)(_AngleRange * DataRaw[i] / (_MaxScale - _MinScale));


                DataAngular[i, 0] = (_GaugeMode == RadialGaugeMode.Stacked ? _StartingAngleGauges : AngleInit);
                DataAngular[i, 1] =  AngleSwept;

                if (DataRaw[i] >= 0)
                    AngleSumPos += AngleSwept;
                else
                    AngleSumNeg += AngleSwept;
                //System.Diagnostics.Debug.Print("AngleInit: {1}\tAngleSwept: {2}\tDataAngular[{0}, 0]: {3}\tDataAngular[{0}, 0]: {4}\tAngleSumPos: {5}\tAngleSumNeg: {6}", i, AngleInit, AngleSwept, DataAngular[i, 0], DataAngular[i, 1], AngleSumPos, AngleSumNeg);
            }

            // Compute the initial angle for the background gauges
            _StartingAngleBackGauges = _StartingAngleGauges;
            _StartingAngleBackGauges += (_GaugeDirection == RadialGaugeDirection.AntiClockwise ? -1 : 1) * (float)(_AngleRange * _MinScale / (_MaxScale - _MinScale));
        }

        /// <summary>
        /// Sets the value of <see cref="MaxScale"/> and <see cref="MinScale"/> properties, which in turn triggers the <see cref="ComputeAngularData"/> routine
        /// </summary>
        private void ComputeMaxMin()
        {
            if (GaugeMode == RadialGaugeMode.Sequential || GaugeMode == RadialGaugeMode.SingleGauge)
            {
                _MaxScale = DataRaw.Sum(x => Math.Abs(x));
                _MinScale = 0;
            }
            else
            {
                _MaxScale = DataRaw.Max(x => Math.Abs(x));
                var min = DataRaw.Min();
                _MinScale = min < 0 ? min : 0;
            }
        }

        /// <summary>
        /// Needed as part of IPlottable in ScottPlot.ScottForm
        /// </summary>
        /// <param name="deep"></param>
        public void ValidateData(bool deep = false)
        {
            if (GaugeLabels != null && GaugeLabels.Length != DataRaw.Length)
                throw new InvalidOperationException("Gauge labels must match size of data values");
        }

        /// <summary>
        /// Needed as part of IPlottable in ScottPlot.ScottForm
        /// </summary>
        /// <returns></returns>
        public LegendItem[] GetLegendItems()
        {
            if (GaugeLabels is null)
                return null;

            List<LegendItem> legendItems = new List<LegendItem>();
            for (int i = 0; i < GaugeLabels.Length; i++)
            {
                var item = new LegendItem()
                {
                    label = GaugeLabels[i],
                    color = GaugeColors[i],
                    lineWidth = 10,
                    markerShape = MarkerShape.none
                };
                legendItems.Add(item);
            }

            return legendItems.ToArray();
        }

        /// <summary>
        /// Needed as part of IPlottable in ScottPlot.ScottForm
        /// </summary>
        /// <returns></returns>
        public AxisLimits GetAxisLimits() => 
            (GaugeLabels != null) ? new AxisLimits(-3.5, 3.5, -3.5, 3.5) : new AxisLimits(-2.5, 2.5, -2.5, 2.5);

        /// <summary>
        /// Reduces an angle into the range [0�-360�]
        /// </summary>
        /// <param name="angle">Angle value</param>
        /// <returns>Return the angle whithin [0�-360�]</returns>
        private double ReduceAngle(double angle)
        {
            double reduced = angle;
            
            if (angle > 360.0)
                reduced -= 360 * (int)(angle / 360);
            else if (angle < -360.0)
                reduced += 360 * (int)(angle / 360);

            return reduced;
        }

        /// <summary>
        /// This is where the drawing of the plot is done
        /// </summary>
        /// <param name="dims"></param>
        /// <param name="bmp"></param>
        /// <param name="lowQuality"></param>
        public virtual void Render(PlotDimensions dims, Bitmap bmp, bool lowQuality = false)
        {
            int numGroups = DataRaw.Length;
            double minScale = new double[] { dims.GetPixelX(1), dims.GetPixelY(1) }.Min();
            PointF origin = new PointF(dims.GetPixelX(0), dims.GetPixelY(0));

            using Graphics gfx = GDI.Graphics(bmp, dims, lowQuality);
            using Pen pen = GDI.Pen(WebColor);
            using Pen penCircle = GDI.Pen(WebColor);
            using Brush labelBrush = GDI.Brush(GaugeLabelsColor);
            
            float lineWidth = (LineWidth < 0) ? (float)(minScale / ((numGroups) * (GaugeSpacePercentage + 100) / 100)) : LineWidth;
            float radiusSpace = lineWidth * (GaugeSpacePercentage + 100) / 100;
            float gaugeRadius = numGroups * radiusSpace;  // By default, the outer-most radius is computed
            float maxBackAngle = (GaugeDirection == RadialGaugeDirection.AntiClockwise ? -1 : 1) * (NormBackGauge ? (float)AngleRange : 360) ;

            pen.Width = (float)lineWidth;
            pen.StartCap = StartCap;
            pen.EndCap = EndCap;
            penCircle.Width = (float)lineWidth;
            penCircle.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            penCircle.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            using System.Drawing.Font fontGauge = new(Font.Name, lineWidth * GaugeLabelsFontPct / 100, FontStyle.Bold);

            lock (this)
            {
                int index;
                for (int i = 0; i < numGroups; i++)
                {
                    // Data is reversed in case SingleGauge is selected
                    // If OutsideToInside is selected, radius is reversed
                    if (GaugeMode != RadialGaugeMode.SingleGauge)
                    {
                        index = i;
                        gaugeRadius = (GaugeStart == RadialGaugeStart.InsideToOutside ? i + 1 : (numGroups - i)) * radiusSpace;
                    }
                    else
                    {
                        index = numGroups - i - 1;
                    }

                    // Set color values
                    pen.Color = GaugeColors[index];
                    penCircle.Color = LightenBy(GaugeColors[index], DimPercentage);

                    // Draw gauge background
                    if (GaugeMode != RadialGaugeMode.SingleGauge)
                        gfx.DrawArc(penCircle, (origin.X - gaugeRadius), (origin.Y - gaugeRadius), (gaugeRadius * 2), (gaugeRadius * 2), _StartingAngleBackGauges, maxBackAngle);

                    // Draw gauge
                    gfx.DrawArc(pen, (origin.X - gaugeRadius), (origin.Y - gaugeRadius), (gaugeRadius * 2), (gaugeRadius * 2), (float)DataAngular[index, 0], (float)DataAngular[index, 1]);

                    // Draw gauge labels
                    if (ShowGaugeValues)
                    {
                        //DrawTextOnCircle(gfx,
                        //    fontGauge,
                        //    labelBrush,
                        //    new RectangleF(dims.DataOffsetX, dims.DataOffsetY, dims.DataWidth, dims.DataHeight),
                        //    gaugeRadius,
                        //    (float)DataAngular[index, 0] + (float)((int)_GaugeLabelPos * DataAngular[index, 1] / 2),
                        //    origin.X,
                        //    origin.Y,
                        //    DataRaw[index].ToString("0.##"),
                        //    DataRaw[index] >= 0 ? GaugeDirection : (RadialGaugeDirection.Clockwise | RadialGaugeDirection.AntiClockwise) & ~GaugeDirection);

                        DrawTextOnCircle2(gfx,
                            fontGauge,
                            labelBrush,
                            new RectangleF(dims.DataOffsetX, dims.DataOffsetY, dims.DataWidth, dims.DataHeight),
                            gaugeRadius,
                            (float)DataAngular[index, 0],
                            (float)DataAngular[index, 1],
                            origin.X,
                            origin.Y,
                            DataRaw[index].ToString("0.##"),
                            _GaugeLabelPos);
                    }
    
                }

            }

        }

        #region Color routines

        /// <summary>Creates color with corrected brightness.</summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
        /// Negative values produce darker colors.</param>
        /// <returns>Corrected <see cref="Color"/> structure.</returns>
        /// <seealso cref="https://gist.github.com/zihotki/09fc41d52981fb6f93a81ebf20b35cd5"/>
        private Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        private Color LightenBy(Color color, float percent)
        {
            return ChangeColorBrightness(color, percent / 100f);
        }

        private Color DarkenBy(Color color, float percent)
        {
            return ChangeColorBrightness(color, -1f * percent / 100f);
        }

        #endregion Color routines

        #region DrawText routines
        /// <summary>
        /// Draw text centered on the top and bottom of the circle.
        /// </summary>
        /// <param name="gfx"><see langword="keyword">Graphic</see> object used to draw</param>
        /// <param name="font"><see langword="keyword">Font</see> used to draw the text</param>
        /// <param name="brush"><see langword="keyword">Brush</see> used to draw the text</param>
        /// <param name="clientRectangle"><see langword="keyword">Rectangle</see> of the ScottPlot control</param>
        /// <param name="anglePos">Angle (in degrees) where the text will be drawn</param>
        /// <param name="radius">Radius of the circle in pixels</param>
        /// <param name="cx">The x-coordinate of the circle centre</param>
        /// <param name="cy">The y-coordinate of the circle centre</param>
        /// <param name="text">String to be drawn</param>
        /// <seealso cref="http://csharphelper.com/blog/2018/02/draw-text-on-a-circle-in-c/"/>
        protected virtual void DrawTextOnCircle(Graphics gfx, System.Drawing.Font font,
            Brush brush, RectangleF clientRectangle, float radius, float anglePos, float cx, float cy,
            string text, RadialGaugeDirection direction)
        {
            // Modify anglePos to be in the range [0, 360]
            if (anglePos >= 0)
                anglePos -= 360f * (int)(anglePos / 360);
            else
                anglePos += 360f;


            // Use a StringFormat to draw the middle top of each character at (0, 0).
            using StringFormat string_format = new StringFormat();
            string_format.Alignment = StringAlignment.Center;
            string_format.LineAlignment = StringAlignment.Center;

            // Used to scale from radians to degrees.
            double RadToDeg = 180.0 / Math.PI;

            // Measure the characters.
            List<RectangleF> rects = MeasureCharacters(gfx, font, clientRectangle, text);

            // Use LINQ to add up the character widths.
            var width_query = from RectangleF rect in rects select rect.Width;
            float text_width = width_query.Sum();

            // Find the starting angle.
            double width_to_angle = 1 / radius;
            double theta = anglePos * Math.PI / 180;
            theta += (direction == RadialGaugeDirection.AntiClockwise ? -1 : 1) * (2-(int)_GaugeLabelPos) * text_width * width_to_angle/ 2;
            double initPos = theta;

            int charPos;

            // Draw the characters.
            for (int i = 0; i < text.Length; i++)
            {
                // Get the char index position
                if (initPos < Math.PI && initPos > 0)
                    charPos = i;
                else
                    charPos = text.Length - 1 - i;

                if (direction == RadialGaugeDirection.AntiClockwise)
                    charPos = text.Length - 1 - charPos;


                // Increment theta half the angular width of the current character
                theta -= (direction == RadialGaugeDirection.AntiClockwise ? -1 : 1) * rects[charPos].Width / 2 * width_to_angle;

                // Calculate the position of the upper-left corner
                double x = cx + radius * Math.Cos(theta);
                double y = cy + radius * Math.Sin(theta);

                // Transform to position the character.
                if (initPos < Math.PI && initPos > 0)
                    gfx.RotateTransform((float)(RadToDeg * (theta - Math.PI / 2)));
                else
                    gfx.RotateTransform((float)(RadToDeg * (theta + Math.PI / 2)));

                gfx.TranslateTransform((float)x, (float)y, System.Drawing.Drawing2D.MatrixOrder.Append);
                gfx.DrawString(text[charPos].ToString(), font, brush, 0, 0, string_format);
                gfx.ResetTransform();

                // Increment theta the remaining half character.
                theta -= (direction == RadialGaugeDirection.AntiClockwise ? -1 : 1) * rects[charPos].Width / 2 * width_to_angle;

            }

                
            
        }

        /// <summary>
        /// Simplify method
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="font"></param>
        /// <param name="brush"></param>
        /// <param name="clientRectangle"></param>
        /// <param name="radius"></param>
        /// <param name="angleInit"></param>
        /// <param name="angleSwept"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="text"></param>
        /// <param name="posPct"></param>
        /// <param name="direction"></param>
        protected virtual void DrawTextOnCircle2(Graphics gfx, System.Drawing.Font font,
            Brush brush, RectangleF clientRectangle, float radius, float angleInit, float angleSwept, float cx, float cy,
            string text, float posPct)
        {
            // Modify anglePos to be in the range [0, 360]
            if (angleInit >= 0)
                angleInit -= 360f * (int)(angleInit / 360);
            else
                angleInit += 360f;

            // Use a StringFormat to draw the middle top of each character at (0, 0).
            using StringFormat string_format = new StringFormat();
            string_format.Alignment = StringAlignment.Center;
            string_format.LineAlignment = StringAlignment.Center;

            // Used to scale from radians to degrees.
            double RadToDeg = 180.0 / Math.PI;
            double width_to_angle = 1 / radius;

            // Measure the characters. Use LINQ to add up the character widths.
            List<RectangleF> rects = MeasureCharacters(gfx, font, clientRectangle, text);
            var width_query = from RectangleF rect in rects select rect.Width;
            double text_width = width_query.Sum() / radius;

            // Angular data
            bool isPositive = angleSwept >= 0;
            double angle = ReduceAngle(angleInit + angleSwept * (posPct / 100));
            double theta = angle * Math.PI / 180;

            // Set the positition to the middle of the text, and determine in which hemisphere we are at
            angle += (1 - 2 * (posPct / 100)) * (isPositive ? 1 : -1) * text_width * RadToDeg / 2;
            bool isBelow = angle < 180 && angle > 0;
            
            //System.Diagnostics.Debug.Print("Angle initial: {0}\tAngle swept: {1}\tAngle: {2:0.00}\tIsBelow: {3}\tIsPositive: {4}", angleInit, angleSwept, angle, isBelow, isPositive);

            int charPos;
            // Draw the characters.
            for (int i = 0; i < text.Length; i++)
            {
                // Get the char index position
                charPos = isBelow ? i : text.Length - 1 - i;
                if (!isPositive) charPos = text.Length - 1 - charPos;

                // Increment theta half the angular width of the current character
                //theta -= (direction == RadialGaugeDirection.AntiClockwise ? -1 : 1) * rects[charPos].Width / 2 * width_to_angle;
                theta -= (isPositive ? 1 : -1) * rects[charPos].Width / 2 * width_to_angle;

                // Calculate the position of the upper-left corner
                double x = cx + radius * Math.Cos(theta);
                double y = cy + radius * Math.Sin(theta);

                // Transform to position the character.
                if (isBelow)
                    gfx.RotateTransform((float)(RadToDeg * (theta - Math.PI / 2)));
                else
                    gfx.RotateTransform((float)(RadToDeg * (theta + Math.PI / 2)));

                gfx.TranslateTransform((float)x, (float)y, System.Drawing.Drawing2D.MatrixOrder.Append);
                gfx.DrawString(text[charPos].ToString(), font, brush, 0, 0, string_format);
                gfx.ResetTransform();

                // Increment theta the remaining half character.
                //theta -= (direction == RadialGaugeDirection.AntiClockwise ? -1 : 1) * rects[charPos].Width / 2 * width_to_angle;
                theta -= (isPositive ? 1 : -1) * rects[charPos].Width / 2 * width_to_angle;
            }
        }


        /// <summary>
        /// Measure the characters in a string with no more than 32 characters.
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="font"></param>
        /// <param name="clientRectangle"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private List<RectangleF> MeasureCharactersInWord(Graphics gfx, System.Drawing.Font font, RectangleF clientRectangle, string text)
        {
            List<RectangleF> result = new();

            using StringFormat string_format = new();

            string_format.Alignment = StringAlignment.Center;
            string_format.LineAlignment = StringAlignment.Center;
            string_format.Trimming = StringTrimming.None;
            string_format.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;

            CharacterRange[] ranges = new CharacterRange[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                ranges[i] = new CharacterRange(i, 1);
            }
            string_format.SetMeasurableCharacterRanges(ranges);

            // Find the character ranges.
            RectangleF rect = new RectangleF(0, 0, 10000, 100);
            Region[] regions =
                gfx.MeasureCharacterRanges(
                    text, font, clientRectangle,
                    string_format);

            // Convert the regions into rectangles.
            foreach (Region region in regions)
                result.Add(region.GetBounds(gfx));

            return result;
        }

        /// <summary>
        /// Measure the characters in the string.
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="font"></param>
        /// <param name="clientRectangle"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private List<RectangleF> MeasureCharacters(Graphics gfx, System.Drawing.Font font, RectangleF clientRectangle, string text)
        {
            List<RectangleF> results = new List<RectangleF>();

            // The X location for the next character.
            float x = 0;

            // Get the character sizes 31 characters at a time.
            for (int start = 0; start < text.Length; start += 32)
            {
                // Get the substring.
                int len = 32;
                if (start + len >= text.Length) len = text.Length - start;
                string substring = text.Substring(start, len);

                // Measure the characters.
                List<RectangleF> rects =
                    MeasureCharactersInWord(gfx, font, clientRectangle, substring);

                // Remove lead-in for the first character.
                if (start == 0) x += rects[0].Left;

                // Save all but the last rectangle.
                for (int i = 0; i < rects.Count + 1 - 1; i++)
                {
                    RectangleF new_rect = new RectangleF(
                        x, rects[i].Top,
                        rects[i].Width, rects[i].Height);
                    results.Add(new_rect);

                    // Move to the next character's X position.
                    x += rects[i].Width;
                }
            }

            // Return the results.
            return results;
        }

        #endregion DrawText routines

    }
}


// https://github.com/ScottPlot/ScottPlot/blob/master/src/ScottPlot/Enums/
// under RadialGauge.cs
namespace ScottPlot
{
    public enum RadialGaugeDirection
    {
        Clockwise,
        AntiClockwise
    }

    public enum RadialGaugeStart
    {
        InsideToOutside,
        OutsideToInside
    }

    public enum RadialGaugeMode
    {
        Stacked,
        Sequential,
        SingleGauge
    }

    public enum RadialGaugeLabelPos
    {
        Beginning = 0,
        Middle = 1,
        End = 2
    }
}


// https://github.com/ScottPlot/ScottPlot/blob/c18fd8842a0551db462aaa4190d548a1e3965e48/src/ScottPlot/Plot/Plot.Add.cs
namespace ScottPlot
{
    public partial class PlotExt: ScottPlot.Plot
    {
        /// <summary>
        /// Add a radar plot (a two-dimensional chart of three or more quantitative variables represented on axes starting from the same point)
        /// </summary>
        /// <param name="values">2D array containing categories (columns) and groups (rows)</param>
        /// <param name="independentAxes">if true, axis (category) values are scaled independently</param>
        /// <param name="maxValues">if provided, each category (column) is normalized to these values</param>
        /// <param name="disableFrameAndGrid">also make the plot frameless and disable its grid</param>
        /// <returns>the radar plot that was just created and added to the plot</returns>
        public ScottPlot.Plottable.RadialGaugePlot AddRadialGauge(double[] values, bool independentAxes = false, double? maxValues = null, bool disableFrameAndGrid = true)
        {

            Color[] colors = Enumerable.Range(0, values.Length)
                                       .Select(i => this.GetSettings(false).PlottablePalette.GetColor(i))   // modify later
                                       .ToArray();

            Color[] fills = colors.Select(x => Color.FromArgb(50, x)).ToArray();

            ScottPlot.Plottable.RadialGaugePlot plottable = new(values, colors);
            Add(plottable);

            if (disableFrameAndGrid)
            {
                Frameless();
                Grid(enable: false);
            }

            return plottable;
        }
    }
}
