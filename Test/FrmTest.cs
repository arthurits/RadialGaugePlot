using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class FrmTest : Form
    {
        private ScottPlot.Plottable.RadialGaugePlot plottable;
        public FrmTest()
        {
            InitializeComponent();

            double[] values = {100, 80, 65, 45, -20 };
            formsPlot1.Plot.Palette = ScottPlot.Drawing.Palette.Nord;
            Color[] colors = Enumerable.Range(0, values.Length)
                                       .Select(i => formsPlot1.Plot.GetSettings(false).PlottablePalette.GetColor(i))   // modify later
                                       .ToArray();
            colors = new Color[]
            {
                ColorTranslator.FromHtml("#266489"),
                ColorTranslator.FromHtml("#68B9C0"),
                ColorTranslator.FromHtml("#90D585"),
                ColorTranslator.FromHtml("#F3C151"),
                ColorTranslator.FromHtml("#F37F64"),
                ColorTranslator.FromHtml("#424856"),
                ColorTranslator.FromHtml("#8F97A4"),
                ColorTranslator.FromHtml("#DAC096"),
                ColorTranslator.FromHtml("#76846E"),
                ColorTranslator.FromHtml("#DABFAF"),
                ColorTranslator.FromHtml("#A65B69"),
                ColorTranslator.FromHtml("#97A69D")
            };

            //ScottPlot.Plottable.RadialGaugePlot plottable = new(values, colors, false, new double []{ values.Max() * 4 / 3 });
            plottable = new(values, colors);
            plottable.GaugeLabels = new string[] { "Data #1", "Data #2", "Data #3", "Data #4", "Data #5" };
            //plottable.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            formsPlot1.Plot.Add(plottable);
            formsPlot1.Plot.Frameless();
            formsPlot1.Plot.Grid(enable: false);
            //formsPlot1.Plot.XAxis2.Label("Radial gauge plot");
            formsPlot1.Plot.Title("Radial gauge plot");
            formsPlot1.Plot.Legend(enable: true, ScottPlot.Alignment.UpperRight);

            // Combo boxes
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 2;

            // Check boxes
            checkBox1.Checked = plottable.ShowGaugeValues;
            checkBox2.Checked = plottable.NormBackGauge;

            // Other numeric controls
            numStart.Value = (decimal)plottable.StartingAngleGauges;
            numSpace.Value = (decimal)plottable.GaugeSpacePercentage;
            numDim.Value = (decimal)plottable.DimPercentage;
            numRange.Value = (decimal)plottable.AngleRange;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            plottable.GaugeDirection = (ScottPlot.RadialGaugeDirection)comboBox1.SelectedIndex;
            formsPlot1.Render();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            plottable.GaugeMode = (ScottPlot.RadialGaugeMode)comboBox2.SelectedIndex;
            formsPlot1.Render();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            plottable.GaugeStart = (ScottPlot.RadialGaugeStart)comboBox3.SelectedIndex;
            formsPlot1.Render();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            plottable.GaugeLabelPos = (ScottPlot.RadialGaugeLabelPos)comboBox4.SelectedIndex;
            formsPlot1.Render();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            plottable.ShowGaugeValues = checkBox1.Checked;
            comboBox4.Enabled = checkBox1.Checked;
            formsPlot1.Render();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            plottable.NormBackGauge = checkBox2.Checked;
            formsPlot1.Render();
        }

        private void numStart_ValueChanged(object sender, EventArgs e)
        {
            int ratio = Convert.ToInt32(numStart.Value);
            if (trackStart.Value != ratio) trackStart.Value = ratio;

            plottable.StartingAngleGauges = (float)numStart.Value;
            formsPlot1.Render();
        }

        private void trackStart_ValueChanged(object sender, EventArgs e)
        {
            int ratio = trackStart.Value;
            if (numStart.Value != ratio) numStart.Value = ratio;
        }

        private void numSpace_ValueChanged(object sender, EventArgs e)
        {
            int ratio = Convert.ToInt32(numSpace.Value);
            if (trackSpace.Value != ratio) trackSpace.Value = ratio;

            plottable.GaugeSpacePercentage = (float)numSpace.Value;
            formsPlot1.Render();
        }

        private void trackSpace_ValueChanged(object sender, EventArgs e)
        {
            int ratio = trackSpace.Value;
            if (numSpace.Value != ratio) numSpace.Value = ratio;
        }

        private void numDim_ValueChanged(object sender, EventArgs e)
        {
            int ratio = Convert.ToInt32(numDim.Value);
            if (trackDim.Value != ratio) trackDim.Value = ratio;

            plottable.DimPercentage = (float)numDim.Value;
            formsPlot1.Render();
        }

        private void trackDim_ValueChanged(object sender, EventArgs e)
        {
            int ratio = trackDim.Value;
            if (numDim.Value != ratio) numDim.Value = ratio;
        }

        private void numMax_ValueChanged(object sender, EventArgs e)
        {
            int ratio = Convert.ToInt32(numRange.Value);
            if (trackRange.Value != ratio) trackRange.Value = ratio;

            plottable.AngleRange = (float)numRange.Value;
            formsPlot1.Render();
        }

        private void trackMax_ValueChanged(object sender, EventArgs e)
        {
            int ratio = trackRange.Value;
            if (numRange.Value != ratio) numRange.Value = ratio;
        }
    }
}
