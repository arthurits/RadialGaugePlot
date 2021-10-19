
namespace TestControl
{
    partial class FrmTestControl
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTestControl));
            RadialGaugePlot.PlotElement plotElement1 = new RadialGaugePlot.PlotElement();
            Drawing.Font font1 = new Drawing.Font();
            RadialGaugePlot.PlotElement plotElement2 = new RadialGaugePlot.PlotElement();
            Drawing.Font font2 = new Drawing.Font();
            RadialGaugePlot.PlotElement plotElement3 = new RadialGaugePlot.PlotElement();
            Drawing.Font font3 = new Drawing.Font();
            RadialGaugePlot.PlotElement plotElement4 = new RadialGaugePlot.PlotElement();
            Drawing.Font font4 = new Drawing.Font();
            this.plot1 = new RadialGaugePlot.RadialGaugePlot();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.trackStart = new System.Windows.Forms.TrackBar();
            this.trackSpace = new System.Windows.Forms.TrackBar();
            this.trackDim = new System.Windows.Forms.TrackBar();
            this.trackRange = new System.Windows.Forms.TrackBar();
            this.numStart = new System.Windows.Forms.NumericUpDown();
            this.numSpace = new System.Windows.Forms.NumericUpDown();
            this.numDim = new System.Windows.Forms.NumericUpDown();
            this.numRange = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.numLabelFraction = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackDim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLabelFraction)).BeginInit();
            this.SuspendLayout();
            // 
            // plot1
            // 
            this.plot1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plot1.AngleRange = 360D;
            this.plot1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.plot1.BackTransparency = 0.9F;
            this.plot1.Center = ((System.Drawing.PointF)(resources.GetObject("plot1.Center")));
            font1.Bold = false;
            font1.Color = System.Drawing.Color.Black;
            font1.Name = "Segoe UI";
            font1.Rotation = 0F;
            font1.Size = 12F;
            plotElement1.Font = font1;
            plotElement1.Margin = new System.Windows.Forms.Padding(11);
            plotElement1.Padding = new System.Windows.Forms.Padding(0);
            plotElement1.Text = null;
            plotElement1.Visible = true;
            this.plot1.Chart = plotElement1;
            this.plot1.CircularBackground = true;
            this.plot1.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(100)))), ((int)(((byte)(137))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(185)))), ((int)(((byte)(192))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(213)))), ((int)(((byte)(133))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(193)))), ((int)(((byte)(81))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(127)))), ((int)(((byte)(100)))))};
            this.plot1.Data = new double[] {
        100D,
        80D,
        65D,
        45D,
        -20D};
            this.plot1.EndCap = System.Drawing.Drawing2D.LineCap.Triangle;
            this.plot1.GaugeDirection = RadialGaugePlot.RadialGaugeDirection.Clockwise;
            this.plot1.GaugeLabelsColor = System.Drawing.Color.White;
            this.plot1.GaugeLabelsFontFraction = 0.7F;
            this.plot1.GaugeLabelsPosition = 1F;
            this.plot1.GaugeMode = RadialGaugePlot.RadialGaugeMode.Stacked;
            this.plot1.GaugeSpaceFraction = 0.5F;
            this.plot1.GaugeStart = RadialGaugePlot.RadialGaugeStart.InsideToOutside;
            this.plot1.LineWidth = -1F;
            this.plot1.Location = new System.Drawing.Point(0, 0);
            this.plot1.MarginFactor = 0.03F;
            this.plot1.Name = "plot1";
            this.plot1.PlotTitle = null;
            this.plot1.ShowGaugeValues = true;
            this.plot1.Size = new System.Drawing.Size(784, 370);
            this.plot1.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            this.plot1.StartingAngleBackGauges = 210F;
            this.plot1.StartingAngleGauges = 270F;
            this.plot1.TabIndex = 0;
            font2.Bold = false;
            font2.Color = System.Drawing.Color.Black;
            font2.Name = "Segoe UI";
            font2.Rotation = 0F;
            font2.Size = 22.5F;
            plotElement2.Font = font2;
            plotElement2.Margin = new System.Windows.Forms.Padding(0);
            plotElement2.Padding = new System.Windows.Forms.Padding(0);
            plotElement2.Text = null;
            plotElement2.Visible = true;
            // this.plot1.Title = plotElement2;
            font3.Bold = false;
            font3.Color = System.Drawing.Color.Black;
            font3.Name = "Segoe UI";
            font3.Rotation = 0F;
            font3.Size = 12F;
            plotElement3.Font = font3;
            plotElement3.Margin = new System.Windows.Forms.Padding(0);
            plotElement3.Padding = new System.Windows.Forms.Padding(0);
            plotElement3.Text = null;
            plotElement3.Visible = false;
            this.plot1.Xaxis = plotElement3;
            font4.Bold = false;
            font4.Color = System.Drawing.Color.Black;
            font4.Name = "Segoe UI";
            font4.Rotation = 0F;
            font4.Size = 12F;
            plotElement4.Font = font4;
            plotElement4.Margin = new System.Windows.Forms.Padding(0);
            plotElement4.Padding = new System.Windows.Forms.Padding(0);
            plotElement4.Text = null;
            plotElement4.Visible = false;
            this.plot1.Yaxis = plotElement4;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(133, 386);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(142, 23);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(133, 426);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(142, 23);
            this.comboBox2.TabIndex = 2;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboBox3
            // 
            this.comboBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(133, 466);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(142, 23);
            this.comboBox3.TabIndex = 3;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // trackStart
            // 
            this.trackStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackStart.Location = new System.Drawing.Point(487, 386);
            this.trackStart.Maximum = 360;
            this.trackStart.Name = "trackStart";
            this.trackStart.Size = new System.Drawing.Size(277, 45);
            this.trackStart.TabIndex = 4;
            this.trackStart.TickFrequency = 10;
            this.trackStart.ValueChanged += new System.EventHandler(this.trackStart_ValueChanged);
            // 
            // trackSpace
            // 
            this.trackSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackSpace.Location = new System.Drawing.Point(487, 426);
            this.trackSpace.Maximum = 100;
            this.trackSpace.Name = "trackSpace";
            this.trackSpace.Size = new System.Drawing.Size(277, 45);
            this.trackSpace.TabIndex = 5;
            this.trackSpace.TickFrequency = 10;
            this.trackSpace.ValueChanged += new System.EventHandler(this.trackSpace_ValueChanged);
            // 
            // trackDim
            // 
            this.trackDim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackDim.Location = new System.Drawing.Point(487, 466);
            this.trackDim.Maximum = 100;
            this.trackDim.Name = "trackDim";
            this.trackDim.Size = new System.Drawing.Size(277, 45);
            this.trackDim.TabIndex = 6;
            this.trackDim.TickFrequency = 10;
            this.trackDim.ValueChanged += new System.EventHandler(this.trackDim_ValueChanged);
            // 
            // trackRange
            // 
            this.trackRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackRange.Location = new System.Drawing.Point(487, 506);
            this.trackRange.Maximum = 360;
            this.trackRange.Name = "trackRange";
            this.trackRange.Size = new System.Drawing.Size(277, 45);
            this.trackRange.TabIndex = 7;
            this.trackRange.TickFrequency = 10;
            this.trackRange.ValueChanged += new System.EventHandler(this.trackRange_ValueChanged);
            // 
            // numStart
            // 
            this.numStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numStart.Location = new System.Drawing.Point(434, 386);
            this.numStart.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numStart.Name = "numStart";
            this.numStart.Size = new System.Drawing.Size(47, 23);
            this.numStart.TabIndex = 8;
            this.numStart.ValueChanged += new System.EventHandler(this.numStart_ValueChanged);
            // 
            // numSpace
            // 
            this.numSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numSpace.Location = new System.Drawing.Point(434, 426);
            this.numSpace.Name = "numSpace";
            this.numSpace.Size = new System.Drawing.Size(47, 23);
            this.numSpace.TabIndex = 9;
            this.numSpace.ValueChanged += new System.EventHandler(this.numSpace_ValueChanged);
            // 
            // numDim
            // 
            this.numDim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numDim.Location = new System.Drawing.Point(434, 466);
            this.numDim.Name = "numDim";
            this.numDim.Size = new System.Drawing.Size(47, 23);
            this.numDim.TabIndex = 10;
            this.numDim.ValueChanged += new System.EventHandler(this.numDim_ValueChanged);
            // 
            // numRange
            // 
            this.numRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numRange.Location = new System.Drawing.Point(434, 506);
            this.numRange.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numRange.Name = "numRange";
            this.numRange.Size = new System.Drawing.Size(47, 23);
            this.numRange.TabIndex = 11;
            this.numRange.ValueChanged += new System.EventHandler(this.numRange_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 388);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "Gauge direction";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 428);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "Gauge mode";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 468);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Gauge start";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(36, 508);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(88, 19);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "Show labels";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(36, 538);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(186, 19);
            this.checkBox2.TabIndex = 16;
            this.checkBox2.Text = "Full-circle background gauges";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // numLabelFraction
            // 
            this.numLabelFraction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numLabelFraction.Location = new System.Drawing.Point(133, 506);
            this.numLabelFraction.Name = "numLabelFraction";
            this.numLabelFraction.Size = new System.Drawing.Size(66, 23);
            this.numLabelFraction.TabIndex = 17;
            this.numLabelFraction.ValueChanged += new System.EventHandler(this.numLabelFraction_ValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(348, 388);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 15);
            this.label4.TabIndex = 18;
            this.label4.Text = "Starting angle";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(348, 428);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 15);
            this.label5.TabIndex = 19;
            this.label5.Text = "Space %";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(348, 468);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 15);
            this.label6.TabIndex = 20;
            this.label6.Text = "Dim %";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(348, 508);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 15);
            this.label7.TabIndex = 21;
            this.label7.Text = "Angle range";
            // 
            // FrmTestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numLabelFraction);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numRange);
            this.Controls.Add(this.numDim);
            this.Controls.Add(this.numSpace);
            this.Controls.Add(this.numStart);
            this.Controls.Add(this.trackRange);
            this.Controls.Add(this.trackDim);
            this.Controls.Add(this.trackSpace);
            this.Controls.Add(this.trackStart);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.plot1);
            this.Name = "FrmTestControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test control";
            ((System.ComponentModel.ISupportInitialize)(this.trackStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackDim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLabelFraction)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RadialGaugePlot.RadialGaugePlot plot1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.TrackBar trackStart;
        private System.Windows.Forms.TrackBar trackSpace;
        private System.Windows.Forms.TrackBar trackDim;
        private System.Windows.Forms.TrackBar trackRange;
        private System.Windows.Forms.NumericUpDown numStart;
        private System.Windows.Forms.NumericUpDown numSpace;
        private System.Windows.Forms.NumericUpDown numDim;
        private System.Windows.Forms.NumericUpDown numRange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.NumericUpDown numLabelFraction;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}

