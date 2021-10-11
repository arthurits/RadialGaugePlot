using System;
using System.Drawing;
using System.Windows.Forms;

// https://devblogs.microsoft.com/nuget/add-a-readme-to-your-nuget-package/
namespace RadialGaugePlot
{
    // https://weblog.west-wind.com/posts/2020/Apr/06/Displaying-Nested-Child-Objects-in-the-Windows-Forms-Designer-Property-Grid
    public partial class Plot : UserControl
    {
        [System.ComponentModel.Category("Plot"),
        System.ComponentModel.Description("Defines the space to be used to separate all rectangle areas as a percentage of the minimum dimension")]
        public float MarginFactor { get; set; } = 0.03f;

        [System.ComponentModel.Category("Plot"),
        System.ComponentModel.Description("Defines the space inpixels to be used to separate all rectangle areas")]
        public int MarginSpace { get; private set; } = 0;

        [System.ComponentModel.Category("Plot"),
        System.ComponentModel.Description("Center coordinates of the control")
        System.ComponentModel.DisplayName("Center point")]
        public PointF Center { get; set; }

        /// <summary>
        /// Data to be plotted.
        /// </summary>
        [System.ComponentModel.Category("Plot"),
        System.ComponentModel.Description("Data to be plotted")]
        public virtual double[] Data { get; set; }

        /// <summary>
        /// Colors for series.
        /// </summary>
        [System.ComponentModel.Category("Plot"),
        System.ComponentModel.Description("Series colors")]
        public virtual Color[] Colors { get; set; }

        /// <summary>
        /// Plot title. Leave empty if no title...
        /// </summary>
        [System.ComponentModel.Category("Plot"),
        System.ComponentModel.Description("Title of the plot")]
        public virtual string PlotTitle
        {
            get => _strTitle;
            set
            {
                if(!string.IsNullOrEmpty(value))
                {
                    _strTitle = value;
                    Title.Text = value;
                    ComputeRects();
                }
            }
        }
        private string _strTitle;

        [System.ComponentModel.Category("Plot"),
        System.ComponentModel.Description("Plot legend labels.")]
        public string[] LegendLabels { get; protected set; }

        [System.ComponentModel.Category("Plot"),
        System.ComponentModel.Description("Plot legend.")]
        public Legend Legend { get; protected set; }

        [System.ComponentModel.Category("Plot"),
        System.ComponentModel.Description("Defines the area and properties of the chart's title")]
        public PlotElement Title { get; set; }

        [System.ComponentModel.Category("Plot"),
        System.ComponentModel.Description("Defines the area and properties where data is drawn.")]
        public PlotElement Chart { get; set; }

        [System.ComponentModel.Category("Plot"),
        System.ComponentModel.Description("Defines the area and properties of the chart's x-axis")]
        public PlotElement Xaxis { get; set; }

        [System.ComponentModel.Category("Plot"),
        System.ComponentModel.Description("Defines the area and properties of the chart's y-axis")]
        public PlotElement Yaxis { get; set; }

        /// <summary>
        /// The palette defines the default colors given to the plot
        /// </summary>
        [System.ComponentModel.Category("Plot"),
        System.ComponentModel.Description("Plot colorset defining the colors to be used each time the plot is rendered.")]
        public virtual Plotting.Colorsets.Palette Palette { get; set; } = Plotting.Colorsets.Palette.Microcharts;


        public Plot()
        {
            InitializeComponent();
            
            // Initializes the plot elements
            Legend = new();

            Title = new();
            Title.Render = RenderText;
            Title.Text = PlotTitle;
            Title.Font.Size = Math.Min(Width, Height) * 0.05f;
            
            Chart = new();
            Chart.Render = Render;

            Xaxis = new();
            Xaxis.Render = RenderAxis;

            Yaxis = new();
            Yaxis.Render = RenderAxis;
        }


        private void OnSizeChanged(object sender, EventArgs e)
        {
            // Calculate the space between areas
            MarginSpace = (int)(Math.Min(Width, Height) * MarginFactor);

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

        /// <summary>
        /// Rendering function. This is the master function that calls all the elements whether they are overriden by a derived class or not.
        /// </summary>
        /// <param name="lowQuality">Quality of the rendering</param>
        public void Render(bool lowQuality = false)
        {
            // Get the needed colors from the defined palette
            Colors = Palette.GetColors(Data.Length);

            // Compute dimensions before any drawing takes place
            ComputeRects();

            // First call the virtual Render() function (which can be overriden by a derived class)
            Bitmap bmp = new((int)Width, (int)Height);
            Render(bmp, lowQuality);

            // Draw the title onto the bitmap
            if (Title.Visible && !string.IsNullOrEmpty(Title.Text))
            {
                Title.Render(bmp, lowQuality);
            }

            // Draw the legend onto the bitmap
            LegendItem[] legendItems = GetLegendItems();
            if (legendItems != null && legendItems.Length > 0)
            {
                Legend.LegendItems = legendItems;
                Legend.IsVisible = true;
                Legend.Render(new Rectangle(0, 0, Width, Height), bmp, lowQuality);
            }

            // Set the image into the picturebox control
            SetImage(bmp);
        }

        public virtual void Render(Bitmap bmp, bool lowQuality = false)
        {
            using Graphics newGraphics = Graphics.FromImage(bmp);
            newGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            newGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            newGraphics.DrawRectangle(new Pen(Color.Black), System.Drawing.Rectangle.Round(Chart.GetRectangle()));
            newGraphics.DrawRectangle(new Pen(Color.Black), System.Drawing.Rectangle.Round(Chart.GetRectangleEx()));
            newGraphics.DrawRectangle(new Pen(Color.Black), System.Drawing.Rectangle.Round(Title.GetRectangle()));
            newGraphics.DrawRectangle(new Pen(Color.Black), System.Drawing.Rectangle.Round(Title.GetRectangleEx()));

        }

        /// <summary>
        /// Gets an array of LegenItem
        /// </summary>
        /// <returns></returns>
        public virtual LegendItem[] GetLegendItems()
        {
            return null;
        }

        /// <summary>
        /// Gets the legend bitmap
        /// </summary>
        /// <returns></returns>
        public Bitmap GetLegendBitmap()
        {
            return Legend.GetBitmap();
        }

        /// <summary>
        /// Gets the bitmap shown in the plot.
        /// </summary>
        /// <returns></returns>
        public Image GetImage()
        {
            return this.pictureBox1.Image;
        }

        /// <summary>
        /// Compute geometric data
        /// </summary>
        protected virtual void ComputeRects()
        {
            // Compute the center point of the control
            Center = new(Width / 2, Height / 2);
            
            // Compute the minimum dimension of the control
            float min = Math.Min(Width, Height);

            // Compute the title area rectangle
            SizeF sizeText = new(0, 0);
            if (Title.Visible && !string.IsNullOrEmpty(Title.Text))
            {
                using Graphics gfx = this.pictureBox1.CreateGraphics();
                sizeText = Drawing.GDI.MeasureString(gfx, Title.Text, Title.Font);
                Title.Margin = new Padding(MarginSpace, MarginSpace, MarginSpace, 0);
            }
            Title.Rectangle = new RectangleF(new PointF((Width - sizeText.Width) / 2, Title.Margin.Top + Title.Padding.Top), sizeText);

            // Compute the chart area rectangle
            Chart.Margin = new Padding(MarginSpace);
            var rect = new RectangleF(Center.X,
                Center.Y + (Title.GetRectangleEx().Height + Chart.Margin.Top - Chart.Margin.Bottom) / 2,
                0,
                0);
            rect.Inflate(min/2, min/2 - (Title.GetRectangleEx().Height + Chart.Margin.Top + Chart.Margin.Bottom) / 2);
            Chart.Rectangle = rect;

            //// Compute the x-axis area rectangle
            //if (Xaxis.Visible && !string.IsNullOrEmpty(Xaxis.Text))
            //{
            //    Xaxis.Margin = new Padding(-MarginSpace, MarginSpace, MarginSpace, MarginSpace);
            //}
            //Xaxis.Rectangle = new RectangleF(new PointF((Width - sizeText.Width) / 2, Title.Margin.Top + Title.Padding.Top), sizeText);
        }


        /// <summary>
        /// Function for drawing text. This can be overriden if needed.
        /// </summary>
        /// <param name="bmp"><see cref="Bitmap"/> where the text is rendered</param>
        /// <param name="lowQuality">Render quality</param>
        protected virtual void RenderText (Bitmap bmp, bool lowQuality = false)
        {
            using Graphics newGraphics = Graphics.FromImage(bmp);
            using Font TitleFont = Drawing.GDI.Font(Title.Font);
            newGraphics.DrawString(Title.Text, TitleFont, new SolidBrush(Title.Font.Color), Title.GetRectangle());
        }

        /// <summary>
        /// Function for drawing the axes. This can be overriden if needed.
        /// </summary>
        /// <param name="bmp"><see cref="Bitmap"/> where the text is rendered</param>
        /// <param name="lowQuality">Render quality</param>
        protected virtual void RenderAxis(Bitmap bmp, bool lowQuality = false)
        {
            throw new NotImplementedException();
        }
    }
}
