using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestControl
{
    public partial class FrmTestControl : Form
    {
        public FrmTestControl()
        {
            InitializeComponent();

            // Combo boxes
            //comboBox1.SelectedIndex = 0;
            //comboBox2.SelectedIndex = 0;
            //comboBox3.SelectedIndex = 0;

            // Check boxes
            checkBox1.Checked = plot1.ShowGaugeValues;
            checkBox2.Checked = plot1.NormBackGauge;

            // Other numeric controls
            numLabelFraction.Value = (decimal)plot1.GaugeLabelPos;
            numStart.Value = (decimal)plot1.StartingAngleGauges;
            numSpace.Value = (decimal)plot1.GaugeSpacePercentage;
            numDim.Value = (decimal)plot1.DimPercentage;
            numRange.Value = (decimal)plot1.AngleRange;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            plot1.GaugeDirection = (RadialGaugeControl.RadialGaugePlot.RadialGaugeDirection)comboBox1.SelectedIndex;
            plot1.Render();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            plot1.GaugeMode = (RadialGaugeControl.RadialGaugePlot.RadialGaugeMode)comboBox2.SelectedIndex;
            plot1.Render();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            plot1.GaugeStart = (RadialGaugeControl.RadialGaugePlot.RadialGaugeStart)comboBox3.SelectedIndex;
            plot1.Render();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            plot1.ShowGaugeValues = checkBox1.Checked;
            numLabelFraction.Enabled = checkBox1.Checked;
            plot1.Render();
        }
        private void numLabelFraction_ValueChanged(object sender, EventArgs e)
        {
            plot1.GaugeLabelPos = Convert.ToSingle(numLabelFraction.Value);
            plot1.Render();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            plot1.NormBackGauge = checkBox2.Checked;
            plot1.Render();
        }

        private void numStart_ValueChanged(object sender, EventArgs e)
        {
            int ratio = Convert.ToInt32(numStart.Value);
            if (trackStart.Value != ratio) trackStart.Value = ratio;

            plot1.StartingAngleGauges = (float)numStart.Value;
            plot1.Render();
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

            plot1.GaugeSpacePercentage = (float)numSpace.Value;
            plot1.Render();
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

            plot1.DimPercentage = (float)numDim.Value;
            plot1.Render();
        }

        private void trackDim_ValueChanged(object sender, EventArgs e)
        {
            int ratio = trackDim.Value;
            if (numDim.Value != ratio) numDim.Value = ratio;
        }

        private void numRange_ValueChanged(object sender, EventArgs e)
        {
            int ratio = Convert.ToInt32(numRange.Value);
            if (trackRange.Value != ratio) trackRange.Value = ratio;

            plot1.AngleRange = (float)numRange.Value;
            plot1.Render();
        }

        private void trackRange_ValueChanged(object sender, EventArgs e)
        {
            int ratio = trackRange.Value;
            if (numRange.Value != ratio) numRange.Value = ratio;
        }

    }
}
