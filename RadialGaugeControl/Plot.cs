using System;
using System.Drawing;
using System.Windows.Forms;

namespace RadialGaugeControl
{
    public partial class Plot : UserControl
    {
        public Plot()
        {
            InitializeComponent();
        }

        private void Plot_SizeChanged(object sender, EventArgs e)
        {
            Bitmap bmp = new System.Drawing.Bitmap((int)Width, (int)Height);
            pictureBox1.Image = Render(bmp);
        }

        protected virtual Bitmap Render(Bitmap bmp, bool lowQuality = false)
        {
            return bmp;
        }
    }
}
