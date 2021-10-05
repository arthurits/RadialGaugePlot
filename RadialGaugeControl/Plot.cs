using System;
using System.Drawing;
using System.Windows.Forms;

// https://devblogs.microsoft.com/nuget/add-a-readme-to-your-nuget-package/
namespace RadialGaugePlot
{
    public partial class Plot : UserControl
    {
        [System.ComponentModel.Category("Plot"),
        System.ComponentModel.Description("Defines the space to be used to separate all rectangle areas as a percentage of the minimum dimension")]
        public float MarginSpace { get; set; } = 0.05f;
        
        [System.ComponentModel.Category("Plot"),
        System.ComponentModel.Description("Rectangle of the plot title")]
        public RectangleF RectTitle { get; set; }

        [System.ComponentModel.Category("Plot"),
        System.ComponentModel.Description("Rectangle of the plot graph")]
        public RectangleF RectData { get; set; }

        [System.ComponentModel.Category("Plot"),
        System.ComponentModel.Description("Center coordinates of the control")]
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

        public PlotElement Title { get; set; }

        public PlotElement Xaxis { get; set; }

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
            Xaxis = new();
            Xaxis.Render = RenderAxis;
            Xaxis.Visible = false;
        }



        private void OnLoad(object sender, EventArgs e)
        {

        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
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
            //Center = new(Width / 2, Height / 2);
            ComputeRects();

            // First call the virtual Render() function (which can be overriden by a derived class)
            Bitmap bmp = new((int)Width, (int)Height);
            Render(bmp, lowQuality);

            // Draw the title onto the bitmap
            if (!string.IsNullOrEmpty(Title.Text))
            {
                using Graphics newGraphics = Graphics.FromImage(bmp);
                newGraphics.DrawString(Title.Text, Font, new SolidBrush(Color.Black), RectTitle);
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
            newGraphics.DrawRectangle(new Pen(Color.Black), RectData.X, RectData.Y, RectData.Width, RectData.Height);
            newGraphics.DrawRectangle(new Pen(Color.Black), RectTitle.X, RectTitle.Y, RectTitle.Width, RectTitle.Height);
            if (!string.IsNullOrEmpty(Title.Text))
                newGraphics.DrawString(Title.Text, Font, new SolidBrush(Color.Black), RectTitle);
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

            // Compute the Title rect
            //using Bitmap bmp = new (1, 1);
            //using Graphics gfx = System.Drawing.Graphics.FromImage(bmp);
            using Graphics gfx = this.pictureBox1.CreateGraphics();

            SizeF sizeText = new (0, 0);
            if (!string.IsNullOrEmpty(Title.Text))
            {
                sizeText = gfx.MeasureString(Title.Text, Font);
                FontScaling(sizeText, Font.SizeInPoints);
            }

            RectTitle = new RectangleF(new PointF((Width - sizeText.Width) / 2, sizeText.Height * 0.5f), sizeText);

            // Compute the minimum dimension of the control and substract 2 times the space for the title
            float min = Math.Min(Width, Height);
            min /= 2;
            //if (!string.IsNullOrEmpty(Title))
            //    min -= 2 * RectTitle.Height;

            //RectData = new RectangleF(Center.X - min, Center.Y - min, 2 * min, 2 * min);
            RectData = new RectangleF(Center.X, Center.Y + 2 * RectTitle.Height / 2, 0, 0);
            RectData = RectangleF.Inflate(RectData, min * (1 - MarginSpace) - RectTitle.Height/2, min * (1 - MarginSpace) - 2*RectTitle.Height/2);
            //RectData = RectangleF.Inflate(RectData, min, min);
        }

        /// <summary>
        /// Compensate for OS-specific differences in font scaling.
        /// </summary>
        /// <param name="sizeText"><see cref="Size"/> struct containing the text.</param>
        protected virtual void FontScaling(SizeF sizeText, float fontSize = 0)
        {
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

            // Ensure the measured height is at least the font size
            sizeText.Height = Math.Max(fontSize, sizeText.Height);
        }

        protected virtual void RenderText (Bitmap bmp, bool lowQuality = false)
        {
            throw new NotImplementedException();
        }

        protected virtual void RenderAxis(Bitmap bmp, bool lowQuality = false)
        {
            throw new NotImplementedException();
        }
    }
}
