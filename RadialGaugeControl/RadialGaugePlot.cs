using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
//using ScottPlot.Drawing;

// Credits:
//
// Inspired (and expanding) by https://github.com/dotnet-ad/Microcharts/blob/main/Sources/Microcharts/Charts/RadialGaugeChart.cs
//
// Lighten or darken color
// https://stackoverflow.com/questions/801406/c-create-a-lighter-darker-color-based-on-a-system-color
// https://www.pvladov.com/2012/09/make-color-lighter-or-darker.html
// https://gist.github.com/zihotki/09fc41d52981fb6f93a81ebf20b35cd5
//
// Circular Segment
// https://github.com/falahati/CircularProgressBar/blob/master/CircularProgressBar/CircularProgressBar.cs
// https://github.com/aalitor/AltoControls/blob/on-development/AltoControls/Controls/Circular%20Progress%20Bar.cs
//
// http://csharphelper.com/blog/2015/02/draw-lines-with-custom-end-caps-in-c/
//
// Text on path
// http://csharphelper.com/blog/2018/02/draw-text-on-a-circle-in-c/
// http://csharphelper.com/blog/2016/01/draw-text-on-a-curve-in-c/


namespace RadialGaugePlot
{
    /// <summary>
    /// A radial gauge chart is a graphical method of displaying scalar data in the form of 
    /// a chart made of circular gauges so that each scalar is represented by each gauge.
    /// 
    /// Data is managed using a single array where each element is asigned to each gauge.
    /// Internally this data is stored in a single array and is converted to angular paramters,
    /// through ComputeAngularData(), which are more suitable for drawing purposes and stored in a 2D array.
    /// </summary>
    public class RadialGaugePlot : Plot
    {
        #region Properties
        /// <summary>
        /// The maximum angular interval that the gauges will consist of.
        /// It takes values in the range [0-360], default value is 360. Outside this range, unexpected side-effects might happen.
        /// </summary>
        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("The maximum angular interval that the gauges will consist of.\nIt takes values in the range [0°-360°], default value is 360°. Outside this range, unexpected side-effects might happen.")]
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
        /// Data to be plotted.
        /// It's copied from of the data passed to either the constructor or the <see cref="Update(double[], bool)"/> method.
        /// </summary>
        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("Data to be plotted.")]
        public override double[] Data { get; set; }

        /// <summary>
        /// Angular data (rows: gauges; first column: initial angle; second column: swept angle) computed from <see cref="Data"/>
        /// </summary>
        protected double[,] DataAngular;

        /// <summary>
        /// Dimmed percentage used to draw the gauges' background.
        /// Values in the range [0-100], default value is 90 [percent]. Outside this range, unexpected side-effects might happen.
        /// </summary>
        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("Dimmed percentage used to draw the gauges' background.\nValues in the range [0-100], default value is 90 [percent]. Outside this range, unexpected side-effects might happen.")]
        public float DimPercentage
        {
            get => _DimPercentage;
            set => _DimPercentage = (value > 100 ? 100 : (value < 0 ? 0 : value));
        }
        private float _DimPercentage = 90f;

        /// <summary>
        /// Colors for each gauge. These colors are dimmed according to <see cref="DimPercentage"/> to draw the gauges' background.
        /// Length must be equal to the length of data passed to either the constructor or the <see cref="Update(double[], bool)"/> method.
        /// </summary>
        public override Color[] Colors { get; set; }

        /// <summary>
        /// Determines whether the gauges are drawn clockwise (default value) or anti-clockwise (counter clockwise).
        /// </summary>
        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("Determines whether the gauges are drawn clockwise (default value) or anti-clockwise (counter clockwise).")]
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
        /// Determines the gauge label position as a percentage of the gauge length
        /// 0 being the beginning and 100 (default value) the ending of the gauge.
        /// </summary>
        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("Determines the gauge label position as a percentage of the gauge length: 0 is the beginning and 100 the ending (default value).")]
        public float GaugeLabelPos
        {
            get => _GaugeLabelPos;
            set => _GaugeLabelPos = value > 100 ? 100 : (value < 0 ? 0 : value);
        }
        private float _GaugeLabelPos = 100;

        /// <summary>
        /// Labels for each gauge.
        /// Length must be equal to the length of data passed to either the constructor or the <see cref="Update(double[], bool)"/> method.
        /// </summary>
        public string[] GaugeLabels;

        /// <summary>
        /// <see langword="Color"/> of the value labels drawn inside the gauges.
        /// </summary>
        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("Color of the value labels drawn inside the gauges.")]
        public Color GaugeLabelsColor { get; set; } = Color.White;

        /// <summary>
        /// Size of the gague label text as a percentage of the gauge width.
        /// Values in the range [0-100], default value is 75 [percent]. Other values might produce unexpected side-effects.
        /// </summary>
        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("Size of the gague label text as a percentage of the gauge width.\nValues in the range [0-100], default value is 75 [percent]. Other values might produce unexpected side-effects.")]
        public float GaugeLabelsFontPct
        {
            get => _GaugeLabelsFontPct;
            set => _GaugeLabelsFontPct = (value > 100 ? 100 : (value < 0 ? 0 : value));
        }
        private float _GaugeLabelsFontPct = 75f;

        /// <summary>
        /// Determines whether the gauges are drawn stacked (dafault value), sequentially, or as a single gauge (ressembling a pie plot).
        /// </summary>
        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("Determines whether the gauges are drawn stacked (dafault value), sequentially, or as a single gauge (ressembling a pie plot).")]
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

        /// <summary>
        /// The empty space between gauges as a percentage of the gauge width.
        /// Values in the range [0-100], default value is 50 [percent]. Other values might produce unexpected side-effects.
        /// </summary>
        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("The empty space between gauges as a percentage of the gauge width.\nValues in the range [0-100], default value is 50 [percent]. Other values might produce unexpected side-effects.")]
        public float GaugeSpacePercentage
        {
            get => _GaugeSpacePercentage;
            set => _GaugeSpacePercentage = (value > 100 ? 100 : (value < 0 ? 0 : value));
        }
        private float _GaugeSpacePercentage = 50f;

        /// <summary>
        /// Determines whether the gauges are drawn starting from the inside (default value) or from the outside.
        /// </summary>
        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("Determines whether the gauges are drawn starting from the inside (default value) or from the outside.")]
        public RadialGaugeStart GaugeStart { get; set; } = RadialGaugeStart.InsideToOutside;

        /// <summary>
        /// Gets or sets the size (in pixels) of each gauge.
        /// If <0, then it will be calculated from the available space.
        /// </summary>
        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("Gets or sets the size (in pixels) of each gauge.\nIf <0, then it will be calculated from the available space.")]
        public float LineWidth { get; set; } = -1;

        /// <summary>
        /// The maximum value for scaling the gauges.
        /// This value is associated to <see cref="StartingAngleGauges"/> and to <see cref="AngleRange"/>.
        /// </summary>
        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("Tha maximum value for scaling the gauges.\nThis value is associated to StartingAngleGauges and to AngleRange.")]
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
        /// The minimum value for scaling the gauges.
        /// This value is associated to <see cref="StartingAngleGauges"/> and to <see cref="AngleRange"/>.
        /// </summary>
        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("The minimum value for scaling the gauges.\nThis value is associated to StartingAngleGauges and to AngleRange.")]
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
        /// <see langword="True"/> if the gauges' background is adjusted to <see cref="StartingAngleGauges"/>.
        /// Default value is set to <see langword="False"/>.
        /// </summary>
        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("True if the gauges' background is adjusted to StartingAngleGauges.\nDefault value is set to False.")]
        public bool NormBackGauge { get; set; } = false;

        /// <summary>
        /// The initial angle (in degrees) where the background gauges begin. Default value is 270° the same as <see cref="StartingAngleGauges"/>.
        /// </summary>
        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("The initial angle (in degrees) where the background gauges begin.\nDefault value is 270° the same as StartingAngleGauges.")]
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
        /// Angle (in degrees) at which the gauges start: 270° for North (default value), 0° for East, 90° for South, 180° for West, and so on.
        /// Expected values in the range [0°-360°], otherwise unexpected side-effects might happen.
        /// </summary>
        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("Angle (in degrees) at which the gauges start: 270° for North (default value), 0° for East, 90° for South, 180° for West, and so on.\nExpected values in the range [0°-360°], otherwise unexpected side-effects might happen.")]
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
        /// <see langword="True"/> if value labels are shown inside the gauges.
        /// Size of the text is set by <see cref="GaugeLabelsFontPct"/> and color by <see cref="GaugeLabelsColor"/>.
        /// </summary>
        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("True if value labels are shown inside the gauges.\nSize of the text is set by GaugeLabelsFontPct and color by GaugeLabelsColor.")]
        public bool ShowGaugeValues { get; set; } = true;

        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("End cap line style.")]
        public System.Drawing.Drawing2D.LineCap EndCap { get; set; } = System.Drawing.Drawing2D.LineCap.Triangle;

        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("Start cap line style.")]
        public System.Drawing.Drawing2D.LineCap StartCap { get; set; } = System.Drawing.Drawing2D.LineCap.Round;

        [System.ComponentModel.Category("Radial gauge"),
        System.ComponentModel.Description("Title string which is rendered at the top of the plot.")]
        public override string PlotTitle { get => base.PlotTitle; set => base.PlotTitle = value; }

        #endregion Properties

        public RadialGaugePlot()
        {
            base.Xaxis.Visible = false;
            base.Yaxis.Visible = false;

            double[] values = { 100, 80, 65, 45, -20 };
            Update(values);
            ComputeAngularData();
        }

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="values">Array of values to be plotted as gauges.</param>
        /// <param name="labels">Legend labels.</param>
        /// <param name="lineColors">Array colors for the gauges.</param>
        public RadialGaugePlot(double[] values, string[] labels = null, Color[] lineColors = null)
            :base()
        {
            //if (labels != null && labels.Length > 0)
            //{
            //    base.LegendLabels = labels;
            //    //base.Legend.IsVisible = true;
            //}

            //if (lineColors != null && lineColors.Length > 0)
            //    Colors = lineColors;

            Update(values, labels, lineColors);
            //ComputeAngularData();
        }

        public override string ToString() => $"RadialGauge with {Data.Length} points.";

        /// <summary>
        /// Replace the data values with new ones. This passed data is copied and stored in <see cref="Data"/>.
        /// It implicitly calls the <see cref="ComputeAngularData"/> routine.
        /// </summary>
        /// <param name="values">Array of values to be plotted as gauges.</param>
        /// <param name="labels">Legend labels.</param>
        /// <param name="lineColors">Array colors for the gauges.</param>
        public void Update(double[] values, string[] labels = null, Color[] lineColors = null)
        {
            Data = new double[values.Length];
            Array.Copy(values, 0, Data, 0, values.Length);

            if (labels != null && labels.Length > 0)
            {
                base.LegendLabels = labels;
                //base.Legend.IsVisible = true;
            }

            if (lineColors != null && lineColors.Length > 0)
                Colors = lineColors;

            // Validate the input data
            Validate();

            // Sets MaxScale and MinScale values and calls ComputeAngularData
            ComputeMaxMin();
            ComputeAngularData();
        }

        public override LegendItem[] GetLegendItems()
        {
            if (GaugeLabels is null)
                return null;

            List<LegendItem> legendItems = new();

            for (int i = 0; i < Math.Min(Data.Length, GaugeLabels.Length); i++)
            {
                var item = new LegendItem()
                {
                    label = GaugeLabels[i],
                    color = Colors[i],
                    lineWidth = 10,
                    markerShape = MarkerShape.none
                };
                legendItems.Add(item);
            }

            return legendItems.ToArray();
        }

        /// <summary>
        /// In case the original data should be needed from a caller
        /// </summary>
        /// <returns>The original data</returns>
        public double[] GetData() => Data;
        
        /// <summary>
        /// In case the angular data should be needed from a caller
        /// </summary>
        /// <returns>The angular data</returns>
        public double[,] GetAngularData() => DataAngular;

        /// <summary>
        /// Converts <see cref="Data"/> into <see cref="DataAngular"/>.
        /// Depends on <see cref="Data"/>, <see cref="GaugeMode"/>, <see cref="GaugeDirection"/>, <see cref="StartingAngleGauges"/>, <see cref="AngleRange"/>, and <see cref="MaxScale"/>
        /// </summary>
        private void ComputeAngularData()
        {
            // Check if there's data
            if (Data == null) return;
            DataAngular = new double[Data.Length, 2];

            // Internal variables
            float AngleSumPos = _StartingAngleGauges;
            float AngleSumNeg = _StartingAngleGauges;
            float AngleSwept;
            float AngleInit;
            
            // Loop through DataRaw and compute DataAngular
            for (int i = 0; i < Data.Length; i++)
            {
                AngleInit = (Data[i] >= 0 ? AngleSumPos : AngleSumNeg);
                AngleSwept = (_GaugeDirection == RadialGaugeDirection.AntiClockwise ? -1 : 1) * (float)(_AngleRange * Data[i] / (_MaxScale - _MinScale));

                DataAngular[i, 0] = (_GaugeMode == RadialGaugeMode.Stacked ? _StartingAngleGauges : AngleInit);
                DataAngular[i, 1] = AngleSwept;

                if (Data[i] >= 0)
                    AngleSumPos += AngleSwept;
                else
                    AngleSumNeg += AngleSwept;
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
                _MaxScale = Data.Sum(x => Math.Abs(x));
                _MinScale = 0;
            }
            else
            {
                _MaxScale = Data.Max(x => Math.Abs(x));
                var min = Data.Min();
                _MinScale = min < 0 ? min : 0;
            }
        }

        /// <summary>
        /// Needed as part of IPlottable in ScottPlot.ScottForm
        /// </summary>
        /// <param name="deep"></param>
        public void ValidateData(bool deep = false)
        {
            

            if (Colors.Length != Data.Length)
                throw new InvalidOperationException($"{nameof(Colors)} must be an array with length equal to number of values");
            
            if (GaugeLabels != null && GaugeLabels.Length != Data.Length)
                throw new InvalidOperationException($"If {nameof(GaugeLabels)} is not null, it must match size of data values");

            if (AngleRange < 0 || AngleRange > 360)
                throw new InvalidOperationException($"{nameof(AngleRange)} must be [0°-360°]");

            if (GaugeLabelsFontPct < 0 || GaugeLabelsFontPct > 100)
                throw new InvalidOperationException($"{nameof(GaugeLabelsFontPct)} must be a value from 0 to 100");

            if (GaugeSpacePercentage < 0 || GaugeSpacePercentage > 100)
                throw new InvalidOperationException($"{nameof(GaugeSpacePercentage)} must be from 0 to 100");
        }

        /// <summary>
        /// Reduces an angle into the range [0° - 360°]
        /// </summary>
        /// <param name="angle">Angle value</param>
        /// <returns>Return the angle whithin [0° - 360°]</returns>
        private double ReduceAngle(double angle)
        {
            // This reduces the angle to [-360 - 360]
            double reduced = angle - 360 * (int)(angle / 360);

            // This reduces the angle to [0 - 360]
            if (reduced < 0) reduced += 360.0;

            return reduced;
        }

        /// <summary>
        /// This is where the drawing of the plot is done
        /// </summary>
        /// <param name="dims">Plot dimensions</param>
        /// <param name="bmp">Bitmap where the drawing is done</param>
        /// <param name="lowQuality">Image quality</param>
        public override void Render(Bitmap bmp, bool lowQuality = false)
        {
            int numGroups = Data.Length;
            RectangleF RectData = base.Chart.GetRectangleIn();
            double minScale = Math.Min(RectData.Width, RectData.Height) / 2;

            using Graphics gfx = Graphics.FromImage(bmp);   // https://github.com/ScottPlot/ScottPlot/blob/master/src/ScottPlot/Drawing/GDI.cs;
            gfx.SmoothingMode = lowQuality ? System.Drawing.Drawing2D.SmoothingMode.HighSpeed : System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gfx.TextRenderingHint = lowQuality ? System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit : System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            using Pen pen = new (Color.Black);
            using Pen penCircle = new(Color.Black);
            using Brush labelBrush = new SolidBrush(GaugeLabelsColor);

            float lineWidth = (LineWidth < 0) ? (float)(minScale / ((numGroups + 0.33) * (GaugeSpacePercentage + 100) / 100)) : LineWidth;
            float radiusSpace = lineWidth * (GaugeSpacePercentage + 100) / 100;
            float gaugeRadius = numGroups * radiusSpace;  // By default, the outer-most radius is computed
            float maxBackAngle = (GaugeDirection == RadialGaugeDirection.AntiClockwise ? -1 : 1) * (NormBackGauge ? (float)AngleRange : 360);

            pen.Width = (float)lineWidth;
            pen.StartCap = StartCap;
            pen.EndCap = EndCap;
            penCircle.Width = (float)lineWidth;
            penCircle.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            penCircle.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            using System.Drawing.Font fontGauge = new(Font.Name, lineWidth * GaugeLabelsFontPct / 100, FontStyle.Bold);

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
                pen.Color = Colors[index];
                penCircle.Color = LightenBy(Colors[index], DimPercentage);

                // Draw gauge background
                if (GaugeMode != RadialGaugeMode.SingleGauge)
                    gfx.DrawArc(penCircle,
                        (RectData.X + RectData.Width/2 - gaugeRadius),
                        (RectData.Y + RectData.Height / 2 - gaugeRadius),
                        (gaugeRadius * 2),
                        (gaugeRadius * 2),
                        _StartingAngleBackGauges, maxBackAngle);

                // Draw gauge
                gfx.DrawArc(pen,
                    (RectData.X + RectData.Width / 2 - gaugeRadius),
                    (RectData.Y + RectData.Height / 2 - gaugeRadius),
                    (gaugeRadius * 2),
                    (gaugeRadius * 2),
                    (float)DataAngular[index, 0],
                    (float)DataAngular[index, 1]);

                // Draw gauge labels
                if (ShowGaugeValues)
                {
                    DrawTextOnCircle(gfx,
                        fontGauge,
                        labelBrush,
                        new RectangleF(RectData.X, RectData.Y, RectData.Width, RectData.Height),
                        gaugeRadius,
                        (float)DataAngular[index, 0],
                        (float)DataAngular[index, 1],
                        RectData.X + RectData.Width / 2,
                        RectData.Y + RectData.Height / 2,
                        Data[index].ToString("0.##"),
                        _GaugeLabelPos);
                }

            }
            //gfx.DrawRectangle(new Pen(Color.Black,2), RectData.X, RectData.Y, RectData.Width, RectData.Height);
            base.Render(bmp);

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
                correctionFactor = correctionFactor < -1 ? 0 : 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                if (correctionFactor > 1) correctionFactor = 1;
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
        /// <param name="gfx"><see langword="keyword">Graphic</see> object used to draw.</param>
        /// <param name="font"><see langword="keyword">Font</see> used to draw the text.</param>
        /// <param name="brush"><see langword="keyword">Brush</see> used to draw the text.</param>
        /// <param name="clientRectangle">Clipping <see langword="keyword">RectangleF</see> holding the text. Normally, the maximum of the drawing area.</param>
        /// <param name="radius">Radius of the circle in pixels.</param>
        /// <param name="angleInit">Gauge's starting angle.</param>
        /// <param name="angleSwept">Gauge's angular range (span).</param>
        /// <param name="cx">The x-coordinate of the circle centre.</param>
        /// <param name="cy">The y-coordinate of the circle centre.</param>
        /// <param name="text">String of text to be drawn.</param>
        /// <param name="posPct">Label's position as a percentage of the gauge's angular range.</param>
        /// <seealso cref="http://csharphelper.com/blog/2018/02/draw-text-on-a-circle-in-c/"/>
        protected virtual void DrawTextOnCircle(Graphics gfx, System.Drawing.Font font,
            Brush brush, RectangleF clientRectangle, float radius, float angleInit, float angleSwept, float cx, float cy,
            string text, float posPct)
        {
            // Modify anglePos to be in the range [0, 360]
            angleInit = (float)ReduceAngle(angleInit);

            // Use a StringFormat to draw the middle top of each character at (0, 0).
            using StringFormat string_format = new ();
            string_format.Alignment = StringAlignment.Center;
            string_format.LineAlignment = StringAlignment.Center;

            // Used to scale from radians to degrees.
            double RadToDeg = 180.0 / Math.PI;
            double width_to_angle = 1 / radius;

            // Measure the characters. Use LINQ to add up the character widths.
            //List<RectangleF> rects = MeasureCharacters(gfx, font, clientRectangle, text);
            //var width_query = from RectangleF rect in rects select rect.Width;
            //double text_width = width_query.Sum() / radius;

            RectangleF[] letterRectangles = MeasureCharacters(gfx, font, text, clientRectangle);
            double totalLetterWidths = letterRectangles.Select(rect => rect.Width).Sum();
            double text_width = totalLetterWidths / radius;

            // Angular data
            bool isPositive = angleSwept >= 0;
            double angle = ReduceAngle(angleInit + angleSwept * (posPct / 100));
            angle += (1 - 2 * (posPct / 100)) * (isPositive ? 1 : -1) * RadToDeg * text_width / 2; // Set the position to the middle of the text

            bool isBelow = angle < 180 && angle > 0;
            int sign = isBelow ? 1 : -1;
            double theta = angle * Math.PI / 180;
            theta += sign * text_width / 2;   // Set the position to the beginning of the text

            // Draw the characters.
            for (int i = 0; i < text.Length; i++)
            {
                // Increment theta half the angular width of the current character
                theta -= sign * letterRectangles[i].Width / 2 * width_to_angle;

                // Calculate the position of the upper-left corner
                double x = cx + radius * Math.Cos(theta);
                double y = cy + radius * Math.Sin(theta);

                gfx.RotateTransform((float)(RadToDeg * (theta - sign * Math.PI / 2)));
                gfx.TranslateTransform((float)x, (float)y, System.Drawing.Drawing2D.MatrixOrder.Append);
                gfx.DrawString(text[i].ToString(), font, brush, 0, 0, string_format);
                gfx.ResetTransform();

                // Increment theta the remaining half character.
                theta -= sign * letterRectangles[i].Width / 2 * width_to_angle;
            }
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
            List<RectangleF> results = new ();

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
                    RectangleF new_rect = new (x, rects[i].Top, rects[i].Width, rects[i].Height);
                    results.Add(new_rect);

                    // Move to the next character's X position.
                    x += rects[i].Width;
                }
            }

            // Return the results.
            return results;
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
            RectangleF rect = new (0, 0, 10000, 100);
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
        /// Return an array indicating the size of each character in a string.
        /// Specifiy the maximum expected size to avoid issues associated with text wrapping.
        /// </summary>
        /// <param name="gfx"><see langword="keyword">Graphic</see> object used to draw.</param>
        /// <param name="font"><see langword="keyword">Font</see> used to draw the text.</param>
        /// <param name="text">String of text to be drawn.</param>
        /// <param name="clientRect">Clipping <see langword="keyword">RectangleF</see> holding the text. Normally, the maximum of the drawing area.</param>
        /// <returns></returns>
        private static RectangleF[] MeasureCharacters(Graphics gfx, System.Drawing.Font font, string text, RectangleF clientRect)
        {
            using StringFormat stringFormat = new()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.None,
                FormatFlags = StringFormatFlags.MeasureTrailingSpaces,
            };

            CharacterRange[] charRanges = Enumerable.Range(0, text.Length)
                .Select(x => new CharacterRange(x, 1))
                .ToArray();

            stringFormat.SetMeasurableCharacterRanges(charRanges);

            //RectangleF imageRectangle = new(0, 0, maxWidth, maxHeight);
            Region[] characterRegions = gfx.MeasureCharacterRanges(text, font, clientRect, stringFormat);
            RectangleF[] characterRectangles = characterRegions.Select(x => x.GetBounds(gfx)).ToArray();

            return characterRectangles;

        }

        #endregion DrawText routines
    }
}




