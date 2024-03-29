﻿using System;
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
            comboBox1.DataSource = Enum.GetValues(typeof(RadialGaugePlot.RadialGaugeDirection));
            comboBox2.DataSource = Enum.GetValues(typeof(RadialGaugePlot.RadialGaugeMode));
            comboBox3.DataSource = Enum.GetValues(typeof(RadialGaugePlot.RadialGaugeStart));
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

            // Check boxes
            checkBox1.Checked = plot1.ShowGaugeValues;
            checkBox2.Checked = plot1.CircularBackground;

            // Other numeric controls
            numLabelFraction.Value = 100 * (decimal)plot1.GaugeLabelsPosition;
            numStart.Value = (decimal)plot1.StartingAngleGauges;
            numSpace.Value = 100 * (decimal)plot1.GaugeSpaceFraction;
            numDim.Value = 100 * (decimal)plot1.BackTransparency;
            numRange.Value = (decimal)plot1.AngleRange;

            // plot1.Palette = new Plotting.Colorsets.Palette(new Plotting.Colorsets.Custom());
            plot1.Palette = Plotting.Colorsets.Palette.OneHalf;
            plot1.PlotTitle = "Example title";
            plot1.Update(new double[] { 100, 80, 65, 45, -20 },
                new string[] { "alpha", "beta", "gamma", "delta", "epsilon" });
            plot1.Legend.IsVisible = true;
            plot1.Legend.Location = RadialGaugePlot.Alignment.UpperRight;
            plot1.Render();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            plot1.GaugeDirection = (RadialGaugePlot.RadialGaugeDirection)comboBox1.SelectedIndex;
            plot1.Render();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            plot1.GaugeMode = (RadialGaugePlot.RadialGaugeMode)comboBox2.SelectedIndex;
            plot1.Render();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            plot1.GaugeStart = (RadialGaugePlot.RadialGaugeStart)comboBox3.SelectedIndex;
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
            plot1.GaugeLabelsPosition = Convert.ToSingle(numLabelFraction.Value / 100);
            plot1.Render();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            plot1.CircularBackground = checkBox2.Checked;
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

            plot1.GaugeSpaceFraction = (float)numSpace.Value / 100;
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

            plot1.BackTransparency = (float)numDim.Value / 100;
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
