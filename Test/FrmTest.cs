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

            double[] values = {100, 80, 65, 42, 20 };



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
            plottable = new(values, colors, false);
            plottable.CategoryLabels = new string [] { "C #1", "C #2", "C #3", "C #4", "C #5" };
            plottable.GroupLabels = new string[] { "G #1", "G #2", "G #3", "G #4", "G #5" };
            //plottable.StartingAngle = 150;
            formsPlot1.Plot.Add(plottable);
            formsPlot1.Plot.Frameless();
            formsPlot1.Plot.Grid(enable: false);
            //formsPlot1.Plot.XAxis2.Label("Radial gauge plot");
            formsPlot1.Plot.Title("Radial gauge plot");
            formsPlot1.Plot.Legend(enable: true, ScottPlot.Alignment.LowerRight);

            // Combo
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            plottable.ShowGaugeValues = checkBox1.Checked;
            formsPlot1.Render();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            plottable.NormBackGauge = checkBox2.Checked;
            formsPlot1.Render();
        }
    }
}
