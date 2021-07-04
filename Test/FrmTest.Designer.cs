
namespace Test
{
    partial class FrmTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTest));
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackStart = new System.Windows.Forms.TrackBar();
            this.lblStarting = new System.Windows.Forms.Label();
            this.lblSpace = new System.Windows.Forms.Label();
            this.lblDim = new System.Windows.Forms.Label();
            this.trackSpace = new System.Windows.Forms.TrackBar();
            this.trackDim = new System.Windows.Forms.TrackBar();
            this.trackRange = new System.Windows.Forms.TrackBar();
            this.lblRange = new System.Windows.Forms.Label();
            this.numStart = new System.Windows.Forms.NumericUpDown();
            this.numSpace = new System.Windows.Forms.NumericUpDown();
            this.numDim = new System.Windows.Forms.NumericUpDown();
            this.numRange = new System.Windows.Forms.NumericUpDown();
            this.tipRange = new System.Windows.Forms.ToolTip(this.components);
            this.tipDim = new System.Windows.Forms.ToolTip(this.components);
            this.tipSpace = new System.Windows.Forms.ToolTip(this.components);
            this.tipStarting = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackDim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRange)).BeginInit();
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formsPlot1.BackColor = System.Drawing.Color.Transparent;
            this.formsPlot1.Location = new System.Drawing.Point(0, 0);
            this.formsPlot1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(690, 403);
            this.formsPlot1.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Clockwise",
            "Anti-clockwise"});
            this.comboBox1.Location = new System.Drawing.Point(113, 409);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(114, 23);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Stacked",
            "Sequential",
            "SingleGauge"});
            this.comboBox2.Location = new System.Drawing.Point(113, 440);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(114, 23);
            this.comboBox2.TabIndex = 2;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(16, 501);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(127, 19);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Show gauge values";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(16, 526);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(183, 19);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Normalize background gauge";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // comboBox3
            // 
            this.comboBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "Inside",
            "Outside"});
            this.comboBox3.Location = new System.Drawing.Point(113, 472);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(114, 23);
            this.comboBox3.TabIndex = 5;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 412);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Gauge direction";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 443);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Gauge mode";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 475);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Gauge start";
            // 
            // trackStart
            // 
            this.trackStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackStart.Location = new System.Drawing.Point(385, 409);
            this.trackStart.Maximum = 360;
            this.trackStart.Name = "trackStart";
            this.trackStart.Size = new System.Drawing.Size(292, 45);
            this.trackStart.TabIndex = 9;
            this.trackStart.TickFrequency = 10;
            this.tipStarting.SetToolTip(this.trackStart, resources.GetString("trackStart.ToolTip"));
            this.trackStart.ValueChanged += new System.EventHandler(this.trackStart_ValueChanged);
            // 
            // lblStarting
            // 
            this.lblStarting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStarting.AutoSize = true;
            this.lblStarting.Location = new System.Drawing.Point(258, 412);
            this.lblStarting.Name = "lblStarting";
            this.lblStarting.Size = new System.Drawing.Size(80, 15);
            this.lblStarting.TabIndex = 10;
            this.lblStarting.Text = "Starting angle";
            // 
            // lblSpace
            // 
            this.lblSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSpace.AutoSize = true;
            this.lblSpace.Location = new System.Drawing.Point(258, 443);
            this.lblSpace.Name = "lblSpace";
            this.lblSpace.Size = new System.Drawing.Size(51, 15);
            this.lblSpace.TabIndex = 11;
            this.lblSpace.Text = "Space %";
            // 
            // lblDim
            // 
            this.lblDim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDim.AutoSize = true;
            this.lblDim.Location = new System.Drawing.Point(258, 474);
            this.lblDim.Name = "lblDim";
            this.lblDim.Size = new System.Drawing.Size(42, 15);
            this.lblDim.TabIndex = 12;
            this.lblDim.Text = "Dim %";
            // 
            // trackSpace
            // 
            this.trackSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackSpace.Location = new System.Drawing.Point(385, 440);
            this.trackSpace.Maximum = 100;
            this.trackSpace.Name = "trackSpace";
            this.trackSpace.Size = new System.Drawing.Size(291, 45);
            this.trackSpace.TabIndex = 13;
            this.trackSpace.TickFrequency = 10;
            this.tipSpace.SetToolTip(this.trackSpace, "The empty space between gauges as a percentage of the gauge width.\r\nValues in the" +
        " range [0-100], default value is 50 [percent]. Other values might produce unexpe" +
        "cted side-effects.");
            this.trackSpace.ValueChanged += new System.EventHandler(this.trackSpace_ValueChanged);
            // 
            // trackDim
            // 
            this.trackDim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackDim.Location = new System.Drawing.Point(385, 471);
            this.trackDim.Maximum = 100;
            this.trackDim.Name = "trackDim";
            this.trackDim.Size = new System.Drawing.Size(290, 45);
            this.trackDim.TabIndex = 14;
            this.trackDim.TickFrequency = 10;
            this.tipDim.SetToolTip(this.trackDim, "Dimmed percentage used to draw the gauges\' background.\r\nValues in the range [0-10" +
        "0], default value is 90 [percent]. Outside this range, unexpected side-effects m" +
        "ight happen.");
            this.trackDim.ValueChanged += new System.EventHandler(this.trackDim_ValueChanged);
            // 
            // trackRange
            // 
            this.trackRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackRange.Location = new System.Drawing.Point(385, 502);
            this.trackRange.Maximum = 360;
            this.trackRange.Name = "trackRange";
            this.trackRange.Size = new System.Drawing.Size(289, 45);
            this.trackRange.TabIndex = 15;
            this.trackRange.TickFrequency = 10;
            this.tipRange.SetToolTip(this.trackRange, "The maximum angular interval that the gauges will consist of.\r\nIt takes values in" +
        " the range [0-360], default value is 360. Outside this range, unexpected side-ef" +
        "fects might happen.");
            this.trackRange.ValueChanged += new System.EventHandler(this.trackMax_ValueChanged);
            // 
            // lblRange
            // 
            this.lblRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRange.AutoSize = true;
            this.lblRange.Location = new System.Drawing.Point(258, 505);
            this.lblRange.Name = "lblRange";
            this.lblRange.Size = new System.Drawing.Size(71, 15);
            this.lblRange.TabIndex = 16;
            this.lblRange.Text = "Angle range";
            // 
            // numStart
            // 
            this.numStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numStart.Location = new System.Drawing.Point(339, 410);
            this.numStart.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numStart.Name = "numStart";
            this.numStart.Size = new System.Drawing.Size(44, 23);
            this.numStart.TabIndex = 17;
            this.tipStarting.SetToolTip(this.numStart, resources.GetString("numStart.ToolTip"));
            this.numStart.ValueChanged += new System.EventHandler(this.numStart_ValueChanged);
            // 
            // numSpace
            // 
            this.numSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numSpace.Location = new System.Drawing.Point(339, 441);
            this.numSpace.Name = "numSpace";
            this.numSpace.Size = new System.Drawing.Size(44, 23);
            this.numSpace.TabIndex = 18;
            this.tipSpace.SetToolTip(this.numSpace, "The empty space between gauges as a percentage of the gauge width.\r\nValues in the" +
        " range [0-100], default value is 50 [percent]. Other values might produce unexpe" +
        "cted side-effects.");
            this.numSpace.ValueChanged += new System.EventHandler(this.numSpace_ValueChanged);
            // 
            // numDim
            // 
            this.numDim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numDim.Location = new System.Drawing.Point(339, 472);
            this.numDim.Name = "numDim";
            this.numDim.Size = new System.Drawing.Size(44, 23);
            this.numDim.TabIndex = 19;
            this.tipDim.SetToolTip(this.numDim, "Dimmed percentage used to draw the gauges\' background.\r\nValues in the range [0-10" +
        "0], default value is 90 [percent]. Outside this range, unexpected side-effects m" +
        "ight happen.");
            this.numDim.ValueChanged += new System.EventHandler(this.numDim_ValueChanged);
            // 
            // numRange
            // 
            this.numRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numRange.Location = new System.Drawing.Point(339, 503);
            this.numRange.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numRange.Name = "numRange";
            this.numRange.Size = new System.Drawing.Size(44, 23);
            this.numRange.TabIndex = 20;
            this.tipRange.SetToolTip(this.numRange, "The maximum angular interval that the gauges will consist of.\r\nIt takes values in" +
        " the range [0-360], default value is 360. Outside this range, unexpected side-ef" +
        "fects might happen.");
            this.numRange.ValueChanged += new System.EventHandler(this.numMax_ValueChanged);
            // 
            // FrmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 556);
            this.Controls.Add(this.numRange);
            this.Controls.Add(this.numDim);
            this.Controls.Add(this.numSpace);
            this.Controls.Add(this.numStart);
            this.Controls.Add(this.lblRange);
            this.Controls.Add(this.trackRange);
            this.Controls.Add(this.trackDim);
            this.Controls.Add(this.trackSpace);
            this.Controls.Add(this.lblDim);
            this.Controls.Add(this.lblSpace);
            this.Controls.Add(this.lblStarting);
            this.Controls.Add(this.trackStart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.formsPlot1);
            this.Name = "FrmTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test radial gauge plot";
            ((System.ComponentModel.ISupportInitialize)(this.trackStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackDim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRange)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScottPlot.FormsPlot formsPlot1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackStart;
        private System.Windows.Forms.Label lblStarting;
        private System.Windows.Forms.Label lblSpace;
        private System.Windows.Forms.Label lblDim;
        private System.Windows.Forms.TrackBar trackSpace;
        private System.Windows.Forms.TrackBar trackDim;
        private System.Windows.Forms.TrackBar trackRange;
        private System.Windows.Forms.Label lblRange;
        private System.Windows.Forms.NumericUpDown numStart;
        private System.Windows.Forms.NumericUpDown numSpace;
        private System.Windows.Forms.NumericUpDown numDim;
        private System.Windows.Forms.NumericUpDown numRange;
        private System.Windows.Forms.ToolTip tipRange;
        private System.Windows.Forms.ToolTip tipDim;
        private System.Windows.Forms.ToolTip tipSpace;
        private System.Windows.Forms.ToolTip tipStarting;
    }
}